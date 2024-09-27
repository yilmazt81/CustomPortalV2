using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TranslateApp
{
    public partial class FormMain : Form
    {

        string serviceEndPoint = "https://localhost:7232/api/AppLang/TranslateText";
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

        public TranslateTextReturn TranslateTex(TranslateTextDTO translateTextDTO)
        {
           var  client = new HttpClient();

            var jsonClass = JsonConvert.SerializeObject(translateTextDTO);
            var content = new StringContent(jsonClass, Encoding.UTF8, "application/json");
            TranslateTextReturn requestReturn;

            var result = client.PostAsync(serviceEndPoint, content).Result;
            if (result.IsSuccessStatusCode)
            {
                var str = result.Content.ReadAsStringAsync().Result;
                requestReturn = JsonConvert.DeserializeObject<TranslateTextReturn>(str);

                return requestReturn;
            }
            else
            {
                throw new Exception("Request Return Code : " + result.StatusCode);

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
            var parts = line.Split(':');
            if (parts.Length != 2)
            {
                return "";
            }
            return parts[0].Trim();
        }


        private string GetValue(string line)
        {
            var parts = line.Split(':');
            return parts[1].Trim();
        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                var linesSource = File.ReadAllLines(textBoxSourceResource.Text, Encoding.UTF8).ToList();
                var linesTarget = File.ReadAllLines(textBoxTargetFile.Text, Encoding.UTF8).ToList();

                foreach (var sourceOne in linesSource)
                {
                    var groupTag = GetKey(sourceOne);
                    if (string.IsNullOrEmpty(groupTag))
                        continue;

                    if (!linesTarget.Any(s => GetKey(s) == groupTag))
                    {
                        var txtValue = GetValue(sourceOne);

                        var textTranslate = TranslateTex(new TranslateTextDTO() { SourceLangeuage = comboBoxSource.Text, TargetLanguage = comboBoxTarget.Text, Text = txtValue.Replace('"', ' ').Replace(',',' ').Trim() });
                        if (textTranslate.translations.Length != 0)
                        {
                            linesTarget.Insert(linesTarget.Count - 2, $"{groupTag}:\"{textTranslate.translations[0].text}\",");
                        }
                        else
                        {
                            linesTarget.Insert(linesTarget.Count - 2, $"{groupTag}:\"NONETRANSLATE\",");
                        }
                    }
                }

                File.WriteAllLines(textBoxTargetFile.Text, linesTarget, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
