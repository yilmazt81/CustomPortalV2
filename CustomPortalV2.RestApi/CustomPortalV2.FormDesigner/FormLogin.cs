using DigitalArchive.Api;
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
        RestHelper restHelper = null;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
             restHelper = new RestHelper("https://customdigiform.istanbulyazilimofisi.com.tr/", "");
            //restHelper = new RestHelper("https://onlineislemler.adagumrukleme.com/", "");
 

            var loginInfo = restHelper.LoginUser(textBoxUserName.Text, textBoxPassword.Text);

            if (loginInfo.iserror)
            {
                MessageBox.Show("Hatalı Kullanıcı adı ve şifre", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                AppHelper.restHelper = restHelper;
                DialogResult = DialogResult.OK;
            }

        }
    }
}
