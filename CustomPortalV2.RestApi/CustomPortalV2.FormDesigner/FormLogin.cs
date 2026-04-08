
using CustomPortalV2.Utils;
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
    public partial class FormLogin : Form
    {
        RestApiHelper restHelper = null;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
             restHelper = new RestApiHelper(); 
  
            var loginInfo = restHelper.LoginUser(textBoxUserName.Text,textBoxPassword.Text,textBoxCompanyCode.Text);

            if (!loginInfo.IsLogin)
            {
                MessageBox.Show("Hatalı Kullanıcı adı ve şifre", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                AppHelper.restHelper = restHelper;
                DialogResult = DialogResult.OK;
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
