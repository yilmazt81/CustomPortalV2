namespace CustomPortalV2.FormDesigner
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxFormDefination = new System.Windows.Forms.ComboBox();
            this.buttonNewVersion = new System.Windows.Forms.Button();
            this.tabControlFormVersions = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonVersionEdit = new System.Windows.Forms.Button();
            this.comboBoxFormVersion = new System.Windows.Forms.ComboBox();
            this.buttonCreateFormBranch = new System.Windows.Forms.Button();
            this.tabControlFormVersions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dokuman Tipi";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // comboBoxFormDefination
            // 
            this.comboBoxFormDefination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormDefination.FormattingEnabled = true;
            this.comboBoxFormDefination.Location = new System.Drawing.Point(153, 30);
            this.comboBoxFormDefination.Name = "comboBoxFormDefination";
            this.comboBoxFormDefination.Size = new System.Drawing.Size(329, 26);
            this.comboBoxFormDefination.TabIndex = 1;
            this.comboBoxFormDefination.SelectedIndexChanged += new System.EventHandler(this.comboBoxFormDefination_SelectedIndexChanged);
            // 
            // buttonNewVersion
            // 
            this.buttonNewVersion.Location = new System.Drawing.Point(509, 77);
            this.buttonNewVersion.Name = "buttonNewVersion";
            this.buttonNewVersion.Size = new System.Drawing.Size(70, 26);
            this.buttonNewVersion.TabIndex = 3;
            this.buttonNewVersion.Text = "Yeni";
            this.buttonNewVersion.UseVisualStyleBackColor = true;
            this.buttonNewVersion.Click += new System.EventHandler(this.buttonMainDefination_Click);
            // 
            // tabControlFormVersions
            // 
            this.tabControlFormVersions.Controls.Add(this.tabPage1);
            this.tabControlFormVersions.Controls.Add(this.tabPage2);
            this.tabControlFormVersions.Location = new System.Drawing.Point(35, 127);
            this.tabControlFormVersions.Name = "tabControlFormVersions";
            this.tabControlFormVersions.SelectedIndex = 0;
            this.tabControlFormVersions.Size = new System.Drawing.Size(747, 224);
            this.tabControlFormVersions.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(739, 193);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Şubelere Göre Versiyon";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(739, 193);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Form Version";
            // 
            // buttonVersionEdit
            // 
            this.buttonVersionEdit.Location = new System.Drawing.Point(599, 77);
            this.buttonVersionEdit.Name = "buttonVersionEdit";
            this.buttonVersionEdit.Size = new System.Drawing.Size(70, 26);
            this.buttonVersionEdit.TabIndex = 6;
            this.buttonVersionEdit.Text = "Değiştir";
            this.buttonVersionEdit.UseVisualStyleBackColor = true;
            this.buttonVersionEdit.Click += new System.EventHandler(this.buttonVersionEdit_Click);
            // 
            // comboBoxFormVersion
            // 
            this.comboBoxFormVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormVersion.FormattingEnabled = true;
            this.comboBoxFormVersion.Location = new System.Drawing.Point(153, 77);
            this.comboBoxFormVersion.Name = "comboBoxFormVersion";
            this.comboBoxFormVersion.Size = new System.Drawing.Size(329, 26);
            this.comboBoxFormVersion.TabIndex = 7;
            // 
            // buttonCreateFormBranch
            // 
            this.buttonCreateFormBranch.Location = new System.Drawing.Point(509, 109);
            this.buttonCreateFormBranch.Name = "buttonCreateFormBranch";
            this.buttonCreateFormBranch.Size = new System.Drawing.Size(211, 26);
            this.buttonCreateFormBranch.TabIndex = 8;
            this.buttonCreateFormBranch.Text = "Şube için Özel Oluştur";
            this.buttonCreateFormBranch.UseVisualStyleBackColor = true;
            this.buttonCreateFormBranch.Click += new System.EventHandler(this.buttonCreateFormBranch_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 363);
            this.Controls.Add(this.buttonCreateFormBranch);
            this.Controls.Add(this.comboBoxFormVersion);
            this.Controls.Add(this.buttonVersionEdit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControlFormVersions);
            this.Controls.Add(this.buttonNewVersion);
            this.Controls.Add(this.comboBoxFormDefination);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.tabControlFormVersions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxFormDefination;
        private System.Windows.Forms.Button buttonNewVersion;
        private System.Windows.Forms.TabControl tabControlFormVersions;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonVersionEdit;
        private System.Windows.Forms.ComboBox comboBoxFormVersion;
        private System.Windows.Forms.Button buttonCreateFormBranch;
    }
}