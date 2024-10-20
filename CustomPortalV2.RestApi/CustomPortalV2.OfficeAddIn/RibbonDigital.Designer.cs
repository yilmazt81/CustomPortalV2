namespace CustomPortalV2.OfficeAddIn
{
    partial class RibbonDigital : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public RibbonDigital()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RibbonDigital));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.buttonLogin = this.Factory.CreateRibbonButton();
            this.groupTagFields = this.Factory.CreateRibbonGroup();
            this.comboBoxFormDefination = this.Factory.CreateRibbonComboBox();
            this.comboBoxFormGroups = this.Factory.CreateRibbonComboBox();
            this.comboBoxDefinationFieldTags = this.Factory.CreateRibbonComboBox();
            this.buttonRemoveTag = this.Factory.CreateRibbonButton();
            this.buttonAddTag = this.Factory.CreateRibbonButton();
            this.buttonValidateFields = this.Factory.CreateRibbonButton();
            this.groupUnderline = this.Factory.CreateRibbonGroup();
            this.comboBoxUnderlineRule = this.Factory.CreateRibbonComboBox();
            this.buttonGroup1 = this.Factory.CreateRibbonButtonGroup();
            this.buttonUnderlineStartTag = this.Factory.CreateRibbonButton();
            this.buttonUnderlineEndTag = this.Factory.CreateRibbonButton();
            this.buttonRule = this.Factory.CreateRibbonButton();
            this.group3 = this.Factory.CreateRibbonGroup();
            this.buttonOpenForms = this.Factory.CreateRibbonButton();
            this.buttonCompanyDefination = this.Factory.CreateRibbonButton();
            this.groupStandart = this.Factory.CreateRibbonGroup();
            this.buttonToday = this.Factory.CreateRibbonButton();
            this.buttonTodayOne = this.Factory.CreateRibbonButton();
            this.buttonTomorrow = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.groupTagFields.SuspendLayout();
            this.groupUnderline.SuspendLayout();
            this.buttonGroup1.SuspendLayout();
            this.group3.SuspendLayout();
            this.groupStandart.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Groups.Add(this.groupTagFields);
            this.tab1.Groups.Add(this.groupUnderline);
            this.tab1.Groups.Add(this.group3);
            this.tab1.Groups.Add(this.groupStandart);
            this.tab1.Label = "Digital Form";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.buttonLogin);
            this.group1.Label = "Alan Listesi";
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
            // groupTagFields
            // 
            this.groupTagFields.Items.Add(this.comboBoxFormDefination);
            this.groupTagFields.Items.Add(this.comboBoxFormGroups);
            this.groupTagFields.Items.Add(this.comboBoxDefinationFieldTags);
            this.groupTagFields.Items.Add(this.buttonRemoveTag);
            this.groupTagFields.Items.Add(this.buttonAddTag);
            this.groupTagFields.Items.Add(this.buttonValidateFields);
            this.groupTagFields.Label = "Form Adresleme";
            this.groupTagFields.Name = "groupTagFields";
            // 
            // comboBoxFormDefination
            // 
            this.comboBoxFormDefination.Label = "Form Türü";
            this.comboBoxFormDefination.Name = "comboBoxFormDefination";
            this.comboBoxFormDefination.Text = null;
            this.comboBoxFormDefination.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.comboBoxFormDefination_TextChanged);
            // 
            // comboBoxFormGroups
            // 
            this.comboBoxFormGroups.Label = "Form Grupları";
            this.comboBoxFormGroups.Name = "comboBoxFormGroups";
            this.comboBoxFormGroups.Text = null;
            this.comboBoxFormGroups.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.comboBoxFormGroups_TextChanged);
            // 
            // comboBoxDefinationFieldTags
            // 
            this.comboBoxDefinationFieldTags.Label = "Form alanları";
            this.comboBoxDefinationFieldTags.Name = "comboBoxDefinationFieldTags";
            this.comboBoxDefinationFieldTags.Text = null;
            // 
            // buttonRemoveTag
            // 
            this.buttonRemoveTag.Image = ((System.Drawing.Image)(resources.GetObject("buttonRemoveTag.Image")));
            this.buttonRemoveTag.Label = "Yer İşareti Sil";
            this.buttonRemoveTag.Name = "buttonRemoveTag";
            this.buttonRemoveTag.ShowImage = true;
            this.buttonRemoveTag.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonRemoveTag_Click);
            // 
            // buttonAddTag
            // 
            this.buttonAddTag.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddTag.Image")));
            this.buttonAddTag.Label = "Yer İşareti Ekle";
            this.buttonAddTag.Name = "buttonAddTag";
            this.buttonAddTag.ShowImage = true;
            this.buttonAddTag.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonAddTag_Click);
            // 
            // buttonValidateFields
            // 
            this.buttonValidateFields.Image = ((System.Drawing.Image)(resources.GetObject("buttonValidateFields.Image")));
            this.buttonValidateFields.Label = "Doğrula";
            this.buttonValidateFields.Name = "buttonValidateFields";
            this.buttonValidateFields.ShowImage = true;
            this.buttonValidateFields.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonValidateFields_Click);
            // 
            // groupUnderline
            // 
            this.groupUnderline.Items.Add(this.comboBoxUnderlineRule);
            this.groupUnderline.Items.Add(this.buttonGroup1);
            this.groupUnderline.Items.Add(this.buttonRule);
            this.groupUnderline.Label = "Çizilecek Maddeler";
            this.groupUnderline.Name = "groupUnderline";
            // 
            // comboBoxUnderlineRule
            // 
            this.comboBoxUnderlineRule.Label = "Bağlangıç";
            this.comboBoxUnderlineRule.Name = "comboBoxUnderlineRule";
            this.comboBoxUnderlineRule.Text = null;
            // 
            // buttonGroup1
            // 
            this.buttonGroup1.Items.Add(this.buttonUnderlineStartTag);
            this.buttonGroup1.Items.Add(this.buttonUnderlineEndTag);
            this.buttonGroup1.Name = "buttonGroup1";
            // 
            // buttonUnderlineStartTag
            // 
            this.buttonUnderlineStartTag.Image = ((System.Drawing.Image)(resources.GetObject("buttonUnderlineStartTag.Image")));
            this.buttonUnderlineStartTag.Label = "Bağlangıç";
            this.buttonUnderlineStartTag.Name = "buttonUnderlineStartTag";
            this.buttonUnderlineStartTag.ShowImage = true;
            this.buttonUnderlineStartTag.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonUnderlineStartTag_Click);
            // 
            // buttonUnderlineEndTag
            // 
            this.buttonUnderlineEndTag.Image = ((System.Drawing.Image)(resources.GetObject("buttonUnderlineEndTag.Image")));
            this.buttonUnderlineEndTag.Label = "Bitiş";
            this.buttonUnderlineEndTag.Name = "buttonUnderlineEndTag";
            this.buttonUnderlineEndTag.ShowImage = true;
            this.buttonUnderlineEndTag.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonUnderlineEndTag_Click);
            // 
            // buttonRule
            // 
            this.buttonRule.Image = ((System.Drawing.Image)(resources.GetObject("buttonRule.Image")));
            this.buttonRule.Label = "Add Form Rule";
            this.buttonRule.Name = "buttonRule";
            this.buttonRule.ShowImage = true;
            this.buttonRule.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonRule_Click);
            // 
            // group3
            // 
            this.group3.Items.Add(this.buttonOpenForms);
            this.group3.Items.Add(this.buttonCompanyDefination);
            this.group3.Label = "Digital Formlarım";
            this.group3.Name = "group3";
            // 
            // buttonOpenForms
            // 
            this.buttonOpenForms.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenForms.Image")));
            this.buttonOpenForms.Label = "Formlarım";
            this.buttonOpenForms.Name = "buttonOpenForms";
            this.buttonOpenForms.ShowImage = true;
            this.buttonOpenForms.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonOpenForms_Click);
            // 
            // buttonCompanyDefination
            // 
            this.buttonCompanyDefination.Image = ((System.Drawing.Image)(resources.GetObject("buttonCompanyDefination.Image")));
            this.buttonCompanyDefination.Label = "Adres Tanımları";
            this.buttonCompanyDefination.Name = "buttonCompanyDefination";
            this.buttonCompanyDefination.ShowImage = true;
            // 
            // groupStandart
            // 
            this.groupStandart.Items.Add(this.buttonToday);
            this.groupStandart.Items.Add(this.buttonTodayOne);
            this.groupStandart.Items.Add(this.buttonTomorrow);
            this.groupStandart.Label = "Standart Alanlar";
            this.groupStandart.Name = "groupStandart";
            // 
            // buttonToday
            // 
            this.buttonToday.Image = ((System.Drawing.Image)(resources.GetObject("buttonToday.Image")));
            this.buttonToday.Label = "Bugünün Tarihi";
            this.buttonToday.Name = "buttonToday";
            this.buttonToday.ShowImage = true;
            this.buttonToday.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonToday_Click);
            // 
            // buttonTodayOne
            // 
            this.buttonTodayOne.Image = ((System.Drawing.Image)(resources.GetObject("buttonTodayOne.Image")));
            this.buttonTodayOne.Label = "Dünün Tarihi";
            this.buttonTodayOne.Name = "buttonTodayOne";
            this.buttonTodayOne.ShowImage = true;
            this.buttonTodayOne.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonTodayOne_Click);
            // 
            // buttonTomorrow
            // 
            this.buttonTomorrow.Image = ((System.Drawing.Image)(resources.GetObject("buttonTomorrow.Image")));
            this.buttonTomorrow.Label = "Yarın";
            this.buttonTomorrow.Name = "buttonTomorrow";
            this.buttonTomorrow.ShowImage = true;
            this.buttonTomorrow.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonTomorrow_Click);
            // 
            // RibbonDigital
            // 
            this.Name = "RibbonDigital";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.RibbonDigital_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.groupTagFields.ResumeLayout(false);
            this.groupTagFields.PerformLayout();
            this.groupUnderline.ResumeLayout(false);
            this.groupUnderline.PerformLayout();
            this.buttonGroup1.ResumeLayout(false);
            this.buttonGroup1.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.groupStandart.ResumeLayout(false);
            this.groupStandart.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonLogin;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupTagFields;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox comboBoxFormDefination;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonOpenForms;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonCompanyDefination;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox comboBoxFormGroups;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox comboBoxDefinationFieldTags;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonAddTag;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonRemoveTag;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupUnderline;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox comboBoxUnderlineRule;
        internal Microsoft.Office.Tools.Ribbon.RibbonButtonGroup buttonGroup1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonUnderlineStartTag;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonUnderlineEndTag;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonRule;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonValidateFields;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupStandart;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonToday;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonTodayOne;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonTomorrow;
    }

    partial class ThisRibbonCollection
    {
        internal RibbonDigital RibbonDigital
        {
            get { return this.GetRibbon<RibbonDigital>(); }
        }
    }
}
