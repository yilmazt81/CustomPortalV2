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

namespace DigitalArchive.OfficeAddIn
{
    public partial class FormLogin : Form
    {
        RestApiHelper restHelper = null;
        Ini _inifile = new Ini("config.ini");
       
        public string SessionToken { get; set; }
        public FormLogin()
        {
            InitializeComponent();

            textBoxCompanyCode.Text = _inifile.GetValue("CompanyCode");
            textBoxUserName.Text = _inifile.GetValue("UserName");
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            restHelper = new RestApiHelper();

            var loginInfo = restHelper.LoginUser(textBoxUserName.Text, textBoxPassword.Text, textBoxCompanyCode.Text);

            if (!loginInfo.IsLogin)
            {
                MessageBox.Show("Hatalı Kullanıcı adı ve şifre", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                _inifile.WriteValue("CompanyCode", textBoxCompanyCode.Text);
                _inifile.WriteValue("UserName", textBoxUserName.Text);
                _inifile.WriteValue("Token", loginInfo.token);
                _inifile.Save();
                SessionToken = loginInfo.token;
                // RestApiHelper.restHelper = restHelper;
                DialogResult = DialogResult.OK;
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
