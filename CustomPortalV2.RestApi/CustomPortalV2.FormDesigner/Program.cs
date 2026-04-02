using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomPortalV2.FormDesigner
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormLogin formLogin = new FormLogin();  
            if (formLogin.ShowDialog() == DialogResult.OK)
            {

                Application.Run(new FormMain());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
