namespace TranslateApp
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
            this.textBoxSourceResource = new System.Windows.Forms.TextBox();
            this.buttonOpenSourceResource = new System.Windows.Forms.Button();
            this.buttonTargetResource = new System.Windows.Forms.Button();
            this.textBoxTargetFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxSource = new System.Windows.Forms.ComboBox();
            this.comboBoxTarget = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kaynak Resource";
            // 
            // textBoxSourceResource
            // 
            this.textBoxSourceResource.Location = new System.Drawing.Point(182, 36);
            this.textBoxSourceResource.Name = "textBoxSourceResource";
            this.textBoxSourceResource.Size = new System.Drawing.Size(394, 27);
            this.textBoxSourceResource.TabIndex = 1;
            // 
            // buttonOpenSourceResource
            // 
            this.buttonOpenSourceResource.AccessibleRole = System.Windows.Forms.AccessibleRole.RowHeader;
            this.buttonOpenSourceResource.Location = new System.Drawing.Point(582, 36);
            this.buttonOpenSourceResource.Name = "buttonOpenSourceResource";
            this.buttonOpenSourceResource.Size = new System.Drawing.Size(36, 27);
            this.buttonOpenSourceResource.TabIndex = 2;
            this.buttonOpenSourceResource.Text = "...";
            this.buttonOpenSourceResource.UseVisualStyleBackColor = true;
            this.buttonOpenSourceResource.Click += new System.EventHandler(this.buttonOpenSourceResource_Click);
            // 
            // buttonTargetResource
            // 
            this.buttonTargetResource.AccessibleRole = System.Windows.Forms.AccessibleRole.RowHeader;
            this.buttonTargetResource.Location = new System.Drawing.Point(582, 90);
            this.buttonTargetResource.Name = "buttonTargetResource";
            this.buttonTargetResource.Size = new System.Drawing.Size(36, 27);
            this.buttonTargetResource.TabIndex = 5;
            this.buttonTargetResource.Text = "...";
            this.buttonTargetResource.UseVisualStyleBackColor = true;
            this.buttonTargetResource.Click += new System.EventHandler(this.buttonTargetResource_Click);
            // 
            // textBoxTargetFile
            // 
            this.textBoxTargetFile.Location = new System.Drawing.Point(182, 90);
            this.textBoxTargetFile.Name = "textBoxTargetFile";
            this.textBoxTargetFile.Size = new System.Drawing.Size(394, 27);
            this.textBoxTargetFile.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 90);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hedef Resource";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(531, 161);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(122, 46);
            this.buttonStart.TabIndex = 6;
            this.buttonStart.Text = "Başla";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 176);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Kaynak";
            // 
            // comboBoxSource
            // 
            this.comboBoxSource.FormattingEnabled = true;
            this.comboBoxSource.Location = new System.Drawing.Point(123, 171);
            this.comboBoxSource.Name = "comboBoxSource";
            this.comboBoxSource.Size = new System.Drawing.Size(121, 28);
            this.comboBoxSource.TabIndex = 8;
            // 
            // comboBoxTarget
            // 
            this.comboBoxTarget.FormattingEnabled = true;
            this.comboBoxTarget.Location = new System.Drawing.Point(344, 171);
            this.comboBoxTarget.Name = "comboBoxTarget";
            this.comboBoxTarget.Size = new System.Drawing.Size(121, 28);
            this.comboBoxTarget.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(264, 176);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Hedef";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 321);
            this.Controls.Add(this.comboBoxTarget);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxSource);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonTargetResource);
            this.Controls.Add(this.textBoxTargetFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonOpenSourceResource);
            this.Controls.Add(this.textBoxSourceResource);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSourceResource;
        private System.Windows.Forms.Button buttonOpenSourceResource;
        private System.Windows.Forms.Button buttonTargetResource;
        private System.Windows.Forms.TextBox textBoxTargetFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxSource;
        private System.Windows.Forms.ComboBox comboBoxTarget;
        private System.Windows.Forms.Label label4;
    }
}

