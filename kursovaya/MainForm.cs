using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace CreditRatingApp
{
    public partial class MainForm : Form
    {
        private readonly int currentUserId;
        private readonly string currentUserLogin;
        private readonly string currentUserFullName;
        private readonly string currentUserRole;

        public MainForm(int userId, string login, string fullName, string role)
        {
            InitializeComponent();

            currentUserId = userId;
            currentUserLogin = login;
            currentUserFullName = fullName;
            currentUserRole = role;

            lblCurrentUser.Text = $"Пользователь: {currentUserFullName} | Роль: {currentUserRole}";
            Text = $"АИС анализа кредитного рейтинга - {currentUserFullName}";

            InitializeSearchControls();
            InitializeUserRoleControls();
            InitializeReportControls();

            ApplyRolePermissions();
            LoadAllData();
        }

        private void ApplyRolePermissions()
        {
            if (currentUserRole != "Администратор")
            {
                tabMain.TabPages.Remove(tabUsers);
                tabMain.TabPages.Remove(tabReports);
            }
        }

        private void InitializeSearchControls()
        {
            cmbClientSearchField.Items.Clear();
            cmbClientSearchField.Items.Add("ФИО");
            cmbClientSearchField.Items.Add("Телефон");
            cmbClientSearchField.Items.Add("Email");
            cmbClientSearchField.Items.Add("ID");
            cmbClientSearchField.SelectedIndex = 0;
        }

        private void InitializeUserRoleControls()
        {
            cmbUserRole.Items.Clear();
            cmbUserRole.Items.Add("Пользователь");
            cmbUserRole.Items.Add("Администратор");
            cmbUserRole.SelectedIndex = 0;
        }

        private void InitializeReportControls()
        {
            cmbReportType.Items.Clear();
            cmbReportType.Items.Add("Отчет по клиентам");
            cmbReportType.Items.Add("Отчет по кредитным рейтингам");
            cmbReportType.Items.Add("Отчет по клиентам с низким рейтингом");
            cmbReportType.SelectedIndex = 0;
        }

        private void LoadAllData()
        {
            LoadClients();
            LoadFinancialData();
            LoadCredits();
            LoadCreditRatings();
            LoadRatingClients();

            if (currentUserRole == "Администратор")
            {
                LoadUsers();
            }
        }

        private DataTable ExecuteSelectQuery(string query)
        {
            DataTable table = new DataTable();

            using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
            {
                connection.Open();

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection))
                {
                    adapter.Fill(table);
                }
            }

            return table;
        }

        private DataTable ExecuteParameterizedSelectQuery(string query, params NpgsqlParameter[] parameters)
        {
            DataTable table = new DataTable();

            using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }

            return table;
        }

        private void ShowDatabaseError(Exception ex)
        {
            MessageBox.Show(
                "Ошибка при работе с базой данных:\n" + ex.Message,
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void LoadClients()
        {
            try
            {
                string query = @"
                    SELECT
                        c.client_id AS ""ID"",
                        c.full_name AS ""ФИО"",
                        c.birth_date AS ""Дата рождения"",
                        c.phone AS ""Телефон"",
                        c.email AS ""Email"",
                        u.full_name AS ""Кто добавил"",
                        c.created_at AS ""Дата создания""
                    FROM clients c
                    LEFT JOIN users u ON c.created_by = u.user_id
                    ORDER BY c.client_id;
                ";

                dgvClients.DataSource = ExecuteSelectQuery(query);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void LoadFinancialData()
        {
            try
            {
                string query = @"
                    SELECT
                        fd.financial_data_id AS ""ID"",
                        c.full_name AS ""Клиент"",
                        fd.monthly_income AS ""Ежемесячный доход"",
                        fd.monthly_expenses AS ""Ежемесячные расходы"",
                        fd.employment_status AS ""Статус занятости"",
                        fd.updated_at AS ""Дата обновления""
                    FROM financial_data fd
                    JOIN clients c ON fd.client_id = c.client_id
                    ORDER BY fd.financial_data_id;
                ";

                dgvFinancialData.DataSource = ExecuteSelectQuery(query);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void LoadCredits()
        {
            try
            {
                string query = @"
                    SELECT
                        cr.credit_id AS ""ID"",
                        c.full_name AS ""Клиент"",
                        cr.credit_amount AS ""Сумма кредита"",
                        cr.remaining_debt AS ""Остаток задолженности"",
                        cr.monthly_payment AS ""Ежемесячный платеж"",
                        cr.status AS ""Статус"",
                        cr.created_at AS ""Дата создания""
                    FROM credits cr
                    JOIN clients c ON cr.client_id = c.client_id
                    ORDER BY cr.credit_id;
                ";

                dgvCredits.DataSource = ExecuteSelectQuery(query);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void LoadCreditRatings()
        {
            try
            {
                string query = @"
                    SELECT
                        r.credit_rating_id AS ""ID"",
                        c.full_name AS ""Клиент"",
                        r.score AS ""Балл"",
                        r.rating_level AS ""Уровень рейтинга"",
                        r.debt_load_ratio AS ""Долговая нагрузка"",
                        r.decision AS ""Решение"",
                        r.comment AS ""Комментарий"",
                        r.calculation_date AS ""Дата расчета"",
                        u.full_name AS ""Кто рассчитал""
                    FROM credit_ratings r
                    JOIN clients c ON r.client_id = c.client_id
                    LEFT JOIN users u ON r.calculated_by = u.user_id
                    ORDER BY r.credit_rating_id DESC;
                ";

                dgvCreditRatings.DataSource = ExecuteSelectQuery(query);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void LoadUsers()
        {
            try
            {
                string query = @"
                    SELECT
                        user_id AS ""ID"",
                        login AS ""Логин"",
                        full_name AS ""ФИО"",
                        role AS ""Роль"",
                        created_at AS ""Дата создания""
                    FROM users
                    ORDER BY user_id;
                ";

                dgvUsers.DataSource = ExecuteSelectQuery(query);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void LoadRatingClients()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT client_id, full_name
                        FROM clients
                        ORDER BY full_name;
                    ";

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        cmbRatingClient.DataSource = table;
                        cmbRatingClient.DisplayMember = "full_name";
                        cmbRatingClient.ValueMember = "client_id";
                        cmbRatingClient.SelectedIndex = table.Rows.Count > 0 ? 0 : -1;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllData();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // =========================
        // Клиенты
        // =========================

        private void btnClientAdd_Click(object sender, EventArgs e)
        {
            ClientForm form = new ClientForm(currentUserId);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadClients();
                LoadRatingClients();
            }
        }

        private void btnClientEdit_Click(object sender, EventArgs e)
        {
            if (dgvClients.CurrentRow == null)
            {
                MessageBox.Show("Выберите клиента для редактирования.");
                return;
            }

            int clientId = Convert.ToInt32(dgvClients.CurrentRow.Cells["ID"].Value);

            ClientForm form = new ClientForm(currentUserId, clientId);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadClients();
                LoadRatingClients();
            }
        }

        private void btnClientDelete_Click(object sender, EventArgs e)
        {
            if (dgvClients.CurrentRow == null)
            {
                MessageBox.Show("Выберите клиента для удаления.");
                return;
            }

            int clientId = Convert.ToInt32(dgvClients.CurrentRow.Cells["ID"].Value);
            string fullName = dgvClients.CurrentRow.Cells["ФИО"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Удалить клиента \"{fullName}\"?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        DELETE FROM clients
                        WHERE client_id = @client_id;
                    ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@client_id", clientId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Клиент успешно удален.");

                LoadClients();
                LoadFinancialData();
                LoadCredits();
                LoadCreditRatings();
                LoadRatingClients();
                rtbClientFullInfo.Clear();
            }
            catch (PostgresException ex) when (ex.SqlState == "23503")
            {
                MessageBox.Show(
                    "Нельзя удалить клиента, потому что с ним связаны финансовые данные, кредиты или рейтинги.",
                    "Ошибка удаления",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void btnClientSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtClientSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show(
                    "Введите значение для поиска клиента.",
                    "Поиск клиента",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable clients = FindClients(searchText);

                if (clients.Rows.Count == 0)
                {
                    rtbClientFullInfo.Text = "Клиенты по заданным параметрам не найдены.";
                    return;
                }

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("РЕЗУЛЬТАТЫ ПОИСКА");
                sb.AppendLine($"Параметр поиска: {cmbClientSearchField.Text}");
                sb.AppendLine($"Значение: {searchText}");
                sb.AppendLine($"Дата поиска: {DateTime.Now}");
                sb.AppendLine(new string('=', 90));
                sb.AppendLine();

                foreach (DataRow row in clients.Rows)
                {
                    int clientId = Convert.ToInt32(row["client_id"]);

                    AppendClientInfo(sb, row);
                    AppendClientFinancialData(sb, clientId);
                    AppendClientCredits(sb, clientId);
                    AppendClientRatings(sb, clientId);

                    sb.AppendLine(new string('=', 90));
                    sb.AppendLine();
                }

                rtbClientFullInfo.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private DataTable FindClients(string searchText)
        {
            string selectedField = cmbClientSearchField.Text;

            string condition;

            if (selectedField == "ФИО")
            {
                condition = "c.full_name ILIKE '%' || @search || '%'";
            }
            else if (selectedField == "Телефон")
            {
                condition = "c.phone ILIKE '%' || @search || '%'";
            }
            else if (selectedField == "Email")
            {
                condition = "c.email ILIKE '%' || @search || '%'";
            }
            else
            {
                condition = "CAST(c.client_id AS TEXT) = @search";
            }

            string query = $@"
                SELECT
                    c.client_id,
                    c.full_name,
                    c.birth_date,
                    c.phone,
                    c.email,
                    c.created_at,
                    u.full_name AS created_by_name
                FROM clients c
                LEFT JOIN users u ON c.created_by = u.user_id
                WHERE {condition}
                ORDER BY c.full_name;
            ";

            return ExecuteParameterizedSelectQuery(
                query,
                new NpgsqlParameter("@search", searchText));
        }

        private void btnClientSearchClear_Click(object sender, EventArgs e)
        {
            txtClientSearch.Clear();
            rtbClientFullInfo.Clear();
            cmbClientSearchField.SelectedIndex = 0;
            txtClientSearch.Focus();
        }

        private void AppendClientInfo(StringBuilder sb, DataRow row)
        {
            sb.AppendLine("ОСНОВНАЯ ИНФОРМАЦИЯ О КЛИЕНТЕ");
            sb.AppendLine($"ID клиента: {row["client_id"]}");
            sb.AppendLine($"ФИО: {row["full_name"]}");
            sb.AppendLine($"Дата рождения: {Convert.ToDateTime(row["birth_date"]).ToShortDateString()}");
            sb.AppendLine($"Телефон: {row["phone"]}");
            sb.AppendLine($"Email: {row["email"]}");
            sb.AppendLine($"Кто добавил: {row["created_by_name"]}");
            sb.AppendLine($"Дата создания записи: {row["created_at"]}");
            sb.AppendLine();
        }

        private void AppendClientFinancialData(StringBuilder sb, int clientId)
        {
            DataTable table = ExecuteParameterizedSelectQuery(@"
                SELECT
                    monthly_income,
                    monthly_expenses,
                    employment_status,
                    updated_at
                FROM financial_data
                WHERE client_id = @client_id
                ORDER BY updated_at DESC;
            ", new NpgsqlParameter("@client_id", clientId));

            sb.AppendLine("ФИНАНСОВЫЕ ДАННЫЕ");

            if (table.Rows.Count == 0)
            {
                sb.AppendLine("Финансовые данные отсутствуют.");
                sb.AppendLine();
                return;
            }

            int number = 1;

            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine($"Запись №{number}");
                sb.AppendLine($"Ежемесячный доход: {row["monthly_income"]}");
                sb.AppendLine($"Ежемесячные расходы: {row["monthly_expenses"]}");
                sb.AppendLine($"Статус занятости: {row["employment_status"]}");
                sb.AppendLine($"Дата обновления: {row["updated_at"]}");
                sb.AppendLine();

                number++;
            }
        }

        private void AppendClientCredits(StringBuilder sb, int clientId)
        {
            DataTable table = ExecuteParameterizedSelectQuery(@"
                SELECT
                    credit_amount,
                    remaining_debt,
                    monthly_payment,
                    status,
                    created_at
                FROM credits
                WHERE client_id = @client_id
                ORDER BY created_at DESC;
            ", new NpgsqlParameter("@client_id", clientId));

            sb.AppendLine("КРЕДИТНЫЕ ДАННЫЕ");

            if (table.Rows.Count == 0)
            {
                sb.AppendLine("Кредитные данные отсутствуют.");
                sb.AppendLine();
                return;
            }

            int number = 1;

            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine($"Кредит №{number}");
                sb.AppendLine($"Сумма кредита: {row["credit_amount"]}");
                sb.AppendLine($"Остаток задолженности: {row["remaining_debt"]}");
                sb.AppendLine($"Ежемесячный платеж: {row["monthly_payment"]}");
                sb.AppendLine($"Статус: {row["status"]}");
                sb.AppendLine($"Дата создания: {row["created_at"]}");
                sb.AppendLine();

                number++;
            }
        }

        private void AppendClientRatings(StringBuilder sb, int clientId)
        {
            DataTable table = ExecuteParameterizedSelectQuery(@"
                SELECT
                    score,
                    rating_level,
                    debt_load_ratio,
                    decision,
                    comment,
                    calculation_date
                FROM credit_ratings
                WHERE client_id = @client_id
                ORDER BY calculation_date DESC;
            ", new NpgsqlParameter("@client_id", clientId));

            sb.AppendLine("РЕЗУЛЬТАТЫ РАСЧЕТА КРЕДИТНОГО РЕЙТИНГА");

            if (table.Rows.Count == 0)
            {
                sb.AppendLine("Расчеты кредитного рейтинга отсутствуют.");
                sb.AppendLine();
                return;
            }

            int number = 1;

            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine($"Расчет №{number}");
                sb.AppendLine($"Балл: {row["score"]}");
                sb.AppendLine($"Уровень рейтинга: {row["rating_level"]}");
                sb.AppendLine($"Долговая нагрузка: {row["debt_load_ratio"]}%");
                sb.AppendLine($"Решение: {row["decision"]}");
                sb.AppendLine($"Комментарий: {row["comment"]}");
                sb.AppendLine($"Дата расчета: {row["calculation_date"]}");
                sb.AppendLine();

                number++;
            }
        }

        // =========================
        // Финансовые данные
        // =========================

        private void btnFinancialDataAdd_Click(object sender, EventArgs e)
        {
            FinancialDataForm form = new FinancialDataForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadFinancialData();
            }
        }

        private void btnFinancialDataEdit_Click(object sender, EventArgs e)
        {
            if (dgvFinancialData.CurrentRow == null)
            {
                MessageBox.Show("Выберите финансовую запись для редактирования.");
                return;
            }

            int financialDataId = Convert.ToInt32(dgvFinancialData.CurrentRow.Cells["ID"].Value);

            FinancialDataForm form = new FinancialDataForm(financialDataId);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadFinancialData();
            }
        }

        private void btnFinancialDataDelete_Click(object sender, EventArgs e)
        {
            if (dgvFinancialData.CurrentRow == null)
            {
                MessageBox.Show("Выберите финансовую запись для удаления.");
                return;
            }

            int financialDataId = Convert.ToInt32(dgvFinancialData.CurrentRow.Cells["ID"].Value);

            DialogResult result = MessageBox.Show(
                "Удалить выбранную финансовую запись?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        DELETE FROM financial_data
                        WHERE financial_data_id = @financial_data_id;
                    ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@financial_data_id", financialDataId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Финансовая запись успешно удалена.");
                LoadFinancialData();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        // =========================
        // Кредиты
        // =========================

        private void btnCreditAdd_Click(object sender, EventArgs e)
        {
            CreditForm form = new CreditForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadCredits();
            }
        }

        private void btnCreditEdit_Click(object sender, EventArgs e)
        {
            if (dgvCredits.CurrentRow == null)
            {
                MessageBox.Show("Выберите кредит для редактирования.");
                return;
            }

            int creditId = Convert.ToInt32(dgvCredits.CurrentRow.Cells["ID"].Value);

            CreditForm form = new CreditForm(creditId);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadCredits();
            }
        }

        private void btnCreditDelete_Click(object sender, EventArgs e)
        {
            if (dgvCredits.CurrentRow == null)
            {
                MessageBox.Show("Выберите кредит для удаления.");
                return;
            }

            int creditId = Convert.ToInt32(dgvCredits.CurrentRow.Cells["ID"].Value);

            DialogResult result = MessageBox.Show(
                "Удалить выбранный кредит?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        DELETE FROM credits
                        WHERE credit_id = @credit_id;
                    ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@credit_id", creditId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Кредит успешно удален.");
                LoadCredits();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        // =========================
        // Рейтинг
        // =========================

        private void btnCalculateRating_Click(object sender, EventArgs e)
        {
            if (cmbRatingClient.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента для расчета рейтинга.");
                return;
            }

            int clientId = Convert.ToInt32(cmbRatingClient.SelectedValue);

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT *
                        FROM calculate_credit_rating(@client_id, @user_id);
                    ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@client_id", clientId);
                        command.Parameters.AddWithValue("@user_id", currentUserId);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int score = Convert.ToInt32(reader["score"]);
                                string ratingLevel = reader["rating_level"].ToString();
                                decimal debtLoadRatio = Convert.ToDecimal(reader["debt_load_ratio"]);
                                string decision = reader["decision"].ToString();
                                string comment = reader["comment"].ToString();

                                MessageBox.Show(
                                    $"Рейтинг рассчитан.\n\n" +
                                    $"Балл: {score}\n" +
                                    $"Уровень: {ratingLevel}\n" +
                                    $"Долговая нагрузка: {debtLoadRatio}%\n" +
                                    $"Решение: {decision}\n\n" +
                                    $"{comment}",
                                    "Расчет рейтинга",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                        }
                    }
                }

                LoadCreditRatings();
            }
            catch (PostgresException ex)
            {
                MessageBox.Show(
                    "Ошибка при расчете рейтинга:\n" + ex.MessageText,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        // =========================
        // Пользователи
        // =========================

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                return;
            }

            txtUserLogin.Text = dgvUsers.CurrentRow.Cells["Логин"].Value.ToString();
            txtUserFullName.Text = dgvUsers.CurrentRow.Cells["ФИО"].Value.ToString();
            cmbUserRole.SelectedItem = dgvUsers.CurrentRow.Cells["Роль"].Value.ToString();
            txtUserPassword.Clear();
            txtUserConfirmPassword.Clear();
        }

        private void btnUserClear_Click(object sender, EventArgs e)
        {
            ClearUserFields();
        }

        private void ClearUserFields()
        {
            txtUserLogin.Clear();
            txtUserFullName.Clear();
            txtUserPassword.Clear();
            txtUserConfirmPassword.Clear();
            cmbUserRole.SelectedIndex = 0;
            txtUserLogin.Focus();
        }

        private bool ValidateUserFields(bool isNewUser)
        {
            if (string.IsNullOrWhiteSpace(txtUserLogin.Text) ||
                string.IsNullOrWhiteSpace(txtUserFullName.Text) ||
                cmbUserRole.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Введите логин, ФИО и роль пользователя.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (isNewUser && string.IsNullOrWhiteSpace(txtUserPassword.Text))
            {
                MessageBox.Show(
                    "Введите пароль для нового пользователя.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtUserPassword.Text) ||
                !string.IsNullOrWhiteSpace(txtUserConfirmPassword.Text))
            {
                if (txtUserPassword.Text != txtUserConfirmPassword.Text)
                {
                    MessageBox.Show(
                        "Пароли не совпадают.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                if (txtUserPassword.Text.Length < 4)
                {
                    MessageBox.Show(
                        "Пароль должен содержать минимум 4 символа.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private bool IsLoginExists(NpgsqlConnection connection, string login, int? editedUserId)
        {
            string query;

            if (editedUserId.HasValue)
            {
                query = @"
            SELECT COUNT(*)
            FROM users
            WHERE login = @login
              AND user_id <> @user_id;
        ";
            }
            else
            {
                query = @"
            SELECT COUNT(*)
            FROM users
            WHERE login = @login;
        ";
            }

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@login", login);

                if (editedUserId.HasValue)
                {
                    command.Parameters.AddWithValue("@user_id", editedUserId.Value);
                }

                long count = (long)command.ExecuteScalar();
                return count > 0;
            }
        }
        private void btnUserAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateUserFields(true))
            {
                return;
            }

            string login = txtUserLogin.Text.Trim();
            string fullName = txtUserFullName.Text.Trim();
            string password = txtUserPassword.Text.Trim();
            string role = cmbUserRole.Text.Trim();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
                {
                    connection.Open();

                    if (IsLoginExists(connection, login, null))
                    {
                        MessageBox.Show(
                            "Пользователь с таким логином уже существует.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    string query = @"
                        INSERT INTO users
                            (login, password_hash, full_name, role, created_at)
                        VALUES
                            (@login, @password_hash, @full_name, @role, NOW());
                    ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password_hash", password);
                        command.Parameters.AddWithValue("@full_name", fullName);
                        command.Parameters.AddWithValue("@role", role);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Пользователь успешно добавлен.");
                ClearUserFields();
                LoadUsers();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void btnUserEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show("Выберите пользователя для изменения.");
                return;
            }

            if (!ValidateUserFields(false))
            {
                return;
            }

            int userId = Convert.ToInt32(dgvUsers.CurrentRow.Cells["ID"].Value);
            string login = txtUserLogin.Text.Trim();
            string fullName = txtUserFullName.Text.Trim();
            string password = txtUserPassword.Text.Trim();
            string role = cmbUserRole.Text.Trim();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
                {
                    connection.Open();

                    if (IsLoginExists(connection, login, userId))
                    {
                        MessageBox.Show(
                            "Пользователь с таким логином уже существует.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    string query;

                    if (string.IsNullOrWhiteSpace(password))
                    {
                        query = @"
                            UPDATE users
                            SET
                                login = @login,
                                full_name = @full_name,
                                role = @role
                            WHERE user_id = @user_id;
                        ";
                    }
                    else
                    {
                        query = @"
                            UPDATE users
                            SET
                                login = @login,
                                password_hash = @password_hash,
                                full_name = @full_name,
                                role = @role
                            WHERE user_id = @user_id;
                        ";
                    }

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@full_name", fullName);
                        command.Parameters.AddWithValue("@role", role);
                        command.Parameters.AddWithValue("@user_id", userId);

                        if (!string.IsNullOrWhiteSpace(password))
                        {
                            command.Parameters.AddWithValue("@password_hash", password);
                        }

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Данные пользователя успешно изменены.");
                ClearUserFields();
                LoadUsers();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void btnUserDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show("Выберите пользователя для удаления.");
                return;
            }

            int userId = Convert.ToInt32(dgvUsers.CurrentRow.Cells["ID"].Value);
            string login = dgvUsers.CurrentRow.Cells["Логин"].Value.ToString();

            if (userId == currentUserId)
            {
                MessageBox.Show(
                    "Нельзя удалить пользователя, под которым выполнен текущий вход.",
                    "Ошибка удаления",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Удалить пользователя \"{login}\"?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        DELETE FROM users
                        WHERE user_id = @user_id;
                    ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@user_id", userId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Пользователь успешно удален.");
                ClearUserFields();
                LoadUsers();
            }
            catch (PostgresException ex) when (ex.SqlState == "23503")
            {
                MessageBox.Show(
                    "Нельзя удалить пользователя, потому что с ним связаны клиенты или расчеты рейтинга.",
                    "Ошибка удаления",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        // =========================
        // Отчеты
        // =========================

        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            if (cmbReportType.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите тип отчета.");
                return;
            }
            
            string reportType = cmbReportType.SelectedItem.ToString();

            try
            {
                if (reportType == "Отчет по клиентам")
                {
                    CreateClientsReport();
                }
                else if (reportType == "Отчет по кредитным рейтингам")
                {
                    CreateRatingsReport();
                }
                else if (reportType == "Отчет по клиентам с низким рейтингом")
                {
                    CreateLowRatingReport();
                }
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void CreateClientsReport()
        {
            DataTable table = ExecuteSelectQuery(@"
                SELECT
                    client_id,
                    full_name,
                    birth_date,
                    phone,
                    email,
                    created_at
                FROM clients
                ORDER BY client_id;
            ");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("ОТЧЕТ ПО КЛИЕНТАМ");
            sb.AppendLine($"Дата формирования: {DateTime.Now}");
            sb.AppendLine(new string('=', 70));
            sb.AppendLine();

            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine($"ID: {row["client_id"]}");
                sb.AppendLine($"ФИО: {row["full_name"]}");
                sb.AppendLine($"Дата рождения: {Convert.ToDateTime(row["birth_date"]).ToShortDateString()}");
                sb.AppendLine($"Телефон: {row["phone"]}");
                sb.AppendLine($"Email: {row["email"]}");
                sb.AppendLine($"Дата создания: {row["created_at"]}");
                sb.AppendLine(new string('-', 70));
            }

            rtbReport.Text = sb.ToString();
        }

        private void CreateRatingsReport()
        {
            DataTable table = ExecuteSelectQuery(@"
        SELECT
            latest.full_name,
            latest.score,
            latest.rating_level,
            latest.debt_load_ratio,
            latest.decision,
            latest.comment,
            latest.calculation_date
        FROM (
            SELECT DISTINCT ON (r.client_id)
                r.client_id,
                c.full_name,
                r.score,
                r.rating_level,
                r.debt_load_ratio,
                r.decision,
                r.comment,
                r.calculation_date
            FROM credit_ratings r
            JOIN clients c ON r.client_id = c.client_id
            ORDER BY r.client_id, r.calculation_date DESC, r.credit_rating_id DESC
        ) latest
        ORDER BY latest.full_name;
    ");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("ОТЧЕТ ПО АКТУАЛЬНЫМ КРЕДИТНЫМ РЕЙТИНГАМ");
            sb.AppendLine($"Дата формирования: {DateTime.Now}");
            sb.AppendLine(new string('=', 70));
            sb.AppendLine();

            if (table.Rows.Count == 0)
            {
                sb.AppendLine("Расчеты кредитного рейтинга отсутствуют.");
            }

            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine($"Клиент: {row["full_name"]}");
                sb.AppendLine($"Балл: {row["score"]}");
                sb.AppendLine($"Уровень: {row["rating_level"]}");
                sb.AppendLine($"Долговая нагрузка: {row["debt_load_ratio"]}%");
                sb.AppendLine($"Решение: {row["decision"]}");
                sb.AppendLine($"Комментарий: {row["comment"]}");
                sb.AppendLine($"Дата последнего расчета: {row["calculation_date"]}");
                sb.AppendLine(new string('-', 70));
            }

            rtbReport.Text = sb.ToString();
        }
        private void CreateLowRatingReport()
        {
            DataTable table = ExecuteSelectQuery(@"
        SELECT
            latest.full_name,
            latest.phone,
            latest.email,
            latest.score,
            latest.rating_level,
            latest.debt_load_ratio,
            latest.decision,
            latest.comment,
            latest.calculation_date
        FROM (
            SELECT DISTINCT ON (r.client_id)
                r.client_id,
                c.full_name,
                c.phone,
                c.email,
                r.score,
                r.rating_level,
                r.debt_load_ratio,
                r.decision,
                r.comment,
                r.calculation_date
            FROM credit_ratings r
            JOIN clients c ON r.client_id = c.client_id
            ORDER BY r.client_id, r.calculation_date DESC, r.credit_rating_id DESC
        ) latest
        WHERE latest.rating_level = 'Низкий'
           OR latest.score < 50
           OR latest.decision ILIKE '%отказ%'
        ORDER BY latest.calculation_date DESC;
    ");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("ОТЧЕТ ПО КЛИЕНТАМ С АКТУАЛЬНЫМ НИЗКИМ РЕЙТИНГОМ");
            sb.AppendLine($"Дата формирования: {DateTime.Now}");
            sb.AppendLine(new string('=', 70));
            sb.AppendLine();

            if (table.Rows.Count == 0)
            {
                sb.AppendLine("Клиенты с актуальным низким рейтингом не найдены.");
            }

            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine($"Клиент: {row["full_name"]}");
                sb.AppendLine($"Телефон: {row["phone"]}");
                sb.AppendLine($"Email: {row["email"]}");
                sb.AppendLine($"Балл: {row["score"]}");
                sb.AppendLine($"Уровень: {row["rating_level"]}");
                sb.AppendLine($"Долговая нагрузка: {row["debt_load_ratio"]}%");
                sb.AppendLine($"Решение: {row["decision"]}");
                sb.AppendLine($"Комментарий: {row["comment"]}");
                sb.AppendLine($"Дата последнего расчета: {row["calculation_date"]}");
                sb.AppendLine(new string('-', 70));
            }

            rtbReport.Text = sb.ToString();
        }
    }
}