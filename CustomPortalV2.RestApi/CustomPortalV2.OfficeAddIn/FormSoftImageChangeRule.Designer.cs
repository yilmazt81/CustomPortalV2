namespace CustomPortalV2.OfficeAddIn
{
    partial class FormSoftImageChangeRule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSoftImageChangeRule));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxFieldList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFieldValue = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Alan Adı";
            // 
            // comboBoxFieldList
            // 
            this.comboBoxFieldList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFieldList.FormattingEnabled = true;
            this.comboBoxFieldList.Location = new System.Drawing.Point(146, 29);
            this.comboBoxFieldList.Name = "comboBoxFieldList";
            this.comboBoxFieldList.Size = new System.Drawing.Size(281, 28);
            this.comboBoxFieldList.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Alan Değeri";
            // 
            // textBoxFieldValue
            // 
            this.textBoxFieldValue.Location = new System.Drawing.Point(146, 84);
            this.textBoxFieldValue.Name = "textBoxFieldValue";
            this.textBoxFieldValue.Size = new System.Drawing.Size(281, 27);
            this.textBoxFieldValue.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(146, 135);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(281, 27);
            this.textBox1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Başlangıç Tag";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(146, 183);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(281, 27);
            this.textBox2.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Bitiş Tag";
            // 
            // buttonOk
            // 
            this.buttonOk.Image = ((System.Drawing.Image)(resources.GetObject("buttonOk.Image")));
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(305, 271);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(122, 33);
            this.buttonOk.TabIndex = 8;
            this.buttonOk.Text = "Tamam";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(160, 271);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(122, 33);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "İptal";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormSoftImageChangeRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 327);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxFieldValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxFieldList);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FormSoftImageChangeRule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dokuman Değişim kuralları";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxFieldList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxFieldValue;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}