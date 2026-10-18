using System;
using System.Windows.Forms;
using Npgsql;

namespace CreditRatingApp
{
    public partial class ClientForm : Form
    {
        private readonly int currentUserId;
        private readonly int? clientId;

        public ClientForm(int currentUserId)
        {
            InitializeComponent();

            this.currentUserId = currentUserId;
            this.clientId = null;

            this.Text = "Добавление клиента";
            lblTitle.Text = "Добавление клиента";
            btnSave.Text = "Добавить";
        }

        public ClientForm(int currentUserId, int clientId)
        {
            InitializeComponent();

            this.currentUserId = currentUserId;
            this.clientId = clientId;

            this.Text = "Редактирование клиента";
            lblTitle.Text = "Редактирование клиента";
            btnSave.Text = "Сохранить";

            LoadClientData();
        }

        private void LoadClientData()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            full_name,
                            birth_date,
                            phone,
                            email
                        FROM clients
                        WHERE client_id = @client_id;
                    ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@client_id", clientId.Value);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtFullName.Text = reader["full_name"].ToString();
                                dtpBirthDate.Value = Convert.ToDateTime(reader["birth_date"]);
                                txtPhone.Text = reader["phone"].ToString();
                                txtEmail.Text = reader["email"].ToString();
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Клиент не найден.",
                                    "Ошибка",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при загрузке данных клиента:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();
            DateTime birthDate = dtpBirthDate.Value.Date;

            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show(
                    "Введите ФИО клиента.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show(
                    "Введите телефон клиента.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show(
                    "Введите email клиента.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                if (clientId == null)
                {
                    AddClient(fullName, birthDate, phone, email);
                }
                else
                {
                    UpdateClient(fullName, birthDate, phone, email);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (PostgresException ex) when (ex.SqlState == "23505")
            {
                MessageBox.Show(
                    "Клиент с таким email уже существует.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при сохранении клиента:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AddClient(string fullName, DateTime birthDate, string phone, string email)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
            {
                connection.Open();

                string query = @"
                    INSERT INTO clients
                        (full_name, birth_date, phone, email, created_at, created_by)
                    VALUES
                        (@full_name, @birth_date, @phone, @email, NOW(), @created_by);
                ";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@full_name", fullName);
                    command.Parameters.AddWithValue("@birth_date", birthDate);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@created_by", currentUserId);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show(
                "Клиент успешно добавлен.",
                "Сохранение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void UpdateClient(string fullName, DateTime birthDate, string phone, string email)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
            {
                connection.Open();

                string query = @"
                    UPDATE clients
                    SET
                        full_name = @full_name,
                        birth_date = @birth_date,
                        phone = @phone,
                        email = @email
                    WHERE client_id = @client_id;
                ";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@full_name", fullName);
                    command.Parameters.AddWithValue("@birth_date", birthDate);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@client_id", clientId.Value);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show(
                "Данные клиента успешно изменены.",
                "Сохранение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}