namespace CreditRatingApp
{
    partial class CreditForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.ComboBox cmbClient;

        private System.Windows.Forms.Label lblCreditAmount;
        private System.Windows.Forms.NumericUpDown nudCreditAmount;

        private System.Windows.Forms.Label lblRemainingDebt;
        private System.Windows.Forms.NumericUpDown nudRemainingDebt;

        private System.Windows.Forms.Label lblMonthlyPayment;
        private System.Windows.Forms.NumericUpDown nudMonthlyPayment;

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;

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

            this.lblCreditAmount = new System.Windows.Forms.Label();
            this.nudCreditAmount = new System.Windows.Forms.NumericUpDown();

            this.lblRemainingDebt = new System.Windows.Forms.Label();
            this.nudRemainingDebt = new System.Windows.Forms.NumericUpDown();

            this.lblMonthlyPayment = new System.Windows.Forms.Label();
            this.nudMonthlyPayment = new System.Windows.Forms.NumericUpDown();

            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();

            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCreditAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRemainingDebt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonthlyPayment)).BeginInit();
            this.SuspendLayout();

            // 
            // panelMain
            // 
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.lblClient);
            this.panelMain.Controls.Add(this.cmbClient);
            this.panelMain.Controls.Add(this.lblCreditAmount);
            this.panelMain.Controls.Add(this.nudCreditAmount);
            this.panelMain.Controls.Add(this.lblRemainingDebt);
            this.panelMain.Controls.Add(this.nudRemainingDebt);
            this.panelMain.Controls.Add(this.lblMonthlyPayment);
            this.panelMain.Controls.Add(this.nudMonthlyPayment);
            this.panelMain.Controls.Add(this.lblStatus);
            this.panelMain.Controls.Add(this.cmbStatus);
            this.panelMain.Controls.Add(this.btnSave);
            this.panelMain.Controls.Add(this.btnCancel);
            this.panelMain.Location = new System.Drawing.Point(35, 30);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(570, 430);
            this.panelMain.TabIndex = 0;

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(180, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(216, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Добавление кредита";

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
            this.cmbClient.Location = new System.Drawing.Point(240, 87);
            this.cmbClient.Name = "cmbClient";
            this.cmbClient.Size = new System.Drawing.Size(270, 25);
            this.cmbClient.TabIndex = 2;

            // 
            // lblCreditAmount
            // 
            this.lblCreditAmount.AutoSize = true;
            this.lblCreditAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCreditAmount.Location = new System.Drawing.Point(55, 135);
            this.lblCreditAmount.Name = "lblCreditAmount";
            this.lblCreditAmount.Size = new System.Drawing.Size(102, 19);
            this.lblCreditAmount.TabIndex = 3;
            this.lblCreditAmount.Text = "Сумма кредита:";

            // 
            // nudCreditAmount
            // 
            this.nudCreditAmount.DecimalPlaces = 2;
            this.nudCreditAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudCreditAmount.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCreditAmount.Location = new System.Drawing.Point(240, 132);
            this.nudCreditAmount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudCreditAmount.Name = "nudCreditAmount";
            this.nudCreditAmount.Size = new System.Drawing.Size(270, 25);
            this.nudCreditAmount.TabIndex = 4;
            this.nudCreditAmount.ThousandsSeparator = true;

            // 
            // lblRemainingDebt
            // 
            this.lblRemainingDebt.AutoSize = true;
            this.lblRemainingDebt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRemainingDebt.Location = new System.Drawing.Point(55, 180);
            this.lblRemainingDebt.Name = "lblRemainingDebt";
            this.lblRemainingDebt.Size = new System.Drawing.Size(151, 19);
            this.lblRemainingDebt.TabIndex = 5;
            this.lblRemainingDebt.Text = "Остаток задолженности:";

            // 
            // nudRemainingDebt
            // 
            this.nudRemainingDebt.DecimalPlaces = 2;
            this.nudRemainingDebt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudRemainingDebt.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudRemainingDebt.Location = new System.Drawing.Point(240, 177);
            this.nudRemainingDebt.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudRemainingDebt.Name = "nudRemainingDebt";
            this.nudRemainingDebt.Size = new System.Drawing.Size(270, 25);
            this.nudRemainingDebt.TabIndex = 6;
            this.nudRemainingDebt.ThousandsSeparator = true;

            // 
            // lblMonthlyPayment
            // 
            this.lblMonthlyPayment.AutoSize = true;
            this.lblMonthlyPayment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMonthlyPayment.Location = new System.Drawing.Point(55, 225);
            this.lblMonthlyPayment.Name = "lblMonthlyPayment";
            this.lblMonthlyPayment.Size = new System.Drawing.Size(141, 19);
            this.lblMonthlyPayment.TabIndex = 7;
            this.lblMonthlyPayment.Text = "Ежемесячный платеж:";

            // 
            // nudMonthlyPayment
            // 
            this.nudMonthlyPayment.DecimalPlaces = 2;
            this.nudMonthlyPayment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudMonthlyPayment.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMonthlyPayment.Location = new System.Drawing.Point(240, 222);
            this.nudMonthlyPayment.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudMonthlyPayment.Name = "nudMonthlyPayment";
            this.nudMonthlyPayment.Size = new System.Drawing.Size(270, 25);
            this.nudMonthlyPayment.TabIndex = 8;
            this.nudMonthlyPayment.ThousandsSeparator = true;

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.Location = new System.Drawing.Point(55, 270);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(53, 19);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Статус:";

            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(240, 267);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(270, 25);
            this.cmbStatus.TabIndex = 10;

            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSave.Location = new System.Drawing.Point(165, 345);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 38);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(310, 345);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 38);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // 
            // CreditForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 491);
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CreditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Кредит";

            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCreditAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRemainingDebt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonthlyPayment)).EndInit();
            this.ResumeLayout(false);
        }
    }
}