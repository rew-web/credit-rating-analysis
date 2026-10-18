using System;
using System.Data;
using System.Net;
using System.Windows.Forms;
using Npgsql;

namespace CreditRatingApp
{
    public partial class FinancialDataForm : Form
    {
        private readonly int? financialDataId;

        public FinancialDataForm()
        {
            InitializeComponent();

            financialDataId = null;

            this.Text = "Добавление финансовых данных";
            lblTitle.Text = "Добавление финансовых данных";
            btnSave.Text = "Добавить";

            LoadClients();
            InitializeEmploymentStatuses();
        }

        public FinancialDataForm(int financialDataId)
        {
            InitializeComponent();

            this.financialDataId = financialDataId;

            this.Text = "Редактирование финансовых данных";
            lblTitle.Text = "Редактирование финансовых данных";
            btnSave.Text = "Сохранить";

            LoadClients();
            InitializeEmploymentStatuses();
            LoadFinancialData();
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

        private void InitializeEmploymentStatuses()
        {
            cmbEmploymentStatus.Items.Clear();

            cmbEmploymentStatus.Items.Add("Работает");
            cmbEmploymentStatus.Items.Add("Не работает");
            cmbEmploymentStatus.Items.Add("Самозанятый");
            cmbEmploymentStatus.Items.Add("ИП");
            cmbEmploymentStatus.Items.Add("Студент");
            cmbEmploymentStatus.Items.Add("Пенсионер");

            cmbEmploymentStatus.SelectedIndex = 0;
        }

        private void LoadFinancialData()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT
                            client_id,
                            monthly_income,
                            monthly_expenses,
                            employment_status
                        FROM financial_data
                        WHERE financial_data_id = @financial_data_id;
                    ";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@financial_data_id", financialDataId.Value);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cmbClient.SelectedValue = Convert.ToInt32(reader["client_id"]);
                                nudMonthlyIncome.Value = Convert.ToDecimal(reader["monthly_income"]);
                                nudMonthlyExpenses.Value = Convert.ToDecimal(reader["monthly_expenses"]);
                                cmbEmploymentStatus.SelectedItem = reader["employment_status"].ToString();
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Финансовые данные не найдены.",
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
                    "Ошибка при загрузке финансовых данных:\n" + ex.Message,
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

            decimal monthlyIncome = nudMonthlyIncome.Value;
            decimal monthlyExpenses = nudMonthlyExpenses.Value;

            if (monthlyIncome <= 0)
            {
                MessageBox.Show(
                    "Ежемесячный доход должен быть больше нуля.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (monthlyExpenses < 0)
            {
                MessageBox.Show(
                    "Ежемесячные расходы не могут быть отрицательными.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (monthlyExpenses > monthlyIncome)
            {
                DialogResult result = MessageBox.Show(
                    "Расходы больше дохода. Все равно сохранить данные?",
                    "Предупреждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            string employmentStatus = cmbEmploymentStatus.Text.Trim();

            if (string.IsNullOrWhiteSpace(employmentStatus))
            {
                MessageBox.Show(
                    "Выберите статус занятости.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int clientId = Convert.ToInt32(cmbClient.SelectedValue);

            try
            {
                if (financialDataId == null)
                {
                    AddFinancialData(clientId, monthlyIncome, monthlyExpenses, employmentStatus);
                }
                else
                {
                    UpdateFinancialData(clientId, monthlyIncome, monthlyExpenses, employmentStatus);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка при сохранении финансовых данных:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AddFinancialData(
            int clientId,
            decimal monthlyIncome,
            decimal monthlyExpenses,
            string employmentStatus)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
            {
                connection.Open();

                string query = @"
                    INSERT INTO financial_data
                        (client_id, monthly_income, monthly_expenses, employment_status, updated_at)
                    VALUES
                        (@client_id, @monthly_income, @monthly_expenses, @employment_status, NOW());
                ";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@client_id", clientId);
                    command.Parameters.AddWithValue("@monthly_income", monthlyIncome);
                    command.Parameters.AddWithValue("@monthly_expenses", monthlyExpenses);
                    command.Parameters.AddWithValue("@employment_status", employmentStatus);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show(
                "Финансовые данные успешно добавлены.",
                "Сохранение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void UpdateFinancialData(
            int clientId,
            decimal monthlyIncome,
            decimal monthlyExpenses,
            string employmentStatus)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(DbConfig.ConnectionString))
            {
                connection.Open();

                string query = @"
                    UPDATE financial_data
                    SET
                        client_id = @client_id,
                        monthly_income = @monthly_income,
                        monthly_expenses = @monthly_expenses,
                        employment_status = @employment_status,
                        updated_at = NOW()
                    WHERE financial_data_id = @financial_data_id;
                ";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@client_id", clientId);
                    command.Parameters.AddWithValue("@monthly_income", monthlyIncome);
                    command.Parameters.AddWithValue("@monthly_expenses", monthlyExpenses);
                    command.Parameters.AddWithValue("@employment_status", employmentStatus);
                    command.Parameters.AddWithValue("@financial_data_id", financialDataId.Value);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show(
                "Финансовые данные успешно изменены.",
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