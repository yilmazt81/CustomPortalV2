using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using DigitalArchive.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomPortalV2.FormDesigner
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            comboBoxFormDefination.ValueMember = "Id";
            comboBoxFormDefination.DisplayMember = "FormName";
            var returnService = AppHelper.restHelper.GetFormDefinations(); ;
            comboBoxFormDefination.DataSource = returnService.returnobject;

        }
        private DataTable GetDataTable(FormDefinationField[] formDefinationFields)
        {
            DataTable dataTable = new DataTable();

            foreach (var formDefinationField in formDefinationFields.Where(s => !s.Deleted.Value))
            {



                if (formDefinationField.ControlType == "CheckBox" || formDefinationField.ControlType == "RadioBox")
                {

                    var checkbox = AppHelper.restHelper.GetComboBoxItems(formDefinationField.TagName);
                    foreach (var comboBoxItem in checkbox.returnobject)
                    {
                        dataTable.Columns.Add(formDefinationField.TagName + "_" + comboBoxItem.TagName, typeof(string));
                    }

                }
                else
                {
                    dataTable.Columns.Add(formDefinationField.TagName, typeof(string));
                }
                dataTable.AcceptChanges();
            }
            for (int i = 0; i < 1; i++)
            {

                DataRow dataRow = dataTable.NewRow();
                foreach (var formDefinationField in formDefinationFields.Where(s => !s.Deleted.Value))
                {
                    try
                    {
                        if (formDefinationField.ControlType == "DateTime")
                        {
                            dataRow[formDefinationField.TagName] = DateTime.Now.ToString("dd.MM.yyyyy");
                        }
                        else if (formDefinationField.ControlType == "CheckBox" || formDefinationField.ControlType == "RadioBox")
                        {

                            var checkbox = AppHelper.restHelper.GetComboBoxItems(formDefinationField.TagName);
                            foreach (var comboBoxItem in checkbox.returnobject)
                            {
                                dataRow[formDefinationField.TagName + "_" + comboBoxItem.TagName] = "X";
                            }
                        }
                        else
                        {
                            dataRow[formDefinationField.TagName] = formDefinationField.TagName + "Test  Test ";

                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }

                }
                dataTable.Rows.Add(dataRow);
                dataTable.AcceptChanges();
            }

            return dataTable;
        }
        private void buttonMainDefination_Click(object sender, EventArgs e)
        {
            try
            {
                var formDefinationId = (FormDefination)comboBoxFormDefination.SelectedItem;

                var formdefinationFieldsReturn = AppHelper.restHelper.GetFormDefinationAllField(formDefinationId.Id);

                if (formdefinationFieldsReturn.iserror)
                {
                    throw new Exception(formdefinationFieldsReturn.message);
                }
                XtraReport xtraReport = new XtraReport();
                xtraReport.DataSource = GetDataTable(formdefinationFieldsReturn.returnobject);
                FormVersion formVersion = new FormVersion()
                {
                    FormDefinationId = formDefinationId.Id
                };
                FormDesigner.FormReportDesigner formDefination = new FormReportDesigner(xtraReport, String.Empty, formVersion);

                if (formDefination.ShowDialog() == DialogResult.OK)
                {

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxFormDefination_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                var formDefinationId = (FormDefination)comboBoxFormDefination.SelectedItem;

                var formVersionReturn = AppHelper.restHelper.GetFormVersions(formDefinationId.Id);
                if (formVersionReturn.iserror)
                {
                    throw new Exception(formVersionReturn.message);
                }

                var xmlVersionList = formVersionReturn.returnobject.Where(s => Path.GetExtension(s.FileName) == ".xml").ToArray();
                comboBoxFormVersion.ValueMember = "Id";
                comboBoxFormVersion.DisplayMember = "FormLanguage";
                comboBoxFormVersion.DataSource = xmlVersionList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void buttonVersionEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var formDefinationId = (FormDefination)comboBoxFormDefination.SelectedItem;

                var formdefinationFieldsReturn = AppHelper.restHelper.GetFormDefinationAllField(formDefinationId.Id);

                if (formdefinationFieldsReturn.iserror)
                {
                    throw new Exception(formdefinationFieldsReturn.message);
                }
                XtraReport xtraReport = new XtraReport();
                xtraReport.DataSource = GetDataTable(formdefinationFieldsReturn.returnobject);
                FormVersion formVersion = (FormVersion)comboBoxFormVersion.SelectedItem;

                var newVersion = AppHelper.restHelper.GetFormVersion(formVersion.Id);

                var formVersionPath = AppHelper.restHelper.ServiceUrl + newVersion.returnobject.FileName.Replace(@"\", "/");
                string layoutFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".xml");
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(new Uri(formVersionPath), layoutFile);
                }

                xtraReport.LoadLayoutFromXml(layoutFile);
                FormDesigner.FormReportDesigner formDefination = new FormReportDesigner(xtraReport, layoutFile, newVersion.returnobject);

                if (formDefination.ShowDialog() == DialogResult.OK)
                {
                    //comboBoxFormDefination_SelectedIndexChanged(sender, e);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void buttonCreateFormBranch_Click(object sender, EventArgs e)
        {
            try
            {
                var formDefinationId = (FormDefination)comboBoxFormDefination.SelectedItem;

                var formdefinationFieldsReturn = AppHelper.restHelper.GetFormDefinationAllField(formDefinationId.Id);

                if (formdefinationFieldsReturn.iserror)
                {
                    throw new Exception(formdefinationFieldsReturn.message);
                }
                XtraReport xtraReport = new XtraReport();
                xtraReport.DataSource = GetDataTable(formdefinationFieldsReturn.returnobject);
                FormVersion formVersion = (FormVersion)comboBoxFormVersion.SelectedItem;
                var formVersionPath = AppHelper.restHelper.ServiceUrl + formVersion.FileName.Replace(@"\", "/");
                string layoutFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".xml");
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(new Uri(formVersionPath), layoutFile);
                }

                xtraReport.LoadLayoutFromXml(layoutFile);
                FormDesigner.FormReportDesigner formDefination = new FormReportDesigner(xtraReport, layoutFile, formVersion);

                if (formDefination.ShowDialog() == DialogResult.OK)
                {

                    comboBoxFormDefination_SelectedIndexChanged(sender, e);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
