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

namespace CustomPortalV2.OfficeAddIn
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
            string apiUrl = "https://onlineislemler.adagumrukleme.com/";
            //string apiUrl = "https://customdigiform.istanbulyazilimofisi.com.tr/";
            restHelper = new RestHelper(apiUrl, "");

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
