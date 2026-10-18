--
-- PostgreSQL database dump
--

\restrict Xz2ym3C1MrRRETSLN6MTAd3N2UDnQeNYjOv1ysD8KxXboocaUY8WhC9neJcaRTB

-- Dumped from database version 17.8
-- Dumped by pg_dump version 18.2

-- Started on 2026-10-18 23:55:46

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 5 (class 2615 OID 16682)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

-- *not* creating schema, since initdb creates it


ALTER SCHEMA public OWNER TO postgres;

--
-- TOC entry 4967 (class 0 OID 0)
-- Dependencies: 5
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON SCHEMA public IS '';


--
-- TOC entry 239 (class 1255 OID 16788)
-- Name: calculate_credit_rating(integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.calculate_credit_rating(p_client_id integer, p_user_id integer) RETURNS TABLE(credit_rating_id integer, score integer, rating_level character varying, debt_load_ratio numeric, decision character varying, comment text)
    LANGUAGE plpgsql
    AS $$
DECLARE
    v_income numeric(12, 2);
    v_expenses numeric(12, 2);

    v_total_monthly_payment numeric(12, 2);
    v_total_remaining_debt numeric(12, 2);
    v_active_credit_count integer;
    v_overdue_credit_count integer;

    v_debt_load_ratio numeric(5, 2);
    v_expense_ratio numeric(5, 2);
    v_score integer;

    v_has_stable_income boolean;
    v_has_student_status boolean;
    v_has_pensioner_status boolean;
    v_has_unemployed_status boolean;

    v_rating_level varchar(20);
    v_decision varchar(30);
    v_comment text;

    v_credit_rating_id integer;
BEGIN
    -- 1. Проверяем существование клиента
    IF NOT EXISTS (
        SELECT 1
        FROM clients c
        WHERE c.client_id = p_client_id
    ) THEN
        RAISE EXCEPTION 'Клиент с ID % не найден', p_client_id;
    END IF;

    -- 2. Суммируем все финансовые записи клиента
    SELECT
        COALESCE(SUM(fd.monthly_income), 0),
        COALESCE(SUM(fd.monthly_expenses), 0)
    INTO
        v_income,
        v_expenses
    FROM financial_data fd
    WHERE fd.client_id = p_client_id;

    IF v_income IS NULL OR v_income <= 0 THEN
        RAISE EXCEPTION 'Для клиента с ID % не найдены корректные финансовые данные', p_client_id;
    END IF;

    -- 3. Анализируем все статусы занятости клиента
    SELECT EXISTS (
        SELECT 1
        FROM financial_data fd
        WHERE fd.client_id = p_client_id
          AND fd.employment_status IN ('Работает', 'ИП', 'Самозанятый')
    )
    INTO v_has_stable_income;

    SELECT EXISTS (
        SELECT 1
        FROM financial_data fd
        WHERE fd.client_id = p_client_id
          AND fd.employment_status = 'Студент'
    )
    INTO v_has_student_status;

    SELECT EXISTS (
        SELECT 1
        FROM financial_data fd
        WHERE fd.client_id = p_client_id
          AND fd.employment_status = 'Пенсионер'
    )
    INTO v_has_pensioner_status;

    SELECT EXISTS (
        SELECT 1
        FROM financial_data fd
        WHERE fd.client_id = p_client_id
          AND fd.employment_status = 'Не работает'
    )
    INTO v_has_unemployed_status;

    -- 4. Получаем данные по кредитам клиента
    SELECT
        COALESCE(SUM(c.monthly_payment), 0),
        COALESCE(SUM(c.remaining_debt), 0),
        COUNT(*) FILTER (WHERE c.status = 'Активный'),
        COUNT(*) FILTER (WHERE c.status = 'Просрочен')
    INTO
        v_total_monthly_payment,
        v_total_remaining_debt,
        v_active_credit_count,
        v_overdue_credit_count
    FROM credits c
    WHERE c.client_id = p_client_id
      AND c.status <> 'Закрыт';

    -- 5. Долговая нагрузка считается только по кредитным платежам
    v_debt_load_ratio := ROUND(
        (v_total_monthly_payment / v_income) * 100,
        2
    );

    -- 6. Отдельно считаем долю обычных расходов
    v_expense_ratio := ROUND(
        (v_expenses / v_income) * 100,
        2
    );

    -- 7. Базовый рейтинг
    v_score := 100;

    -- 8. Штраф за кредитную долговую нагрузку
    IF v_debt_load_ratio > 90 THEN
        v_score := v_score - 70;
    ELSIF v_debt_load_ratio > 75 THEN
        v_score := v_score - 55;
    ELSIF v_debt_load_ratio > 60 THEN
        v_score := v_score - 40;
    ELSIF v_debt_load_ratio > 45 THEN
        v_score := v_score - 25;
    ELSIF v_debt_load_ratio > 30 THEN
        v_score := v_score - 10;
    END IF;

    -- 9. Дополнительный штраф за высокий уровень обычных расходов
    IF v_expense_ratio > 90 THEN
        v_score := v_score - 25;
    ELSIF v_expense_ratio > 75 THEN
        v_score := v_score - 15;
    ELSIF v_expense_ratio > 60 THEN
        v_score := v_score - 10;
    END IF;

    -- 10. Штраф за общий остаток долга относительно суммарного дохода
    IF v_total_remaining_debt > v_income * 18 THEN
        v_score := v_score - 25;
    ELSIF v_total_remaining_debt > v_income * 12 THEN
        v_score := v_score - 20;
    ELSIF v_total_remaining_debt > v_income * 6 THEN
        v_score := v_score - 10;
    END IF;

    -- 11. Штраф за количество активных кредитов
    IF v_active_credit_count >= 5 THEN
        v_score := v_score - 15;
    ELSIF v_active_credit_count >= 3 THEN
        v_score := v_score - 10;
    ELSIF v_active_credit_count = 2 THEN
        v_score := v_score - 5;
    END IF;

    -- 12. Штраф за просроченные кредиты
    IF v_overdue_credit_count > 0 THEN
        v_score := v_score - 30;
    END IF;

    -- 13. Штраф за статус занятости
    -- Если есть работа, ИП или самозанятость, штраф не начисляется.
    IF v_has_stable_income THEN
        v_score := v_score;
    ELSIF v_has_unemployed_status THEN
        v_score := v_score - 25;
    ELSIF v_has_student_status THEN
        v_score := v_score - 10;
    ELSIF v_has_pensioner_status THEN
        v_score := v_score - 5;
    END IF;

    -- 14. Ограничиваем рейтинг диапазоном 0-100
    IF v_score < 0 THEN
        v_score := 0;
    END IF;

    IF v_score > 100 THEN
        v_score := 100;
    END IF;

    -- 15. Определяем уровень рейтинга и решение
    IF v_score >= 80 THEN
        v_rating_level := 'Высокий';
        v_decision := 'Одобрить';
        v_comment := 'Клиент имеет высокий кредитный рейтинг и низкий уровень кредитной нагрузки.';
    ELSIF v_score >= 60 THEN
        v_rating_level := 'Средний';
        v_decision := 'Рассмотреть';
        v_comment := 'Клиент имеет средний кредитный рейтинг. Требуется дополнительная проверка.';
    ELSE
        v_rating_level := 'Низкий';
        v_decision := 'Отказать';
        v_comment := 'Клиент имеет низкий кредитный рейтинг и повышенный уровень риска.';
    END IF;

    -- 16. Сохраняем результат расчета
    INSERT INTO credit_ratings (
        client_id,
        score,
        rating_level,
        debt_load_ratio,
        decision,
        comment,
        calculation_date,
        calculated_by
    )
    VALUES (
        p_client_id,
        v_score,
        v_rating_level,
        v_debt_load_ratio,
        v_decision,
        v_comment,
        NOW(),
        p_user_id
    )
    RETURNING credit_ratings.credit_rating_id
    INTO v_credit_rating_id;

    -- 17. Возвращаем результат в приложение
    RETURN QUERY
    SELECT
        v_credit_rating_id,
        v_score,
        v_rating_level::varchar,
        v_debt_load_ratio,
        v_decision::varchar,
        v_comment;
END;
$$;


ALTER FUNCTION public.calculate_credit_rating(p_client_id integer, p_user_id integer) OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 220 (class 1259 OID 16695)
-- Name: clients; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.clients (
    client_id integer NOT NULL,
    full_name character varying(100) NOT NULL,
    birth_date date NOT NULL,
    phone character varying(20),
    email character varying(100),
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    created_by integer
);


ALTER TABLE public.clients OWNER TO postgres;

--
-- TOC entry 4969 (class 0 OID 0)
-- Dependencies: 220
-- Name: TABLE clients; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public.clients IS 'Клиенты, для которых выполняется анализ кредитного рейтинга';


--
-- TOC entry 4970 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN clients.client_id; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.clients.client_id IS 'Идентификатор клиента';


--
-- TOC entry 4971 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN clients.full_name; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.clients.full_name IS 'ФИО клиента';


--
-- TOC entry 4972 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN clients.birth_date; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.clients.birth_date IS 'Дата рождения клиента';


--
-- TOC entry 4973 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN clients.phone; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.clients.phone IS 'Телефон клиента';


--
-- TOC entry 4974 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN clients.email; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.clients.email IS 'Электронная почта клиента';


--
-- TOC entry 4975 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN clients.created_at; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.clients.created_at IS 'Дата добавления клиента';


--
-- TOC entry 4976 (class 0 OID 0)
-- Dependencies: 220
-- Name: COLUMN clients.created_by; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.clients.created_by IS 'Пользователь, добавивший клиента';


--
-- TOC entry 226 (class 1259 OID 16743)
-- Name: credit_ratings; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.credit_ratings (
    credit_rating_id integer NOT NULL,
    client_id integer NOT NULL,
    score integer NOT NULL,
    rating_level character varying(20) NOT NULL,
    debt_load_ratio numeric(5,2),
    decision character varying(30) NOT NULL,
    comment text,
    calculation_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    calculated_by integer,
    CONSTRAINT chk_credit_ratings_debt_load_ratio CHECK (((debt_load_ratio IS NULL) OR (debt_load_ratio >= (0)::numeric))),
    CONSTRAINT chk_credit_ratings_decision CHECK (((decision)::text = ANY ((ARRAY['Одобрить'::character varying, 'Рассмотреть'::character varying, 'Отказать'::character varying])::text[]))),
    CONSTRAINT chk_credit_ratings_rating_level CHECK (((rating_level)::text = ANY ((ARRAY['Высокий'::character varying, 'Средний'::character varying, 'Низкий'::character varying, 'Критический'::character varying])::text[]))),
    CONSTRAINT chk_credit_ratings_score CHECK (((score >= 0) AND (score <= 100)))
);


ALTER TABLE public.credit_ratings OWNER TO postgres;

--
-- TOC entry 4977 (class 0 OID 0)
-- Dependencies: 226
-- Name: TABLE credit_ratings; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public.credit_ratings IS 'Результаты расчета кредитного рейтинга клиента';


--
-- TOC entry 4978 (class 0 OID 0)
-- Dependencies: 226
-- Name: COLUMN credit_ratings.credit_rating_id; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credit_ratings.credit_rating_id IS 'Идентификатор расчета рейтинга';


--
-- TOC entry 4979 (class 0 OID 0)
-- Dependencies: 226
-- Name: COLUMN credit_ratings.client_id; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credit_ratings.client_id IS 'Идентификатор клиента';


--
-- TOC entry 4980 (class 0 OID 0)
-- Dependencies: 226
-- Name: COLUMN credit_ratings.score; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credit_ratings.score IS 'Числовое значение кредитного рейтинга';


--
-- TOC entry 4981 (class 0 OID 0)
-- Dependencies: 226
-- Name: COLUMN credit_ratings.rating_level; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credit_ratings.rating_level IS 'Уровень кредитного рейтинга';


--
-- TOC entry 4982 (class 0 OID 0)
-- Dependencies: 226
-- Name: COLUMN credit_ratings.debt_load_ratio; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credit_ratings.debt_load_ratio IS 'Коэффициент долговой нагрузки';


--
-- TOC entry 4983 (class 0 OID 0)
-- Dependencies: 226
-- Name: COLUMN credit_ratings.decision; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credit_ratings.decision IS 'Решение по результатам анализа';


--
-- TOC entry 4984 (class 0 OID 0)
-- Dependencies: 226
-- Name: COLUMN credit_ratings.comment; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credit_ratings.comment IS 'Комментарий к расчету';


--
-- TOC entry 4985 (class 0 OID 0)
-- Dependencies: 226
-- Name: COLUMN credit_ratings.calculation_date; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credit_ratings.calculation_date IS 'Дата расчета рейтинга';


--
-- TOC entry 4986 (class 0 OID 0)
-- Dependencies: 226
-- Name: COLUMN credit_ratings.calculated_by; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credit_ratings.calculated_by IS 'Пользователь, выполнивший расчет';


--
-- TOC entry 224 (class 1259 OID 16726)
-- Name: credits; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.credits (
    credit_id integer NOT NULL,
    client_id integer NOT NULL,
    credit_amount numeric(12,2) NOT NULL,
    remaining_debt numeric(12,2) NOT NULL,
    monthly_payment numeric(12,2) NOT NULL,
    status character varying(20) NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    CONSTRAINT chk_credits_credit_amount CHECK ((credit_amount >= (0)::numeric)),
    CONSTRAINT chk_credits_monthly_payment CHECK ((monthly_payment >= (0)::numeric)),
    CONSTRAINT chk_credits_remaining_debt CHECK ((remaining_debt >= (0)::numeric)),
    CONSTRAINT chk_credits_status CHECK (((status)::text = ANY ((ARRAY['Активный'::character varying, 'Закрыт'::character varying, 'Просрочен'::character varying])::text[])))
);


ALTER TABLE public.credits OWNER TO postgres;

--
-- TOC entry 4987 (class 0 OID 0)
-- Dependencies: 224
-- Name: TABLE credits; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public.credits IS 'Кредиты клиента';


--
-- TOC entry 4988 (class 0 OID 0)
-- Dependencies: 224
-- Name: COLUMN credits.credit_id; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credits.credit_id IS 'Идентификатор кредита';


--
-- TOC entry 4989 (class 0 OID 0)
-- Dependencies: 224
-- Name: COLUMN credits.client_id; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credits.client_id IS 'Идентификатор клиента';


--
-- TOC entry 4990 (class 0 OID 0)
-- Dependencies: 224
-- Name: COLUMN credits.credit_amount; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credits.credit_amount IS 'Первоначальная сумма кредита';


--
-- TOC entry 4991 (class 0 OID 0)
-- Dependencies: 224
-- Name: COLUMN credits.remaining_debt; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credits.remaining_debt IS 'Оставшаяся задолженность';


--
-- TOC entry 4992 (class 0 OID 0)
-- Dependencies: 224
-- Name: COLUMN credits.monthly_payment; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credits.monthly_payment IS 'Ежемесячный платеж по кредиту';


--
-- TOC entry 4993 (class 0 OID 0)
-- Dependencies: 224
-- Name: COLUMN credits.status; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credits.status IS 'Статус кредита';


--
-- TOC entry 4994 (class 0 OID 0)
-- Dependencies: 224
-- Name: COLUMN credits.created_at; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.credits.created_at IS 'Дата добавления кредита';


--
-- TOC entry 222 (class 1259 OID 16710)
-- Name: financial_data; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.financial_data (
    financial_data_id integer NOT NULL,
    client_id integer NOT NULL,
    monthly_income numeric(12,2) NOT NULL,
    monthly_expenses numeric(12,2) NOT NULL,
    employment_status character varying(50),
    updated_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    CONSTRAINT chk_financial_data_employment_status CHECK (((employment_status IS NULL) OR ((employment_status)::text = ANY ((ARRAY['Работает'::character varying, 'Не работает'::character varying, 'Самозанятый'::character varying, 'ИП'::character varying, 'Пенсионер'::character varying, 'Студент'::character varying])::text[])))),
    CONSTRAINT chk_financial_data_expenses CHECK ((monthly_expenses >= (0)::numeric)),
    CONSTRAINT chk_financial_data_income CHECK ((monthly_income >= (0)::numeric))
);


ALTER TABLE public.financial_data OWNER TO postgres;

--
-- TOC entry 4995 (class 0 OID 0)
-- Dependencies: 222
-- Name: TABLE financial_data; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public.financial_data IS 'Финансовые данные клиента';


--
-- TOC entry 4996 (class 0 OID 0)
-- Dependencies: 222
-- Name: COLUMN financial_data.financial_data_id; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.financial_data.financial_data_id IS 'Идентификатор финансовой записи';


--
-- TOC entry 4997 (class 0 OID 0)
-- Dependencies: 222
-- Name: COLUMN financial_data.client_id; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.financial_data.client_id IS 'Идентификатор клиента';


--
-- TOC entry 4998 (class 0 OID 0)
-- Dependencies: 222
-- Name: COLUMN financial_data.monthly_income; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.financial_data.monthly_income IS 'Ежемесячный доход клиента';


--
-- TOC entry 4999 (class 0 OID 0)
-- Dependencies: 222
-- Name: COLUMN financial_data.monthly_expenses; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.financial_data.monthly_expenses IS 'Ежемесячные расходы клиента';


--
-- TOC entry 5000 (class 0 OID 0)
-- Dependencies: 222
-- Name: COLUMN financial_data.employment_status; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.financial_data.employment_status IS 'Статус занятости клиента';


--
-- TOC entry 5001 (class 0 OID 0)
-- Dependencies: 222
-- Name: COLUMN financial_data.updated_at; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.financial_data.updated_at IS 'Дата обновления финансовых данных';


--
-- TOC entry 218 (class 1259 OID 16684)
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    user_id integer NOT NULL,
    login character varying(50) NOT NULL,
    password_hash character varying(255) NOT NULL,
    full_name character varying(100) NOT NULL,
    role character varying(20) NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    CONSTRAINT chk_users_role CHECK (((role)::text = ANY ((ARRAY['Пользователь'::character varying, 'Администратор'::character varying])::text[])))
);


