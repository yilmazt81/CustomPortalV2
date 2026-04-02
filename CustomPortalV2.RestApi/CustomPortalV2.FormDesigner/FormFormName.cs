using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomPortalV2.FormDesigner
{
    public partial class FormFormName : Form
    {
        public FormFormName()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult= DialogResult.Cancel;
        }

        public string FormName { get { return textBoxFormName.Text; } set { textBoxFormName.Text = value; } }
        private void buttonOk_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.OK;
        }
    }
}
