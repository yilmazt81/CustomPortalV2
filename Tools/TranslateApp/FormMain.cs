using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TranslateApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            comboBoxSource.Items.Add("tr");
            comboBoxSource.Items.Add("en");


            comboBoxTarget.Items.Add("en");
            comboBoxTarget.Items.Add("ru");

            comboBoxSource.SelectedIndex = 0;
            comboBoxTarget.SelectedIndex = 0;


        }

        private void buttonOpenSourceResource_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ResourceFile|*.js";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxSourceResource.Text = ofd.FileName;
            }
        }

        private void buttonTargetResource_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ResourceFile|*.js";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxTargetFile.Text = ofd.FileName;
            }
        }
        private string GetKey(string line)
        {
            var parts=line.Split(':');
            

            return parts[0];
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                var linesSource = File.ReadAllText(textBoxSourceResource.Text, Encoding.UTF8);
                var linesTarget = File.ReadAllText(textBoxTargetFile.Text, Encoding.UTF8);

                foreach (var sourceOne in linesSource)
                { 

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex);
            }
        }
    }
}
