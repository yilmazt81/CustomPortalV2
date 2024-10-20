namespace CustomPortalV2.OfficeAddIn
{
    partial class FormFontProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFontProperty));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numUpDownFontSize = new System.Windows.Forms.NumericUpDown();
            this.checkBoxBold = new System.Windows.Forms.CheckBox();
            this.checkBoxItalic = new System.Windows.Forms.CheckBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxAttachment = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 91);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Yer işareti Adı";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(137, 88);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(202, 27);
            this.textBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 140);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Font Boyutu";
            // 
            // numUpDownFontSize
            // 
            this.numUpDownFontSize.Location = new System.Drawing.Point(137, 140);
            this.numUpDownFontSize.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numUpDownFontSize.Name = "numUpDownFontSize";
            this.numUpDownFontSize.Size = new System.Drawing.Size(120, 27);
            this.numUpDownFontSize.TabIndex = 3;
            this.numUpDownFontSize.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // checkBoxBold
            // 
            this.checkBoxBold.AutoSize = true;
            this.checkBoxBold.Location = new System.Drawing.Point(137, 187);
            this.checkBoxBold.Name = "checkBoxBold";
            this.checkBoxBold.Size = new System.Drawing.Size(68, 24);
            this.checkBoxBold.TabIndex = 4;
            this.checkBoxBold.Text = "Kalın";
            this.checkBoxBold.UseVisualStyleBackColor = true;
            // 
            // checkBoxItalic
            // 
            this.checkBoxItalic.AutoSize = true;
            this.checkBoxItalic.Location = new System.Drawing.Point(137, 228);
            this.checkBoxItalic.Name = "checkBoxItalic";
            this.checkBoxItalic.Size = new System.Drawing.Size(66, 24);
            this.checkBoxItalic.TabIndex = 5;
            this.checkBoxItalic.Text = "italic";
            this.checkBoxItalic.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Image = ((System.Drawing.Image)(resources.GetObject("buttonOk.Image")));
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(252, 266);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(122, 34);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "Tamam";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(112, 266);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(122, 34);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "İptal";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Ek Türü";
            // 
            // comboBoxAttachment
            // 
            this.comboBoxAttachment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAttachment.FormattingEnabled = true;
            this.comboBoxAttachment.Location = new System.Drawing.Point(137, 34);
            this.comboBoxAttachment.Name = "comboBoxAttachment";
            this.comboBoxAttachment.Size = new System.Drawing.Size(202, 28);
            this.comboBoxAttachment.TabIndex = 9;
            // 
            // FormFontProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 317);
            this.Controls.Add(this.comboBoxAttachment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.checkBoxItalic);
            this.Controls.Add(this.checkBoxBold);
            this.Controls.Add(this.numUpDownFontSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "FormFontProperty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Font Özellikleri";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numUpDownFontSize;
        private System.Windows.Forms.CheckBox checkBoxBold;
        private System.Windows.Forms.CheckBox checkBoxItalic;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxAttachment;
    }
}