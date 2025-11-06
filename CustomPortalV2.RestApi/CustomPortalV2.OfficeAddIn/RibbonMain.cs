using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.Utils;
using DigitalArchive.OfficeAddIn;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomPortalV2.OfficeAddIn
{
    public partial class RibbonMain
    {
        Ini _inifile = new Ini("config.ini");
        RestApiHelper restApiHelper = null;
        List<FormDefination> formDefinations = new List<FormDefination>();
        private void RibbonMain_Load(object sender, RibbonUIEventArgs e)
        {

            var token = _inifile.GetValue("Token");
            restApiHelper = new RestApiHelper();
            restApiHelper.Token = token;
            labelUserName.Label = string.IsNullOrEmpty(token) ? "Not logged in" : _inifile.GetValue("UserName");
            if (!string.IsNullOrEmpty(token))
            {
                LoadFormDefination();
            }
        }

        private void buttonLogin_Click(object sender, RibbonControlEventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            if (formLogin.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                restApiHelper = new RestApiHelper();
                restApiHelper.Token = formLogin.SessionToken;
                _inifile.WriteValue("Token", formLogin.SessionToken);
                labelUserName.Label = _inifile.GetValue("UserName");
                LoadFormDefination();
            }
        }

        private void LoadFormDefination()
        {
            var formdefinationReturn = restApiHelper.GetFormDefination();
            formDefinations = formdefinationReturn.Data;
            foreach (var form in formDefinations)
            {
                RibbonDropDownItem downItem = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                downItem.Tag = form.Id;
                downItem.Label = form.FormName;
                comboBoxFormDefinations.Items.Add(downItem);
            }


        }
        private void ShowMessage(string errormMessage)
        {
            MessageBox.Show(errormMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void comboBoxFormDefinations_TextChanged(object sender, RibbonControlEventArgs e)
        {
            var selectedItem = comboBoxFormDefinations.Items.FirstOrDefault(s => s.Label == comboBoxFormDefinations.Text);

            var formDefinationId = (int)selectedItem.Tag;

            var formGroups = restApiHelper.GetFormDefinationGroup(formDefinationId);
            comboBoxFormGroup.Items.Clear();
            comboBoxFormGroup.Text = string.Empty;
            if (formGroups.ReturnCode != 1)
            {
                ShowMessage(formGroups.ReturnMessage);
                return;
            }
            foreach (var item in formGroups.Data)
            {
                RibbonDropDownItem ribbonDropDown = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                ribbonDropDown.Tag = item.Id;
                ribbonDropDown.Label = item.FormNumber + " " + item.Name;
                comboBoxFormGroup.Items.Add(ribbonDropDown);

            }


        }

        private void comboBoxFormGroup_TextChanged(object sender, RibbonControlEventArgs e)
        {
            var selectedItem = comboBoxFormGroup.Items.FirstOrDefault(s => s.Label == comboBoxFormGroup.Text);

            var formGroupId = (int)selectedItem.Tag;
            var formGroupFields = restApiHelper.GetFormGroupDefinationField(formGroupId);
            comboBoxFormFields.Items.Clear();
            comboBoxFormFields.Text = string.Empty;
            if (formGroupFields.ReturnCode != 1)
            {
                ShowMessage(formGroupFields.ReturnMessage);
                return;
            }
            foreach (var item in formGroupFields.Data)
            {
                if (item.ControlType == "CheckBox" || item.ControlType == "RadioBox")
                {
                    var comboItems = restApiHelper.GetComboBoxItems(item.TagName);
                    foreach (var comboItem in comboItems.Data)
                    {
                        RibbonDropDownItem ribbonDropDown = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                        ribbonDropDown.Tag = item.TagName + "_" + comboItem.TagName;
                        ribbonDropDown.Label = item.FieldCaption + "(" + comboItem.Name + ")";

                        comboBoxFormFields.Items.Add(ribbonDropDown);
                    }
                }
                else
                {

                    RibbonDropDownItem ribbonDropDown = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                    ribbonDropDown.Tag = item.TagName;
                    ribbonDropDown.Label = item.FieldCaption;
                    comboBoxFormFields.Items.Add(ribbonDropDown);
                }

            }

        }

        private void buttonAddBookMark_Click(object sender, RibbonControlEventArgs e)
        {
            if (comboBoxFormFields.Text == String.Empty)
            {
                ShowMessage("Bir Alan seçilmedi");
            }

            try
            {
                var doc = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveDocument);

                Bookmarks bookmarks = null;
                Bookmark myBookmark = null;
                object bookmarkRange = null;
                Selection selection = null;

                try
                {
                    selection = Globals.ThisAddIn.Application.Selection;
                    bookmarkRange = (object)selection.Range;
                    bookmarks = doc.Bookmarks;

                    var selectedItem = comboBoxFormFields.Items.FirstOrDefault(s => s.Label == comboBoxFormFields.Text);
                    string bookmarkName = selectedItem.Tag.ToString();
                    if (doc.Bookmarks.Exists(bookmarkName))
                    {
                        var dialog = MessageBox.Show($"{selectedItem.Label} Alan form üzerinden zaten mevcut \nTekrar Tanımlamak istediğinize emin misiniz", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dialog == DialogResult.Yes)
                        {
                            Random random = new Random();
                            for (int i = 0; i < 100; i++)
                            {
                                var nextId = random.Next(1, 200);
                                var newBookMarkName = $"Sayfa{nextId}_{bookmarkName}";
                                if (!doc.Bookmarks.Exists(newBookMarkName))
                                {
                                    bookmarkName = newBookMarkName;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    var newBookMark = bookmarks.Add(bookmarkName, ref bookmarkRange);
                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }

        }
        private void AddDateField(string fieldName, string fieldCaption)
        {
            string bookmarkName = fieldName;
            var doc = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveDocument);

            if (doc.Bookmarks.Exists(bookmarkName))
            {
                var dialog = MessageBox.Show($" {fieldCaption}ün Tarihi Alan form üzerinden zaten mevcut \nTekrar Tanımlamak istediğinize emin misiniz", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    Random random = new Random();
                    for (int i = 0; i < 100; i++)
                    {
                        var nextId = random.Next(1, 200);
                        var newBookMarkName = $"Sayfa{nextId}_{bookmarkName}";
                        if (!doc.Bookmarks.Exists(newBookMarkName))
                        {
                            bookmarkName = newBookMarkName;
                            break;
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            AddBookMark(bookmarkName);
        }

        private void AddBookMark(string bookmarkName)
        {
            var doc = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveDocument);
            Bookmarks bookmarks = null;
            object bookmarkRange = null;
            Selection selection = null;
            selection = Globals.ThisAddIn.Application.Selection;
            bookmarkRange = (object)selection.Range;
            bookmarks = doc.Bookmarks;
            if (doc.Bookmarks.Exists(bookmarkName))
                return;

            var newBookMark = bookmarks.Add(bookmarkName, ref bookmarkRange);
        }
        private void buttonRemoveBookMark_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var doc = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveDocument);

                var selectedItem = comboBoxFormFields.Items.FirstOrDefault(s => s.Label == comboBoxFormFields.Text);
                string bookmarkName = selectedItem.Tag.ToString();
                if (!doc.Bookmarks.Exists(bookmarkName))
                {
                    return;
                }
                var range = doc.Bookmarks[bookmarkName];

                range.Delete();
            }
            catch (Exception ex)
            {

                ShowMessage(ex.Message);
            }

        }

        private void buttonValidateBookMark_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var doc = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveDocument);
                var selectedItem = comboBoxFormDefinations.Items.FirstOrDefault(s => s.Label == comboBoxFormDefinations.Text);
                var formdefinationId = Convert.ToInt32(selectedItem.Tag);
                var formFieldList = restApiHelper.GetFormDefinationAllField(formdefinationId);

                string unSelectedField = "Bu alanları işaretlemediniz\n";
                bool haveUnTagged = false;
                foreach (var oneField in formFieldList.Data.Where(s => !s.Deleted))
                {
                    if (oneField.ControlType == "CheckBox" || oneField.ControlType == "RadioBox")
                    {


                        var comboItems = restApiHelper.GetComboBoxItems(oneField.TagName);
                        foreach (var comboItem in comboItems.Data)
                        {
                            string fieldTag = oneField.TagName + "_" + comboItem.TagName;
                            string caption = oneField.FieldCaption + "(" + comboItem.Name + ")";
                            if (!doc.Bookmarks.Exists(fieldTag))
                            {
                                unSelectedField += caption + "\n";
                                haveUnTagged = true;
                            }

                        }
                    }
                    else
                    {
                        if (!doc.Bookmarks.Exists(oneField.TagName))
                        {
                            unSelectedField += oneField.FieldCaption + "\n";
                            haveUnTagged = true;
                        }

                    }

                }

                if (haveUnTagged)
                {
                    MessageBox.Show(unSelectedField, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }

        }

        private void buttonAddToday_Click(object sender, RibbonControlEventArgs e)
        {
            AddDateField("Today", "Bügün");

        }

        private void buttonAddYesterday_Click(object sender, RibbonControlEventArgs e)
        {
            AddDateField("Yesterday", "Dün");
        }

        private void buttonAddTomorrow_Click(object sender, RibbonControlEventArgs e)
        {
            AddDateField("Tomorrow", "Yarın");
        }

        private void buttonDownloadServer_Click(object sender, RibbonControlEventArgs e)
        {

        }
    }
}
