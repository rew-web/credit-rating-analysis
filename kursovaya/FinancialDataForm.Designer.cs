namespace CreditRatingApp
{
    partial class FinancialDataForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.ComboBox cmbClient;

        private System.Windows.Forms.Label lblMonthlyIncome;
        private System.Windows.Forms.NumericUpDown nudMonthlyIncome;

        private System.Windows.Forms.Label lblMonthlyExpenses;
        private System.Windows.Forms.NumericUpDown nudMonthlyExpenses;

        private System.Windows.Forms.Label lblEmploymentStatus;
        private System.Windows.Forms.ComboBox cmbEmploymentStatus;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();

            this.lblClient = new System.Windows.Forms.Label();
            this.cmbClient = new System.Windows.Forms.ComboBox();

            this.lblMonthlyIncome = new System.Windows.Forms.Label();
            this.nudMonthlyIncome = new System.Windows.Forms.NumericUpDown();

            this.lblMonthlyExpenses = new System.Windows.Forms.Label();
            this.nudMonthlyExpenses = new System.Windows.Forms.NumericUpDown();

            this.lblEmploymentStatus = new System.Windows.Forms.Label();
            this.cmbEmploymentStatus = new System.Windows.Forms.ComboBox();

            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonthlyIncome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonthlyExpenses)).BeginInit();
            this.SuspendLayout();

            // 
            // panelMain
            // 
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.lblClient);
            this.panelMain.Controls.Add(this.cmbClient);
            this.panelMain.Controls.Add(this.lblMonthlyIncome);
            this.panelMain.Controls.Add(this.nudMonthlyIncome);
            this.panelMain.Controls.Add(this.lblMonthlyExpenses);
            this.panelMain.Controls.Add(this.nudMonthlyExpenses);
            this.panelMain.Controls.Add(this.lblEmploymentStatus);
            this.panelMain.Controls.Add(this.cmbEmploymentStatus);
            this.panelMain.Controls.Add(this.btnSave);
            this.panelMain.Controls.Add(this.btnCancel);
            this.panelMain.Location = new System.Drawing.Point(35, 30);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(600, 390);
            this.panelMain.TabIndex = 0;

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(135, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(336, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Добавление финансовых данных";

            // 
            // lblClient
            // 
            this.lblClient.AutoSize = true;
            this.lblClient.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblClient.Location = new System.Drawing.Point(55, 90);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(56, 19);
            this.lblClient.TabIndex = 1;
            this.lblClient.Text = "Клиент:";

            // 
            // cmbClient
            // 
            this.cmbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClient.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbClient.FormattingEnabled = true;
            this.cmbClient.Location = new System.Drawing.Point(260, 87);
            this.cmbClient.Name = "cmbClient";
            this.cmbClient.Size = new System.Drawing.Size(280, 25);
            this.cmbClient.TabIndex = 2;

            // 
            // lblMonthlyIncome
            // 
            this.lblMonthlyIncome.AutoSize = true;
            this.lblMonthlyIncome.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMonthlyIncome.Location = new System.Drawing.Point(55, 140);
            this.lblMonthlyIncome.Name = "lblMonthlyIncome";
            this.lblMonthlyIncome.Size = new System.Drawing.Size(138, 19);
            this.lblMonthlyIncome.TabIndex = 3;
            this.lblMonthlyIncome.Text = "Ежемесячный доход:";

            // 
            // nudMonthlyIncome
            // 
            this.nudMonthlyIncome.DecimalPlaces = 2;
            this.nudMonthlyIncome.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudMonthlyIncome.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMonthlyIncome.Location = new System.Drawing.Point(260, 137);
            this.nudMonthlyIncome.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudMonthlyIncome.Name = "nudMonthlyIncome";
            this.nudMonthlyIncome.Size = new System.Drawing.Size(280, 25);
            this.nudMonthlyIncome.TabIndex = 4;
            this.nudMonthlyIncome.ThousandsSeparator = true;

            // 
            // lblMonthlyExpenses
            // 
            this.lblMonthlyExpenses.AutoSize = true;
            this.lblMonthlyExpenses.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMonthlyExpenses.Location = new System.Drawing.Point(55, 190);
            this.lblMonthlyExpenses.Name = "lblMonthlyExpenses";
            this.lblMonthlyExpenses.Size = new System.Drawing.Size(144, 19);
            this.lblMonthlyExpenses.TabIndex = 5;
            this.lblMonthlyExpenses.Text = "Ежемесячные расходы:";

            // 
            // nudMonthlyExpenses
            // 
            this.nudMonthlyExpenses.DecimalPlaces = 2;
            this.nudMonthlyExpenses.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudMonthlyExpenses.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMonthlyExpenses.Location = new System.Drawing.Point(260, 187);
            this.nudMonthlyExpenses.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudMonthlyExpenses.Name = "nudMonthlyExpenses";
            this.nudMonthlyExpenses.Size = new System.Drawing.Size(280, 25);
            this.nudMonthlyExpenses.TabIndex = 6;
            this.nudMonthlyExpenses.ThousandsSeparator = true;

            // 
            // lblEmploymentStatus
            // 
            this.lblEmploymentStatus.AutoSize = true;
            this.lblEmploymentStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmploymentStatus.Location = new System.Drawing.Point(55, 240);
            this.lblEmploymentStatus.Name = "lblEmploymentStatus";
            this.lblEmploymentStatus.Size = new System.Drawing.Size(121, 19);
            this.lblEmploymentStatus.TabIndex = 7;
            this.lblEmploymentStatus.Text = "Статус занятости:";

            // 
            // cmbEmploymentStatus
            // 
            this.cmbEmploymentStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmploymentStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEmploymentStatus.FormattingEnabled = true;
            this.cmbEmploymentStatus.Location = new System.Drawing.Point(260, 237);
            this.cmbEmploymentStatus.Name = "cmbEmploymentStatus";
            this.cmbEmploymentStatus.Size = new System.Drawing.Size(280, 25);
            this.cmbEmploymentStatus.TabIndex = 8;

            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSave.Location = new System.Drawing.Point(180, 310);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 38);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(320, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 38);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // 
            // FinancialDataForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 451);
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FinancialDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Финансовые данные";

            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonthlyIncome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonthlyExpenses)).EndInit();
            this.ResumeLayout(false);
        }
    }
}