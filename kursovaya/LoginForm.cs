using System;
using System.Windows.Forms;
using kursovaya;
using Npgsql;

namespace CreditRatingApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Введите логин и пароль.",
                    "Ошибка входа",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            user_id,
                            login,
                            password_hash,
                            full_name,
                            role
                        FROM users
                        WHERE login = @login;
                    ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show(
                                    "Пользователь с таким логином не найден.",
                                    "Ошибка входа",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                                return;
                            }

                            int userId = reader.GetInt32(reader.GetOrdinal("user_id"));
                            string userLogin = reader.GetString(reader.GetOrdinal("login"));
                            string passwordHash = reader.GetString(reader.GetOrdinal("password_hash"));
                            string fullName = reader.GetString(reader.GetOrdinal("full_name"));
                            string role = reader.GetString(reader.GetOrdinal("role"));

                            // ВАЖНО:
                            // Сейчас проверка простая: введенный пароль сравнивается со значением из password_hash.
                            // Это подойдет, если ты пока хранишь пароль обычным текстом.
                            // Позже можно заменить на BCrypt.
                            if (password != passwordHash)
                            {
                                MessageBox.Show(
                                    "Неверный пароль.",
                                    "Ошибка входа",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                                return;
                            }

                            MessageBox.Show(
                                $"Добро пожаловать, {fullName}!",
                                "Авторизация",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            MainForm mainForm = new MainForm(userId, userLogin, fullName, role);
                            mainForm.Show();

                            this.Hide();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка подключения к базе данных:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}