ALTER TABLE public.users OWNER TO postgres;

--
-- TOC entry 5002 (class 0 OID 0)
-- Dependencies: 218
-- Name: TABLE users; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON TABLE public.users IS 'Пользователи системы';


--
-- TOC entry 5003 (class 0 OID 0)
-- Dependencies: 218
-- Name: COLUMN users.user_id; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.users.user_id IS 'Идентификатор пользователя';


--
-- TOC entry 5004 (class 0 OID 0)
-- Dependencies: 218
-- Name: COLUMN users.login; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.users.login IS 'Логин пользователя';


--
-- TOC entry 5005 (class 0 OID 0)
-- Dependencies: 218
-- Name: COLUMN users.password_hash; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.users.password_hash IS 'Хэш пароля пользователя';


--
-- TOC entry 5006 (class 0 OID 0)
-- Dependencies: 218
-- Name: COLUMN users.full_name; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.users.full_name IS 'ФИО пользователя';


--
-- TOC entry 5007 (class 0 OID 0)
-- Dependencies: 218
-- Name: COLUMN users.role; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.users.role IS 'Роль пользователя в системе';


--
-- TOC entry 5008 (class 0 OID 0)
-- Dependencies: 218
-- Name: COLUMN users.created_at; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN public.users.created_at IS 'Дата регистрации пользователя';


