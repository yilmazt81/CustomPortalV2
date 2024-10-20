namespace CustomPortalV2.OfficeAddIn
{
    partial class FormFieldList
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
            this.listViewGroupList = new System.Windows.Forms.ListView();
            this.hGroupNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hGroupHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewFieldList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewGroupList
            // 
            this.listViewGroupList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hGroupNo,
            this.hGroupHeader});
            this.listViewGroupList.FullRowSelect = true;
            this.listViewGroupList.GridLines = true;
            this.listViewGroupList.HideSelection = false;
            this.listViewGroupList.Location = new System.Drawing.Point(12, 41);
            this.listViewGroupList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listViewGroupList.Name = "listViewGroupList";
            this.listViewGroupList.Size = new System.Drawing.Size(246, 540);
            this.listViewGroupList.TabIndex = 0;
            this.listViewGroupList.UseCompatibleStateImageBehavior = false;
            this.listViewGroupList.View = System.Windows.Forms.View.Details;
            this.listViewGroupList.SelectedIndexChanged += new System.EventHandler(this.listViewGroupList_SelectedIndexChanged);
            // 
            // hGroupNo
            // 
            this.hGroupNo.Text = "Group No";
            this.hGroupNo.Width = 90;
            // 
            // hGroupHeader
            // 
            this.hGroupHeader.Text = "Group Başlığı";
            this.hGroupHeader.Width = 112;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.listViewGroupList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewFieldList);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(789, 593);
            this.splitContainer1.SplitterDistance = 263;
            this.splitContainer1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Form Groupları";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Form alanları";
            // 
            // listViewFieldList
            // 
            this.listViewFieldList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFieldList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewFieldList.FullRowSelect = true;
            this.listViewFieldList.GridLines = true;
            this.listViewFieldList.HideSelection = false;
            this.listViewFieldList.Location = new System.Drawing.Point(7, 41);
            this.listViewFieldList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listViewFieldList.Name = "listViewFieldList";
            this.listViewFieldList.Size = new System.Drawing.Size(298, 381);
            this.listViewFieldList.TabIndex = 2;
            this.listViewFieldList.UseCompatibleStateImageBehavior = false;
            this.listViewFieldList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Alan Başlığı";
            this.columnHeader1.Width = 125;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Alan Adı";
            this.columnHeader2.Width = 155;
            // 
            // FormFieldList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 593);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormFieldList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alan Listesi";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewGroupList;
        private System.Windows.Forms.ColumnHeader hGroupNo;
        private System.Windows.Forms.ColumnHeader hGroupHeader;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewFieldList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}