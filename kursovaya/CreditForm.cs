using System;
using System.Data;
using System.Net;
using System.Windows.Forms;
using Npgsql;

namespace CreditRatingApp
{
    public partial class CreditForm : Form
    {
        private readonly int? creditId;

        public CreditForm()
        {
            InitializeComponent();

            creditId = null;

            this.Text = "Добавление кредита";
            lblTitle.Text = "Добавление кредита";
            btnSave.Text = "Добавить";

            LoadClients();
            InitializeStatuses();
        }

        public CreditForm(int creditId)
        {
            InitializeComponent();

            this.creditId = creditId;

            this.Text = "Редактирование кредита";
            lblTitle.Text = "Редактирование кредита";
            btnSave.Text = "Сохранить";

            LoadClients();
            InitializeStatuses();
            LoadCreditData();
        }

        private void LoadClients()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            client_id,
                            full_name
                        FROM clients
                        ORDER BY full_name;
                    ";

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection))
                    {
                        DataTable clientsTable = new DataTable();
                        adapter.Fill(clientsTable);

                        cmbClient.DataSource = clientsTable;
                        cmbClient.DisplayMember = "full_name";
                        cmbClient.ValueMember = "client_id";
                        cmbClient.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при загрузке списка клиентов:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void InitializeStatuses()
        {
            cmbStatus.Items.Clear();

            cmbStatus.Items.Add("Активный");
            cmbStatus.Items.Add("Закрыт");
            cmbStatus.Items.Add("Просрочен");

            cmbStatus.SelectedIndex = 0;
        }

        private void LoadCreditData()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT
                            client_id,
                            credit_amount,
                            remaining_debt,
                            monthly_payment,
                            status
                        FROM credits
                        WHERE credit_id = @credit_id;
                    ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@credit_id", creditId.Value);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cmbClient.SelectedValue = Convert.ToInt32(reader["client_id"]);
                                nudCreditAmount.Value = Convert.ToDecimal(reader["credit_amount"]);
                                nudRemainingDebt.Value = Convert.ToDecimal(reader["remaining_debt"]);
                                nudMonthlyPayment.Value = Convert.ToDecimal(reader["monthly_payment"]);
                                cmbStatus.SelectedItem = reader["status"].ToString();
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Кредит не найден.",
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
                    "Ошибка при загрузке данных кредита:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbClient.SelectedValue == null)
            {
                MessageBox.Show(
                    "Выберите клиента.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            decimal creditAmount = nudCreditAmount.Value;
            decimal remainingDebt = nudRemainingDebt.Value;
            decimal monthlyPayment = nudMonthlyPayment.Value;

            if (creditAmount <= 0)
            {
                MessageBox.Show(
                    "Сумма кредита должна быть больше нуля.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (remainingDebt < 0)
            {
                MessageBox.Show(
                    "Остаток задолженности не может быть отрицательным.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (remainingDebt > creditAmount)
            {
                MessageBox.Show(
                    "Остаток задолженности не может быть больше суммы кредита.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (monthlyPayment < 0)
            {
                MessageBox.Show(
                    "Ежемесячный платеж не может быть отрицательным.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string status = cmbStatus.Text.Trim();

            if (string.IsNullOrWhiteSpace(status))
            {
                MessageBox.Show(
                    "Выберите статус кредита.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int clientId = Convert.ToInt32(cmbClient.SelectedValue);

            try
            {
                if (creditId == null)
                {
                    AddCredit(clientId, creditAmount, remainingDebt, monthlyPayment, status);
                }
                else
                {
                    UpdateCredit(clientId, creditAmount, remainingDebt, monthlyPayment, status);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при сохранении кредита:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AddCredit(
            int clientId,
            decimal creditAmount,
            decimal remainingDebt,
            decimal monthlyPayment,
            string status)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
            {
                connection.Open();

                string query = @"
                    INSERT INTO credits
                        (client_id, credit_amount, remaining_debt, monthly_payment, status, created_at)
                    VALUES
                        (@client_id, @credit_amount, @remaining_debt, @monthly_payment, @status, NOW());
                ";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@client_id", clientId);
                    command.Parameters.AddWithValue("@credit_amount", creditAmount);
                    command.Parameters.AddWithValue("@remaining_debt", remainingDebt);
                    command.Parameters.AddWithValue("@monthly_payment", monthlyPayment);
                    command.Parameters.AddWithValue("@status", status);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show(
                "Кредит успешно добавлен.",
                "Сохранение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void UpdateCredit(
            int clientId,
            decimal creditAmount,
            decimal remainingDebt,
            decimal monthlyPayment,
            string status)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
            {
                connection.Open();

                string query = @"
                    UPDATE credits
                    SET
                        client_id = @client_id,
                        credit_amount = @credit_amount,
                        remaining_debt = @remaining_debt,
                        monthly_payment = @monthly_payment,
                        status = @status
                    WHERE credit_id = @credit_id;
                ";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@client_id", clientId);
                    command.Parameters.AddWithValue("@credit_amount", creditAmount);
                    command.Parameters.AddWithValue("@remaining_debt", remainingDebt);
                    command.Parameters.AddWithValue("@monthly_payment", monthlyPayment);
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@credit_id", creditId.Value);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show(
                "Данные кредита успешно изменены.",
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