--
-- TOC entry 227 (class 1259 OID 16774)
-- Name: client_rating_report; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.client_rating_report AS
 SELECT c.client_id,
    c.full_name AS client_full_name,
    c.birth_date,
    c.phone,
    c.email,
    fd.monthly_income,
    fd.monthly_expenses,
    fd.employment_status,
    COALESCE(sum(cr.remaining_debt), (0)::numeric) AS total_remaining_debt,
    COALESCE(sum(cr.monthly_payment), (0)::numeric) AS total_monthly_payment,
    count(cr.credit_id) AS credits_count,
    rt.score,
    rt.rating_level,
    rt.debt_load_ratio,
    rt.decision,
    rt.comment,
    rt.calculation_date,
    u.full_name AS calculated_by_user
   FROM ((((public.clients c
     LEFT JOIN LATERAL ( SELECT fd1.financial_data_id,
            fd1.client_id,
            fd1.monthly_income,
            fd1.monthly_expenses,
            fd1.employment_status,
            fd1.updated_at
           FROM public.financial_data fd1
          WHERE (fd1.client_id = c.client_id)
          ORDER BY fd1.updated_at DESC
         LIMIT 1) fd ON (true))
     LEFT JOIN public.credits cr ON ((cr.client_id = c.client_id)))
     LEFT JOIN LATERAL ( SELECT rt1.credit_rating_id,
            rt1.client_id,
            rt1.score,
            rt1.rating_level,
            rt1.debt_load_ratio,
            rt1.decision,
            rt1.comment,
            rt1.calculation_date,
            rt1.calculated_by
           FROM public.credit_ratings rt1
          WHERE (rt1.client_id = c.client_id)
          ORDER BY rt1.calculation_date DESC
         LIMIT 1) rt ON (true))
     LEFT JOIN public.users u ON ((u.user_id = rt.calculated_by)))
  GROUP BY c.client_id, c.full_name, c.birth_date, c.phone, c.email, fd.monthly_income, fd.monthly_expenses, fd.employment_status, rt.score, rt.rating_level, rt.debt_load_ratio, rt.decision, rt.comment, rt.calculation_date, u.full_name;


