namespace CustomPortalV2.OfficeAddIn
{
    partial class RibbonMain : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RibbonMain()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RibbonMain));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.buttonLogin = this.Factory.CreateRibbonButton();
            this.labelUserName = this.Factory.CreateRibbonLabel();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.comboBoxFormDefinations = this.Factory.CreateRibbonComboBox();
            this.comboBoxFormGroup = this.Factory.CreateRibbonComboBox();
            this.comboBoxFormFields = this.Factory.CreateRibbonComboBox();
            this.group3 = this.Factory.CreateRibbonGroup();
            this.buttonAddBookMark = this.Factory.CreateRibbonButton();
            this.buttonRemoveBookMark = this.Factory.CreateRibbonButton();
            this.buttonValidateBookMark = this.Factory.CreateRibbonButton();
            this.group4 = this.Factory.CreateRibbonGroup();
            this.buttonAddToday = this.Factory.CreateRibbonButton();
            this.buttonAddYesterday = this.Factory.CreateRibbonButton();
            this.buttonAddTomorrow = this.Factory.CreateRibbonButton();
            this.group5 = this.Factory.CreateRibbonGroup();
            this.buttonSendToServer = this.Factory.CreateRibbonButton();
            this.buttonDownloadServer = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            this.group3.SuspendLayout();
            this.group4.SuspendLayout();
            this.group5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Groups.Add(this.group2);
            this.tab1.Groups.Add(this.group3);
            this.tab1.Groups.Add(this.group4);
            this.tab1.Groups.Add(this.group5);
            this.tab1.Label = "Digital Form";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.buttonLogin);
            this.group1.Items.Add(this.labelUserName);
            this.group1.Label = "Giriş";
            this.group1.Name = "group1";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Image = ((System.Drawing.Image)(resources.GetObject("buttonLogin.Image")));
            this.buttonLogin.Label = "Giriş";
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.ShowImage = true;
            this.buttonLogin.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonLogin_Click);
            // 
            // labelUserName
            // 
            this.labelUserName.Label = "label1";
            this.labelUserName.Name = "labelUserName";
            // 
            // group2
            // 
            this.group2.Items.Add(this.comboBoxFormDefinations);
            this.group2.Items.Add(this.comboBoxFormGroup);
            this.group2.Items.Add(this.comboBoxFormFields);
            this.group2.Label = "Form Türü";
            this.group2.Name = "group2";
            // 
            // comboBoxFormDefinations
            // 
            this.comboBoxFormDefinations.Label = "Form Türü";
            this.comboBoxFormDefinations.Name = "comboBoxFormDefinations";
            this.comboBoxFormDefinations.Text = null;
            this.comboBoxFormDefinations.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.comboBoxFormDefinations_TextChanged);
            // 
            // comboBoxFormGroup
            // 
            this.comboBoxFormGroup.Label = "Form Grupları";
            this.comboBoxFormGroup.Name = "comboBoxFormGroup";
            this.comboBoxFormGroup.Text = null;
            this.comboBoxFormGroup.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.comboBoxFormGroup_TextChanged);
            // 
            // comboBoxFormFields
            // 
            this.comboBoxFormFields.Label = "Form Alanları";
            this.comboBoxFormFields.Name = "comboBoxFormFields";
            this.comboBoxFormFields.Text = null;
            // 
            // group3
            // 
            this.group3.Items.Add(this.buttonAddBookMark);
            this.group3.Items.Add(this.buttonRemoveBookMark);
            this.group3.Items.Add(this.buttonValidateBookMark);
            this.group3.Label = "Yer İşareti";
            this.group3.Name = "group3";
            // 
            // buttonAddBookMark
            // 
            this.buttonAddBookMark.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddBookMark.Image")));
            this.buttonAddBookMark.Label = "Ekle";
            this.buttonAddBookMark.Name = "buttonAddBookMark";
            this.buttonAddBookMark.ShowImage = true;
            this.buttonAddBookMark.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAddBookMark_Click);
            // 
            // buttonRemoveBookMark
            // 
            this.buttonRemoveBookMark.Image = ((System.Drawing.Image)(resources.GetObject("buttonRemoveBookMark.Image")));
            this.buttonRemoveBookMark.Label = "Sil";
            this.buttonRemoveBookMark.Name = "buttonRemoveBookMark";
            this.buttonRemoveBookMark.ShowImage = true;
            this.buttonRemoveBookMark.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonRemoveBookMark_Click);
            // 
            // buttonValidateBookMark
            // 
            this.buttonValidateBookMark.Image = ((System.Drawing.Image)(resources.GetObject("buttonValidateBookMark.Image")));
            this.buttonValidateBookMark.Label = "Doğrula";
            this.buttonValidateBookMark.Name = "buttonValidateBookMark";
            this.buttonValidateBookMark.ShowImage = true;
            this.buttonValidateBookMark.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonValidateBookMark_Click);
            // 
            // group4
            // 
            this.group4.Items.Add(this.buttonAddToday);
            this.group4.Items.Add(this.buttonAddYesterday);
            this.group4.Items.Add(this.buttonAddTomorrow);
            this.group4.Label = "Standart Alanları";
            this.group4.Name = "group4";
            // 
            // buttonAddToday
            // 
            this.buttonAddToday.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddToday.Image")));
            this.buttonAddToday.Label = "Bugünün Tarihi";
            this.buttonAddToday.Name = "buttonAddToday";
            this.buttonAddToday.ShowImage = true;
            this.buttonAddToday.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAddToday_Click);
            // 
            // buttonAddYesterday
            // 
            this.buttonAddYesterday.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddYesterday.Image")));
            this.buttonAddYesterday.Label = "Dünün Tarihi";
            this.buttonAddYesterday.Name = "buttonAddYesterday";
            this.buttonAddYesterday.ShowImage = true;
            this.buttonAddYesterday.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAddYesterday_Click);
            // 
            // buttonAddTomorrow
            // 
            this.buttonAddTomorrow.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddTomorrow.Image")));
            this.buttonAddTomorrow.Label = "Yarının Tarihi";
            this.buttonAddTomorrow.Name = "buttonAddTomorrow";
            this.buttonAddTomorrow.ShowImage = true;
            this.buttonAddTomorrow.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAddTomorrow_Click);
            // 
            // group5
            // 
            this.group5.Items.Add(this.buttonSendToServer);
            this.group5.Items.Add(this.buttonDownloadServer);
            this.group5.Label = "Güncelle";
            this.group5.Name = "group5";
            // 
            // buttonSendToServer
            // 
            this.buttonSendToServer.Image = ((System.Drawing.Image)(resources.GetObject("buttonSendToServer.Image")));
            this.buttonSendToServer.Label = "Server a Gönder";
            this.buttonSendToServer.Name = "buttonSendToServer";
            this.buttonSendToServer.ShowImage = true;
            // 
            // buttonDownloadServer
            // 
            this.buttonDownloadServer.Image = ((System.Drawing.Image)(resources.GetObject("buttonDownloadServer.Image")));
            this.buttonDownloadServer.Label = "Server dan indir";
            this.buttonDownloadServer.Name = "buttonDownloadServer";
            this.buttonDownloadServer.ShowImage = true;
            this.buttonDownloadServer.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonDownloadServer_Click);
            // 
            // RibbonMain
            // 
            this.Name = "RibbonMain";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonMain_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.group4.ResumeLayout(false);
            this.group4.PerformLayout();
            this.group5.ResumeLayout(false);
            this.group5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonLogin;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox comboBoxFormDefinations;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox comboBoxFormGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox comboBoxFormFields;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonRemoveBookMark;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAddBookMark;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonValidateBookMark;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group4;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAddToday;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAddYesterday;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAddTomorrow;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group5;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonSendToServer;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonDownloadServer;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel labelUserName;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonMain RibbonMain
        {
            get { return this.GetRibbon<RibbonMain>(); }
        }
    }
}
