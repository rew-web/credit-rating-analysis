using System;
using System.Windows.Forms;
using Npgsql;

namespace CreditRatingApp
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show(
                    "Заполните все поля.",
                    "Ошибка регистрации",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show(
                    "Пароли не совпадают.",
                    "Ошибка регистрации",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (password.Length < 4)
            {
                MessageBox.Show(
                    "Пароль должен содержать минимум 4 символа.",
                    "Ошибка регистрации",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
                {
                    connection.Open();

                    if (IsLoginExists(connection, login))
                    {
                        MessageBox.Show(
                            "Пользователь с таким логином уже существует.",
                            "Ошибка регистрации",
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
                        command.Parameters.AddWithValue("@role", "Пользователь");

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Регистрация успешно выполнена. Теперь вы можете войти в систему.",
                    "Регистрация",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при регистрации пользователя:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool IsLoginExists(NpgsqlConnection connection, string login)
        {
            string query = @"
                SELECT COUNT(*)
                FROM users
                WHERE login = @login;
            ";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@login", login);

                long count = (long)command.ExecuteScalar();
                return count > 0;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool showPassword = chkShowPassword.Checked;

            txtPassword.UseSystemPasswordChar = !showPassword;
            txtConfirmPassword.UseSystemPasswordChar = !showPassword;
        }
    }
}