ALTER VIEW public.client_rating_report OWNER TO postgres;

--
-- TOC entry 5009 (class 0 OID 0)
-- Dependencies: 227
-- Name: VIEW client_rating_report; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON VIEW public.client_rating_report IS 'Отчет по клиентам и их последнему кредитному рейтингу';


--
-- TOC entry 219 (class 1259 OID 16694)
-- Name: clients_client_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.clients_client_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.clients_client_id_seq OWNER TO postgres;

--
-- TOC entry 5010 (class 0 OID 0)
-- Dependencies: 219
-- Name: clients_client_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.clients_client_id_seq OWNED BY public.clients.client_id;


--
-- TOC entry 225 (class 1259 OID 16742)
-- Name: credit_ratings_credit_rating_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.credit_ratings_credit_rating_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.credit_ratings_credit_rating_id_seq OWNER TO postgres;

--
-- TOC entry 5011 (class 0 OID 0)
-- Dependencies: 225
-- Name: credit_ratings_credit_rating_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.credit_ratings_credit_rating_id_seq OWNED BY public.credit_ratings.credit_rating_id;


--
-- TOC entry 223 (class 1259 OID 16725)
-- Name: credits_credit_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.credits_credit_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.credits_credit_id_seq OWNER TO postgres;

--
-- TOC entry 5012 (class 0 OID 0)
-- Dependencies: 223
-- Name: credits_credit_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.credits_credit_id_seq OWNED BY public.credits.credit_id;


