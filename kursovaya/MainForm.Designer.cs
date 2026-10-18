namespace CreditRatingApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnExit;

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabClients;
        private System.Windows.Forms.TabPage tabFinancialData;
        private System.Windows.Forms.TabPage tabCredits;
        private System.Windows.Forms.TabPage tabCreditRatings;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.TabPage tabReports;

        private System.Windows.Forms.DataGridView dgvClients;
        private System.Windows.Forms.DataGridView dgvFinancialData;
        private System.Windows.Forms.DataGridView dgvCredits;
        private System.Windows.Forms.DataGridView dgvCreditRatings;
        private System.Windows.Forms.DataGridView dgvUsers;

        private System.Windows.Forms.Panel panelClientsButtons;
        private System.Windows.Forms.Button btnClientAdd;
        private System.Windows.Forms.Button btnClientEdit;
        private System.Windows.Forms.Button btnClientDelete;
        private System.Windows.Forms.Label lblClientSearch;
        private System.Windows.Forms.ComboBox cmbClientSearchField;
        private System.Windows.Forms.TextBox txtClientSearch;
        private System.Windows.Forms.Button btnClientSearch;
        private System.Windows.Forms.Button btnClientSearchClear;
        private System.Windows.Forms.SplitContainer splitClients;
        private System.Windows.Forms.RichTextBox rtbClientFullInfo;

        private System.Windows.Forms.Panel panelFinancialDataButtons;
        private System.Windows.Forms.Button btnFinancialDataAdd;
        private System.Windows.Forms.Button btnFinancialDataEdit;
        private System.Windows.Forms.Button btnFinancialDataDelete;

        private System.Windows.Forms.Panel panelCreditsButtons;
        private System.Windows.Forms.Button btnCreditAdd;
        private System.Windows.Forms.Button btnCreditEdit;
        private System.Windows.Forms.Button btnCreditDelete;

        private System.Windows.Forms.Panel panelRatingButtons;
        private System.Windows.Forms.Label lblRatingClient;
        private System.Windows.Forms.ComboBox cmbRatingClient;
        private System.Windows.Forms.Button btnCalculateRating;

        private System.Windows.Forms.SplitContainer splitUsers;
        private System.Windows.Forms.Panel panelUsersEdit;
        private System.Windows.Forms.Label lblUsersTitle;
        private System.Windows.Forms.Label lblUserLogin;
        private System.Windows.Forms.TextBox txtUserLogin;
        private System.Windows.Forms.Label lblUserFullName;
        private System.Windows.Forms.TextBox txtUserFullName;
        private System.Windows.Forms.Label lblUserPassword;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.Label lblUserConfirmPassword;
        private System.Windows.Forms.TextBox txtUserConfirmPassword;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.ComboBox cmbUserRole;
        private System.Windows.Forms.Button btnUserAdd;
        private System.Windows.Forms.Button btnUserEdit;
        private System.Windows.Forms.Button btnUserDelete;
        private System.Windows.Forms.Button btnUserClear;

        private System.Windows.Forms.Label lblReportsTitle;
        private System.Windows.Forms.ComboBox cmbReportType;
        private System.Windows.Forms.Button btnCreateReport;
        private System.Windows.Forms.RichTextBox rtbReport;

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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();

            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabClients = new System.Windows.Forms.TabPage();
            this.tabFinancialData = new System.Windows.Forms.TabPage();
            this.tabCredits = new System.Windows.Forms.TabPage();
            this.tabCreditRatings = new System.Windows.Forms.TabPage();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.tabReports = new System.Windows.Forms.TabPage();

            this.dgvClients = new System.Windows.Forms.DataGridView();
            this.dgvFinancialData = new System.Windows.Forms.DataGridView();
            this.dgvCredits = new System.Windows.Forms.DataGridView();
            this.dgvCreditRatings = new System.Windows.Forms.DataGridView();
            this.dgvUsers = new System.Windows.Forms.DataGridView();

            this.panelClientsButtons = new System.Windows.Forms.Panel();
            this.btnClientAdd = new System.Windows.Forms.Button();
            this.btnClientEdit = new System.Windows.Forms.Button();
            this.btnClientDelete = new System.Windows.Forms.Button();
            this.lblClientSearch = new System.Windows.Forms.Label();
            this.cmbClientSearchField = new System.Windows.Forms.ComboBox();
            this.txtClientSearch = new System.Windows.Forms.TextBox();
            this.btnClientSearch = new System.Windows.Forms.Button();
            this.btnClientSearchClear = new System.Windows.Forms.Button();
            this.splitClients = new System.Windows.Forms.SplitContainer();
            this.rtbClientFullInfo = new System.Windows.Forms.RichTextBox();

            this.panelFinancialDataButtons = new System.Windows.Forms.Panel();
            this.btnFinancialDataAdd = new System.Windows.Forms.Button();
            this.btnFinancialDataEdit = new System.Windows.Forms.Button();
            this.btnFinancialDataDelete = new System.Windows.Forms.Button();

            this.panelCreditsButtons = new System.Windows.Forms.Panel();
            this.btnCreditAdd = new System.Windows.Forms.Button();
            this.btnCreditEdit = new System.Windows.Forms.Button();
            this.btnCreditDelete = new System.Windows.Forms.Button();

            this.panelRatingButtons = new System.Windows.Forms.Panel();
            this.lblRatingClient = new System.Windows.Forms.Label();
            this.cmbRatingClient = new System.Windows.Forms.ComboBox();
            this.btnCalculateRating = new System.Windows.Forms.Button();

            this.splitUsers = new System.Windows.Forms.SplitContainer();
            this.panelUsersEdit = new System.Windows.Forms.Panel();
            this.lblUsersTitle = new System.Windows.Forms.Label();
            this.lblUserLogin = new System.Windows.Forms.Label();
            this.txtUserLogin = new System.Windows.Forms.TextBox();
            this.lblUserFullName = new System.Windows.Forms.Label();
            this.txtUserFullName = new System.Windows.Forms.TextBox();
            this.lblUserPassword = new System.Windows.Forms.Label();
            this.txtUserPassword = new System.Windows.Forms.TextBox();
            this.lblUserConfirmPassword = new System.Windows.Forms.Label();
            this.txtUserConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.cmbUserRole = new System.Windows.Forms.ComboBox();
            this.btnUserAdd = new System.Windows.Forms.Button();
            this.btnUserEdit = new System.Windows.Forms.Button();
            this.btnUserDelete = new System.Windows.Forms.Button();
            this.btnUserClear = new System.Windows.Forms.Button();

            this.lblReportsTitle = new System.Windows.Forms.Label();
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.btnCreateReport = new System.Windows.Forms.Button();
            this.rtbReport = new System.Windows.Forms.RichTextBox();

            this.panelTop.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabClients.SuspendLayout();
            this.tabFinancialData.SuspendLayout();
            this.tabCredits.SuspendLayout();
            this.tabCreditRatings.SuspendLayout();
            this.tabUsers.SuspendLayout();
            this.tabReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFinancialData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCredits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCreditRatings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.panelClientsButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitClients)).BeginInit();
            this.splitClients.Panel1.SuspendLayout();
            this.splitClients.Panel2.SuspendLayout();
            this.splitClients.SuspendLayout();
            this.panelFinancialDataButtons.SuspendLayout();
            this.panelCreditsButtons.SuspendLayout();
            this.panelRatingButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitUsers)).BeginInit();
            this.splitUsers.Panel1.SuspendLayout();
            this.splitUsers.Panel2.SuspendLayout();
            this.splitUsers.SuspendLayout();
            this.panelUsersEdit.SuspendLayout();
            this.SuspendLayout();

            // panelTop
            this.panelTop.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.lblCurrentUser);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.btnLogout);
            this.panelTop.Controls.Add(this.btnExit);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 80);
            this.panelTop.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(429, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "АИС анализа кредитного рейтинга клиентов";

            // lblCurrentUser
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCurrentUser.Location = new System.Drawing.Point(23, 48);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(105, 19);
            this.lblCurrentUser.TabIndex = 1;
            this.lblCurrentUser.Text = "Пользователь:";

            // btnRefresh
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(855, 25);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 32);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // btnLogout
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.Location = new System.Drawing.Point(965, 25);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(100, 32);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Выйти";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // btnExit
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(1075, 25);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 32);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Закрыть";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // tabMain
            this.tabMain.Controls.Add(this.tabClients);
            this.tabMain.Controls.Add(this.tabFinancialData);
            this.tabMain.Controls.Add(this.tabCredits);
            this.tabMain.Controls.Add(this.tabCreditRatings);
            this.tabMain.Controls.Add(this.tabUsers);
            this.tabMain.Controls.Add(this.tabReports);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabMain.Location = new System.Drawing.Point(0, 80);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1200, 720);
            this.tabMain.TabIndex = 1;

            // tabClients
            this.tabClients.Controls.Add(this.splitClients);
            this.tabClients.Controls.Add(this.panelClientsButtons);
            this.tabClients.Location = new System.Drawing.Point(4, 26);
            this.tabClients.Name = "tabClients";
            this.tabClients.Padding = new System.Windows.Forms.Padding(3);
            this.tabClients.Size = new System.Drawing.Size(1192, 690);
            this.tabClients.TabIndex = 0;
            this.tabClients.Text = "Клиенты";
            this.tabClients.UseVisualStyleBackColor = true;

            // panelClientsButtons
            this.panelClientsButtons.Controls.Add(this.btnClientAdd);
            this.panelClientsButtons.Controls.Add(this.btnClientEdit);
            this.panelClientsButtons.Controls.Add(this.btnClientDelete);
            this.panelClientsButtons.Controls.Add(this.lblClientSearch);
            this.panelClientsButtons.Controls.Add(this.cmbClientSearchField);
            this.panelClientsButtons.Controls.Add(this.txtClientSearch);
            this.panelClientsButtons.Controls.Add(this.btnClientSearch);
            this.panelClientsButtons.Controls.Add(this.btnClientSearchClear);
            this.panelClientsButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelClientsButtons.Location = new System.Drawing.Point(3, 3);
            this.panelClientsButtons.Name = "panelClientsButtons";
            this.panelClientsButtons.Size = new System.Drawing.Size(1186, 70);
            this.panelClientsButtons.TabIndex = 0;

            // btnClientAdd
            this.btnClientAdd.Location = new System.Drawing.Point(15, 18);
            this.btnClientAdd.Name = "btnClientAdd";
            this.btnClientAdd.Size = new System.Drawing.Size(105, 32);
            this.btnClientAdd.TabIndex = 0;
            this.btnClientAdd.Text = "Добавить";
            this.btnClientAdd.UseVisualStyleBackColor = true;
            this.btnClientAdd.Click += new System.EventHandler(this.btnClientAdd_Click);

            // btnClientEdit
            this.btnClientEdit.Location = new System.Drawing.Point(130, 18);
            this.btnClientEdit.Name = "btnClientEdit";
            this.btnClientEdit.Size = new System.Drawing.Size(105, 32);
            this.btnClientEdit.TabIndex = 1;
            this.btnClientEdit.Text = "Изменить";
            this.btnClientEdit.UseVisualStyleBackColor = true;
            this.btnClientEdit.Click += new System.EventHandler(this.btnClientEdit_Click);

            // btnClientDelete
            this.btnClientDelete.Location = new System.Drawing.Point(245, 18);
            this.btnClientDelete.Name = "btnClientDelete";
            this.btnClientDelete.Size = new System.Drawing.Size(105, 32);
            this.btnClientDelete.TabIndex = 2;
            this.btnClientDelete.Text = "Удалить";
            this.btnClientDelete.UseVisualStyleBackColor = true;
            this.btnClientDelete.Click += new System.EventHandler(this.btnClientDelete_Click);

            // lblClientSearch
            this.lblClientSearch.AutoSize = true;
            this.lblClientSearch.Location = new System.Drawing.Point(385, 24);
            this.lblClientSearch.Name = "lblClientSearch";
            this.lblClientSearch.Size = new System.Drawing.Size(52, 19);
            this.lblClientSearch.TabIndex = 3;
            this.lblClientSearch.Text = "Поиск:";

            // cmbClientSearchField
            this.cmbClientSearchField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClientSearchField.FormattingEnabled = true;
            this.cmbClientSearchField.Location = new System.Drawing.Point(445, 21);
            this.cmbClientSearchField.Name = "cmbClientSearchField";
            this.cmbClientSearchField.Size = new System.Drawing.Size(130, 25);
            this.cmbClientSearchField.TabIndex = 4;

            // txtClientSearch
            this.txtClientSearch.Location = new System.Drawing.Point(585, 21);
            this.txtClientSearch.Name = "txtClientSearch";
            this.txtClientSearch.Size = new System.Drawing.Size(260, 25);
            this.txtClientSearch.TabIndex = 5;

            // btnClientSearch
            this.btnClientSearch.Location = new System.Drawing.Point(855, 18);
            this.btnClientSearch.Name = "btnClientSearch";
            this.btnClientSearch.Size = new System.Drawing.Size(105, 32);
            this.btnClientSearch.TabIndex = 6;
            this.btnClientSearch.Text = "Найти";
            this.btnClientSearch.UseVisualStyleBackColor = true;
            this.btnClientSearch.Click += new System.EventHandler(this.btnClientSearch_Click);

            // btnClientSearchClear
            this.btnClientSearchClear.Location = new System.Drawing.Point(970, 18);
            this.btnClientSearchClear.Name = "btnClientSearchClear";
            this.btnClientSearchClear.Size = new System.Drawing.Size(105, 32);
            this.btnClientSearchClear.TabIndex = 7;
            this.btnClientSearchClear.Text = "Очистить";
            this.btnClientSearchClear.UseVisualStyleBackColor = true;
            this.btnClientSearchClear.Click += new System.EventHandler(this.btnClientSearchClear_Click);

            // splitClients
            this.splitClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitClients.Location = new System.Drawing.Point(3, 73);
            this.splitClients.Name = "splitClients";
            this.splitClients.Orientation = System.Windows.Forms.Orientation.Horizontal;

            this.splitClients.Panel1.Controls.Add(this.dgvClients);
            this.splitClients.Panel2.Controls.Add(this.rtbClientFullInfo);

            this.splitClients.Size = new System.Drawing.Size(1186, 614);
            this.splitClients.SplitterDistance = 300;
            this.splitClients.TabIndex = 1;

            // dgvClients
            this.dgvClients.AllowUserToAddRows = false;
            this.dgvClients.AllowUserToDeleteRows = false;
            this.dgvClients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClients.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClients.Location = new System.Drawing.Point(0, 0);
            this.dgvClients.Name = "dgvClients";
            this.dgvClients.ReadOnly = true;
            this.dgvClients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClients.Size = new System.Drawing.Size(1186, 300);
            this.dgvClients.TabIndex = 0;

            // rtbClientFullInfo
            this.rtbClientFullInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbClientFullInfo.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbClientFullInfo.Location = new System.Drawing.Point(0, 0);
            this.rtbClientFullInfo.Name = "rtbClientFullInfo";
            this.rtbClientFullInfo.ReadOnly = true;
            this.rtbClientFullInfo.Size = new System.Drawing.Size(1186, 310);
            this.rtbClientFullInfo.TabIndex = 0;
            this.rtbClientFullInfo.Text = "";

            // tabFinancialData
            this.tabFinancialData.Controls.Add(this.dgvFinancialData);
            this.tabFinancialData.Controls.Add(this.panelFinancialDataButtons);
            this.tabFinancialData.Location = new System.Drawing.Point(4, 26);
            this.tabFinancialData.Name = "tabFinancialData";
            this.tabFinancialData.Padding = new System.Windows.Forms.Padding(3);
            this.tabFinancialData.Size = new System.Drawing.Size(1192, 690);
            this.tabFinancialData.TabIndex = 1;
            this.tabFinancialData.Text = "Финансовые данные";
            this.tabFinancialData.UseVisualStyleBackColor = true;

            // panelFinancialDataButtons
            this.panelFinancialDataButtons.Controls.Add(this.btnFinancialDataAdd);
            this.panelFinancialDataButtons.Controls.Add(this.btnFinancialDataEdit);
            this.panelFinancialDataButtons.Controls.Add(this.btnFinancialDataDelete);
            this.panelFinancialDataButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFinancialDataButtons.Location = new System.Drawing.Point(3, 3);
            this.panelFinancialDataButtons.Name = "panelFinancialDataButtons";
            this.panelFinancialDataButtons.Size = new System.Drawing.Size(1186, 55);
            this.panelFinancialDataButtons.TabIndex = 0;

            // btnFinancialDataAdd
            this.btnFinancialDataAdd.Location = new System.Drawing.Point(15, 10);
            this.btnFinancialDataAdd.Name = "btnFinancialDataAdd";
            this.btnFinancialDataAdd.Size = new System.Drawing.Size(110, 32);
            this.btnFinancialDataAdd.TabIndex = 0;
            this.btnFinancialDataAdd.Text = "Добавить";
            this.btnFinancialDataAdd.UseVisualStyleBackColor = true;
            this.btnFinancialDataAdd.Click += new System.EventHandler(this.btnFinancialDataAdd_Click);

            // btnFinancialDataEdit
            this.btnFinancialDataEdit.Location = new System.Drawing.Point(135, 10);
            this.btnFinancialDataEdit.Name = "btnFinancialDataEdit";
            this.btnFinancialDataEdit.Size = new System.Drawing.Size(110, 32);
            this.btnFinancialDataEdit.TabIndex = 1;
            this.btnFinancialDataEdit.Text = "Изменить";
            this.btnFinancialDataEdit.UseVisualStyleBackColor = true;
            this.btnFinancialDataEdit.Click += new System.EventHandler(this.btnFinancialDataEdit_Click);

            // btnFinancialDataDelete
            this.btnFinancialDataDelete.Location = new System.Drawing.Point(255, 10);
            this.btnFinancialDataDelete.Name = "btnFinancialDataDelete";
            this.btnFinancialDataDelete.Size = new System.Drawing.Size(110, 32);
            this.btnFinancialDataDelete.TabIndex = 2;
            this.btnFinancialDataDelete.Text = "Удалить";
            this.btnFinancialDataDelete.UseVisualStyleBackColor = true;
            this.btnFinancialDataDelete.Click += new System.EventHandler(this.btnFinancialDataDelete_Click);

            // dgvFinancialData
            this.dgvFinancialData.AllowUserToAddRows = false;
            this.dgvFinancialData.AllowUserToDeleteRows = false;
            this.dgvFinancialData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFinancialData.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvFinancialData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFinancialData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFinancialData.Location = new System.Drawing.Point(3, 58);
            this.dgvFinancialData.Name = "dgvFinancialData";
            this.dgvFinancialData.ReadOnly = true;
            this.dgvFinancialData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFinancialData.Size = new System.Drawing.Size(1186, 629);
            this.dgvFinancialData.TabIndex = 1;

            // tabCredits
            this.tabCredits.Controls.Add(this.dgvCredits);
            this.tabCredits.Controls.Add(this.panelCreditsButtons);
            this.tabCredits.Location = new System.Drawing.Point(4, 26);
            this.tabCredits.Name = "tabCredits";
            this.tabCredits.Padding = new System.Windows.Forms.Padding(3);
            this.tabCredits.Size = new System.Drawing.Size(1192, 690);
            this.tabCredits.TabIndex = 2;
            this.tabCredits.Text = "Кредиты";
            this.tabCredits.UseVisualStyleBackColor = true;

            // panelCreditsButtons
            this.panelCreditsButtons.Controls.Add(this.btnCreditAdd);
            this.panelCreditsButtons.Controls.Add(this.btnCreditEdit);
            this.panelCreditsButtons.Controls.Add(this.btnCreditDelete);
            this.panelCreditsButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCreditsButtons.Location = new System.Drawing.Point(3, 3);
            this.panelCreditsButtons.Name = "panelCreditsButtons";
            this.panelCreditsButtons.Size = new System.Drawing.Size(1186, 55);
            this.panelCreditsButtons.TabIndex = 0;

            // btnCreditAdd
            this.btnCreditAdd.Location = new System.Drawing.Point(15, 10);
            this.btnCreditAdd.Name = "btnCreditAdd";
            this.btnCreditAdd.Size = new System.Drawing.Size(110, 32);
            this.btnCreditAdd.TabIndex = 0;
            this.btnCreditAdd.Text = "Добавить";
            this.btnCreditAdd.UseVisualStyleBackColor = true;
            this.btnCreditAdd.Click += new System.EventHandler(this.btnCreditAdd_Click);

            // btnCreditEdit
            this.btnCreditEdit.Location = new System.Drawing.Point(135, 10);
            this.btnCreditEdit.Name = "btnCreditEdit";
            this.btnCreditEdit.Size = new System.Drawing.Size(110, 32);
            this.btnCreditEdit.TabIndex = 1;
            this.btnCreditEdit.Text = "Изменить";
            this.btnCreditEdit.UseVisualStyleBackColor = true;
            this.btnCreditEdit.Click += new System.EventHandler(this.btnCreditEdit_Click);

            // btnCreditDelete
            this.btnCreditDelete.Location = new System.Drawing.Point(255, 10);
            this.btnCreditDelete.Name = "btnCreditDelete";
            this.btnCreditDelete.Size = new System.Drawing.Size(110, 32);
            this.btnCreditDelete.TabIndex = 2;
            this.btnCreditDelete.Text = "Удалить";
            this.btnCreditDelete.UseVisualStyleBackColor = true;
            this.btnCreditDelete.Click += new System.EventHandler(this.btnCreditDelete_Click);

            // dgvCredits
            this.dgvCredits.AllowUserToAddRows = false;
            this.dgvCredits.AllowUserToDeleteRows = false;
            this.dgvCredits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCredits.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCredits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCredits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCredits.Location = new System.Drawing.Point(3, 58);
            this.dgvCredits.Name = "dgvCredits";
            this.dgvCredits.ReadOnly = true;
            this.dgvCredits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCredits.Size = new System.Drawing.Size(1186, 629);
            this.dgvCredits.TabIndex = 1;

            // tabCreditRatings
            this.tabCreditRatings.Controls.Add(this.dgvCreditRatings);
            this.tabCreditRatings.Controls.Add(this.panelRatingButtons);
            this.tabCreditRatings.Location = new System.Drawing.Point(4, 26);
            this.tabCreditRatings.Name = "tabCreditRatings";
            this.tabCreditRatings.Padding = new System.Windows.Forms.Padding(3);
            this.tabCreditRatings.Size = new System.Drawing.Size(1192, 690);
            this.tabCreditRatings.TabIndex = 3;
            this.tabCreditRatings.Text = "Кредитные рейтинги";
            this.tabCreditRatings.UseVisualStyleBackColor = true;

            // panelRatingButtons
            this.panelRatingButtons.Controls.Add(this.lblRatingClient);
            this.panelRatingButtons.Controls.Add(this.cmbRatingClient);
            this.panelRatingButtons.Controls.Add(this.btnCalculateRating);
            this.panelRatingButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRatingButtons.Location = new System.Drawing.Point(3, 3);
            this.panelRatingButtons.Name = "panelRatingButtons";
            this.panelRatingButtons.Size = new System.Drawing.Size(1186, 65);
            this.panelRatingButtons.TabIndex = 0;

            // lblRatingClient
            this.lblRatingClient.AutoSize = true;
            this.lblRatingClient.Location = new System.Drawing.Point(15, 22);
            this.lblRatingClient.Name = "lblRatingClient";
            this.lblRatingClient.Size = new System.Drawing.Size(60, 19);
            this.lblRatingClient.TabIndex = 0;
            this.lblRatingClient.Text = "Клиент:";

            // cmbRatingClient
            this.cmbRatingClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRatingClient.FormattingEnabled = true;
            this.cmbRatingClient.Location = new System.Drawing.Point(85, 19);
            this.cmbRatingClient.Name = "cmbRatingClient";
            this.cmbRatingClient.Size = new System.Drawing.Size(320, 25);
            this.cmbRatingClient.TabIndex = 1;

            // btnCalculateRating
            this.btnCalculateRating.Location = new System.Drawing.Point(425, 16);
            this.btnCalculateRating.Name = "btnCalculateRating";
            this.btnCalculateRating.Size = new System.Drawing.Size(180, 32);
            this.btnCalculateRating.TabIndex = 2;
            this.btnCalculateRating.Text = "Рассчитать рейтинг";
            this.btnCalculateRating.UseVisualStyleBackColor = true;
            this.btnCalculateRating.Click += new System.EventHandler(this.btnCalculateRating_Click);

            // dgvCreditRatings
            this.dgvCreditRatings.AllowUserToAddRows = false;
            this.dgvCreditRatings.AllowUserToDeleteRows = false;
            this.dgvCreditRatings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCreditRatings.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCreditRatings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCreditRatings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCreditRatings.Location = new System.Drawing.Point(3, 68);
            this.dgvCreditRatings.Name = "dgvCreditRatings";
            this.dgvCreditRatings.ReadOnly = true;
            this.dgvCreditRatings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCreditRatings.Size = new System.Drawing.Size(1186, 619);
            this.dgvCreditRatings.TabIndex = 1;

            // tabUsers
            this.tabUsers.Controls.Add(this.splitUsers);
            this.tabUsers.Location = new System.Drawing.Point(4, 26);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabUsers.Size = new System.Drawing.Size(1192, 690);
            this.tabUsers.TabIndex = 4;
            this.tabUsers.Text = "Пользователи";
            this.tabUsers.UseVisualStyleBackColor = true;

            // splitUsers
            this.splitUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitUsers.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitUsers.Location = new System.Drawing.Point(3, 3);
            this.splitUsers.Name = "splitUsers";
            this.splitUsers.Panel1.Controls.Add(this.dgvUsers);
            this.splitUsers.Panel2.Controls.Add(this.panelUsersEdit);
            this.splitUsers.Size = new System.Drawing.Size(1186, 684);
            this.splitUsers.SplitterDistance = 800;
            this.splitUsers.TabIndex = 0;

            // dgvUsers
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.Location = new System.Drawing.Point(0, 0);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(800, 684);
            this.dgvUsers.TabIndex = 0;
            this.dgvUsers.SelectionChanged += new System.EventHandler(this.dgvUsers_SelectionChanged);

            // panelUsersEdit
            this.panelUsersEdit.Controls.Add(this.lblUsersTitle);
            this.panelUsersEdit.Controls.Add(this.lblUserLogin);
            this.panelUsersEdit.Controls.Add(this.txtUserLogin);
            this.panelUsersEdit.Controls.Add(this.lblUserFullName);
            this.panelUsersEdit.Controls.Add(this.txtUserFullName);
            this.panelUsersEdit.Controls.Add(this.lblUserPassword);
            this.panelUsersEdit.Controls.Add(this.txtUserPassword);
            this.panelUsersEdit.Controls.Add(this.lblUserConfirmPassword);
            this.panelUsersEdit.Controls.Add(this.txtUserConfirmPassword);
            this.panelUsersEdit.Controls.Add(this.lblUserRole);
            this.panelUsersEdit.Controls.Add(this.cmbUserRole);
            this.panelUsersEdit.Controls.Add(this.btnUserAdd);
            this.panelUsersEdit.Controls.Add(this.btnUserEdit);
            this.panelUsersEdit.Controls.Add(this.btnUserDelete);
            this.panelUsersEdit.Controls.Add(this.btnUserClear);
            this.panelUsersEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUsersEdit.Location = new System.Drawing.Point(0, 0);
            this.panelUsersEdit.Name = "panelUsersEdit";
            this.panelUsersEdit.Size = new System.Drawing.Size(382, 684);
            this.panelUsersEdit.TabIndex = 0;

            // lblUsersTitle
            this.lblUsersTitle.AutoSize = true;
            this.lblUsersTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblUsersTitle.Location = new System.Drawing.Point(20, 25);
            this.lblUsersTitle.Name = "lblUsersTitle";
            this.lblUsersTitle.Size = new System.Drawing.Size(219, 21);
            this.lblUsersTitle.TabIndex = 0;
            this.lblUsersTitle.Text = "Управление пользователем";

            // lblUserLogin
            this.lblUserLogin.AutoSize = true;
            this.lblUserLogin.Location = new System.Drawing.Point(20, 75);
            this.lblUserLogin.Name = "lblUserLogin";
            this.lblUserLogin.Size = new System.Drawing.Size(52, 19);
            this.lblUserLogin.TabIndex = 1;
            this.lblUserLogin.Text = "Логин:";

            // txtUserLogin
            this.txtUserLogin.Location = new System.Drawing.Point(20, 98);
            this.txtUserLogin.Name = "txtUserLogin";
            this.txtUserLogin.Size = new System.Drawing.Size(330, 25);
            this.txtUserLogin.TabIndex = 2;

            // lblUserFullName
            this.lblUserFullName.AutoSize = true;
            this.lblUserFullName.Location = new System.Drawing.Point(20, 140);
            this.lblUserFullName.Name = "lblUserFullName";
            this.lblUserFullName.Size = new System.Drawing.Size(42, 19);
            this.lblUserFullName.TabIndex = 3;
            this.lblUserFullName.Text = "ФИО:";

            // txtUserFullName
            this.txtUserFullName.Location = new System.Drawing.Point(20, 163);
            this.txtUserFullName.Name = "txtUserFullName";
            this.txtUserFullName.Size = new System.Drawing.Size(330, 25);
            this.txtUserFullName.TabIndex = 4;

            // lblUserPassword
            this.lblUserPassword.AutoSize = true;
            this.lblUserPassword.Location = new System.Drawing.Point(20, 205);
            this.lblUserPassword.Name = "lblUserPassword";
            this.lblUserPassword.Size = new System.Drawing.Size(185, 19);
            this.lblUserPassword.TabIndex = 5;
            this.lblUserPassword.Text = "Пароль / новый пароль:";

            // txtUserPassword
            this.txtUserPassword.Location = new System.Drawing.Point(20, 228);
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.Size = new System.Drawing.Size(330, 25);
            this.txtUserPassword.TabIndex = 6;
            this.txtUserPassword.UseSystemPasswordChar = true;

            // lblUserConfirmPassword
            this.lblUserConfirmPassword.AutoSize = true;
            this.lblUserConfirmPassword.Location = new System.Drawing.Point(20, 270);
            this.lblUserConfirmPassword.Name = "lblUserConfirmPassword";
            this.lblUserConfirmPassword.Size = new System.Drawing.Size(115, 19);
            this.lblUserConfirmPassword.TabIndex = 7;
            this.lblUserConfirmPassword.Text = "Повтор пароля:";

            // txtUserConfirmPassword
            this.txtUserConfirmPassword.Location = new System.Drawing.Point(20, 293);
            this.txtUserConfirmPassword.Name = "txtUserConfirmPassword";
            this.txtUserConfirmPassword.Size = new System.Drawing.Size(330, 25);
            this.txtUserConfirmPassword.TabIndex = 8;
            this.txtUserConfirmPassword.UseSystemPasswordChar = true;

            // lblUserRole
            this.lblUserRole.AutoSize = true;
            this.lblUserRole.Location = new System.Drawing.Point(20, 335);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(42, 19);
            this.lblUserRole.TabIndex = 9;
            this.lblUserRole.Text = "Роль:";

            // cmbUserRole
            this.cmbUserRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserRole.FormattingEnabled = true;
            this.cmbUserRole.Location = new System.Drawing.Point(20, 358);
            this.cmbUserRole.Name = "cmbUserRole";
            this.cmbUserRole.Size = new System.Drawing.Size(330, 25);
            this.cmbUserRole.TabIndex = 10;

            // btnUserAdd
            this.btnUserAdd.Location = new System.Drawing.Point(20, 415);
            this.btnUserAdd.Name = "btnUserAdd";
            this.btnUserAdd.Size = new System.Drawing.Size(155, 34);
            this.btnUserAdd.TabIndex = 11;
            this.btnUserAdd.Text = "Добавить";
            this.btnUserAdd.UseVisualStyleBackColor = true;
            this.btnUserAdd.Click += new System.EventHandler(this.btnUserAdd_Click);

            // btnUserEdit
            this.btnUserEdit.Location = new System.Drawing.Point(195, 415);
            this.btnUserEdit.Name = "btnUserEdit";
            this.btnUserEdit.Size = new System.Drawing.Size(155, 34);
            this.btnUserEdit.TabIndex = 12;
            this.btnUserEdit.Text = "Изменить";
            this.btnUserEdit.UseVisualStyleBackColor = true;
            this.btnUserEdit.Click += new System.EventHandler(this.btnUserEdit_Click);

            // btnUserDelete
            this.btnUserDelete.Location = new System.Drawing.Point(20, 465);
            this.btnUserDelete.Name = "btnUserDelete";
            this.btnUserDelete.Size = new System.Drawing.Size(155, 34);
            this.btnUserDelete.TabIndex = 13;
            this.btnUserDelete.Text = "Удалить";
            this.btnUserDelete.UseVisualStyleBackColor = true;
            this.btnUserDelete.Click += new System.EventHandler(this.btnUserDelete_Click);

            // btnUserClear
            this.btnUserClear.Location = new System.Drawing.Point(195, 465);
            this.btnUserClear.Name = "btnUserClear";
            this.btnUserClear.Size = new System.Drawing.Size(155, 34);
            this.btnUserClear.TabIndex = 14;
            this.btnUserClear.Text = "Очистить";
            this.btnUserClear.UseVisualStyleBackColor = true;
            this.btnUserClear.Click += new System.EventHandler(this.btnUserClear_Click);

            // tabReports
            this.tabReports.Controls.Add(this.rtbReport);
            this.tabReports.Controls.Add(this.btnCreateReport);
            this.tabReports.Controls.Add(this.cmbReportType);
            this.tabReports.Controls.Add(this.lblReportsTitle);
            this.tabReports.Location = new System.Drawing.Point(4, 26);
            this.tabReports.Name = "tabReports";
            this.tabReports.Padding = new System.Windows.Forms.Padding(3);
            this.tabReports.Size = new System.Drawing.Size(1192, 690);
            this.tabReports.TabIndex = 5;
            this.tabReports.Text = "Отчеты";
            this.tabReports.UseVisualStyleBackColor = true;

            // lblReportsTitle
            this.lblReportsTitle.AutoSize = true;
            this.lblReportsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblReportsTitle.Location = new System.Drawing.Point(20, 20);
            this.lblReportsTitle.Name = "lblReportsTitle";
            this.lblReportsTitle.Size = new System.Drawing.Size(181, 21);
            this.lblReportsTitle.TabIndex = 0;
            this.lblReportsTitle.Text = "Формирование отчета";

            // cmbReportType
            this.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportType.FormattingEnabled = true;
            this.cmbReportType.Location = new System.Drawing.Point(25, 60);
            this.cmbReportType.Name = "cmbReportType";
            this.cmbReportType.Size = new System.Drawing.Size(350, 25);
            this.cmbReportType.TabIndex = 1;

            // btnCreateReport
            this.btnCreateReport.Location = new System.Drawing.Point(395, 56);
            this.btnCreateReport.Name = "btnCreateReport";
            this.btnCreateReport.Size = new System.Drawing.Size(180, 32);
            this.btnCreateReport.TabIndex = 2;
            this.btnCreateReport.Text = "Сформировать";
            this.btnCreateReport.UseVisualStyleBackColor = true;
            this.btnCreateReport.Click += new System.EventHandler(this.btnCreateReport_Click);

            // rtbReport
            this.rtbReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbReport.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbReport.Location = new System.Drawing.Point(25, 110);
            this.rtbReport.Name = "rtbReport";
            this.rtbReport.ReadOnly = true;
            this.rtbReport.Size = new System.Drawing.Size(1140, 550);
            this.rtbReport.TabIndex = 3;
            this.rtbReport.Text = "";

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "АИС анализа кредитного рейтинга";

            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabClients.ResumeLayout(false);
            this.tabFinancialData.ResumeLayout(false);
            this.tabCredits.ResumeLayout(false);
            this.tabCreditRatings.ResumeLayout(false);
            this.tabUsers.ResumeLayout(false);
            this.tabReports.ResumeLayout(false);
            this.tabReports.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFinancialData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCredits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCreditRatings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.panelClientsButtons.ResumeLayout(false);
            this.panelClientsButtons.PerformLayout();
            this.splitClients.Panel1.ResumeLayout(false);
            this.splitClients.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitClients)).EndInit();
            this.splitClients.ResumeLayout(false);
            this.panelFinancialDataButtons.ResumeLayout(false);
            this.panelCreditsButtons.ResumeLayout(false);
            this.panelRatingButtons.ResumeLayout(false);
            this.panelRatingButtons.PerformLayout();
            this.splitUsers.Panel1.ResumeLayout(false);
            this.splitUsers.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitUsers)).EndInit();
            this.splitUsers.ResumeLayout(false);
            this.panelUsersEdit.ResumeLayout(false);
            this.panelUsersEdit.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}