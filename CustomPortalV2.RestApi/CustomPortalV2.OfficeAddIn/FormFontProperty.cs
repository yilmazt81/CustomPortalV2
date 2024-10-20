 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomPortalV2.OfficeAddIn
{
    public partial class FormFontProperty : Form
    {
        public FormFontProperty(int formdefinationId)
        {
            InitializeComponent();

           var formAttachmentTypeList= AppHelper.restHelper.GetFormAttachmentTypes(formdefinationId);

            comboBoxAttachment.Items.Add(new FormAttachmentType()
            {
                FormName ="Ana Form"
            });
            foreach (var item in formAttachmentTypeList.returnobject)
            {
                comboBoxAttachment.Items.Add(item);
            }
            comboBoxAttachment.SelectedIndex = 0;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.OK;
        }
    }
}