--
-- TOC entry 221 (class 1259 OID 16709)
-- Name: financial_data_financial_data_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.financial_data_financial_data_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.financial_data_financial_data_id_seq OWNER TO postgres;

--
-- TOC entry 5013 (class 0 OID 0)
-- Dependencies: 221
-- Name: financial_data_financial_data_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.financial_data_financial_data_id_seq OWNED BY public.financial_data.financial_data_id;


--
-- TOC entry 217 (class 1259 OID 16683)
-- Name: users_user_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.users_user_id_seq OWNER TO postgres;

--
-- TOC entry 5014 (class 0 OID 0)
-- Dependencies: 217
-- Name: users_user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_user_id_seq OWNED BY public.users.user_id;


--
-- TOC entry 4769 (class 2604 OID 16698)
-- Name: clients client_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clients ALTER COLUMN client_id SET DEFAULT nextval('public.clients_client_id_seq'::regclass);


--
-- TOC entry 4775 (class 2604 OID 16746)
-- Name: credit_ratings credit_rating_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.credit_ratings ALTER COLUMN credit_rating_id SET DEFAULT nextval('public.credit_ratings_credit_rating_id_seq'::regclass);


--
-- TOC entry 4773 (class 2604 OID 16729)
-- Name: credits credit_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.credits ALTER COLUMN credit_id SET DEFAULT nextval('public.credits_credit_id_seq'::regclass);


