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
    public partial class FormSoftImageChangeRule : Form
    {
        int formdefinationId = 0;
        public FormSoftImageChangeRule(int pformDefinationTypeId)
        {
            InitializeComponent();

            formdefinationId = pformDefinationTypeId;

            var formGroups = AppHelper.restHelper.GetFormDefinationAllField(pformDefinationTypeId);
           
            foreach (var formg in formGroups.returnobject)
            {
                
                comboBoxFieldList.Items.Add(formg);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {


            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {


            DialogResult=DialogResult.Cancel;
        }
    }
}