--
-- TOC entry 4771 (class 2604 OID 16713)
-- Name: financial_data financial_data_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.financial_data ALTER COLUMN financial_data_id SET DEFAULT nextval('public.financial_data_financial_data_id_seq'::regclass);


--
-- TOC entry 4767 (class 2604 OID 16687)
-- Name: users user_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN user_id SET DEFAULT nextval('public.users_user_id_seq'::regclass);


--
-- TOC entry 4794 (class 2606 OID 16701)
-- Name: clients clients_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clients
    ADD CONSTRAINT clients_pkey PRIMARY KEY (client_id);


--
-- TOC entry 4807 (class 2606 OID 16755)
-- Name: credit_ratings credit_ratings_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.credit_ratings
    ADD CONSTRAINT credit_ratings_pkey PRIMARY KEY (credit_rating_id);


--
-- TOC entry 4804 (class 2606 OID 16736)
-- Name: credits credits_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.credits
    ADD CONSTRAINT credits_pkey PRIMARY KEY (credit_id);


--
-- TOC entry 4801 (class 2606 OID 16719)
-- Name: financial_data financial_data_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.financial_data
    ADD CONSTRAINT financial_data_pkey PRIMARY KEY (financial_data_id);


--
-- TOC entry 4799 (class 2606 OID 16703)
-- Name: clients uq_clients_email; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clients
    ADD CONSTRAINT uq_clients_email UNIQUE (email);


--
-- TOC entry 4790 (class 2606 OID 16693)
-- Name: users users_login_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_login_key UNIQUE (login);


--
-- TOC entry 4792 (class 2606 OID 16691)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_id);


--
-- TOC entry 4795 (class 1259 OID 16766)
-- Name: idx_clients_created_by; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_clients_created_by ON public.clients USING btree (created_by);


--
-- TOC entry 4796 (class 1259 OID 16768)
-- Name: idx_clients_email; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_clients_email ON public.clients USING btree (email);


--
-- TOC entry 4797 (class 1259 OID 16767)
-- Name: idx_clients_full_name; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_clients_full_name ON public.clients USING btree (full_name);


--
-- TOC entry 4808 (class 1259 OID 16772)
-- Name: idx_credit_ratings_calculated_by; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_credit_ratings_calculated_by ON public.credit_ratings USING btree (calculated_by);


--
-- TOC entry 4809 (class 1259 OID 16773)
-- Name: idx_credit_ratings_calculation_date; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_credit_ratings_calculation_date ON public.credit_ratings USING btree (calculation_date);


--
-- TOC entry 4810 (class 1259 OID 16771)
-- Name: idx_credit_ratings_client_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_credit_ratings_client_id ON public.credit_ratings USING btree (client_id);


--
-- TOC entry 4805 (class 1259 OID 16770)
-- Name: idx_credits_client_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_credits_client_id ON public.credits USING btree (client_id);


--
-- TOC entry 4802 (class 1259 OID 16769)
-- Name: idx_financial_data_client_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_financial_data_client_id ON public.financial_data USING btree (client_id);


--
-- TOC entry 4811 (class 2606 OID 16704)
-- Name: clients fk_clients_created_by; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clients
    ADD CONSTRAINT fk_clients_created_by FOREIGN KEY (created_by) REFERENCES public.users(user_id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- TOC entry 4814 (class 2606 OID 16761)
-- Name: credit_ratings fk_credit_ratings_calculated_by; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.credit_ratings
    ADD CONSTRAINT fk_credit_ratings_calculated_by FOREIGN KEY (calculated_by) REFERENCES public.users(user_id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- TOC entry 4815 (class 2606 OID 16756)
-- Name: credit_ratings fk_credit_ratings_client; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.credit_ratings
    ADD CONSTRAINT fk_credit_ratings_client FOREIGN KEY (client_id) REFERENCES public.clients(client_id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4813 (class 2606 OID 16737)
-- Name: credits fk_credits_client; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.credits
    ADD CONSTRAINT fk_credits_client FOREIGN KEY (client_id) REFERENCES public.clients(client_id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4812 (class 2606 OID 16720)
-- Name: financial_data fk_financial_data_client; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.financial_data
    ADD CONSTRAINT fk_financial_data_client FOREIGN KEY (client_id) REFERENCES public.clients(client_id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4968 (class 0 OID 0)
-- Dependencies: 5
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE USAGE ON SCHEMA public FROM PUBLIC;


-- Completed on 2026-10-18 23:55:46

--
-- PostgreSQL database dump complete
--

\unrestrict Xz2ym3C1MrRRETSLN6MTAd3N2UDnQeNYjOv1ysD8KxXboocaUY8WhC9neJcaRTB

