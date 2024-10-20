using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Tools.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using Bookmark = Microsoft.Office.Tools.Word.Bookmark;
using DigitalArchive.Api;

namespace CustomPortalV2.OfficeAddIn
{
    public partial class RibbonDigital
    {
        SoftImageChangeFormatRule[] softImageChangeFormatRules = null;
        private void RibbonDigital_Load(object sender, RibbonUIEventArgs e)
        {


            // Document extendedDocument = Globals.Factory.GetVstoObject(this.Application.ActiveDocument);
        }

        private void buttonLogin_Click(object sender, RibbonControlEventArgs e)
        {

            try
            {
                FormLogin formLogin = new FormLogin();
                if (formLogin.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    comboBoxFormDefination.Items.Clear();
                    var formDefinations = AppHelper.restHelper.GetFormDefinations();
                    foreach (var item in formDefinations.returnobject)
                    {
                        RibbonDropDownItem ribbonDropDown = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                        ribbonDropDown.Tag = item.Id;
                        ribbonDropDown.Label = item.FormName;
                        comboBoxFormDefination.Items.Add(ribbonDropDown);

                    }

                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        private void buttonFieldList_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {


                /* FormFieldList formFieldList = new FormFieldList(formDefinationId);
                 formFieldList.Show();
                 */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBoxFormDefination_TextChanged(object sender, RibbonControlEventArgs e)
        {
            var selectedItem = comboBoxFormDefination.Items.FirstOrDefault(s => s.Label == comboBoxFormDefination.Text);

            var formDefinationId = (int)selectedItem.Tag;
            var formGroups = AppHelper.restHelper.GetFormDefinationGroup(formDefinationId);
            comboBoxFormGroups.Items.Clear();
            comboBoxFormGroups.Text = string.Empty;
            if (formGroups.iserror)
            {
                ShowMessage(formGroups.message);
                return;
            }
            foreach (var item in formGroups.returnobject)
            {
                RibbonDropDownItem ribbonDropDown = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                ribbonDropDown.Tag = item.Id;
                ribbonDropDown.Label = item.FormNumber + " " + item.Name;
                comboBoxFormGroups.Items.Add(ribbonDropDown);

            }

            var formSoftRule = AppHelper.restHelper.GetFormSoftImageRule(formDefinationId);
            if (formSoftRule.iserror)
            {
                ShowMessage(formSoftRule.message);
                return;
            }

            comboBoxUnderlineRule.Items.Clear();
            comboBoxUnderlineRule.Text = String.Empty;
            softImageChangeFormatRules = formSoftRule.returnobject;
            foreach (var item in formSoftRule.returnobject)
            {
                RibbonDropDownItem ribbonDropDown = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                ribbonDropDown.Tag = item.Id;
                ribbonDropDown.Label = $"{item.FieldName} {item.FieldValue}  {item.StartTag} - {item.EndTag}";
                comboBoxUnderlineRule.Items.Add(ribbonDropDown);

            }

        }
        private void ShowMessage(string errormMessage)
        {
            MessageBox.Show(errormMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonOpenForms_Click(object sender, RibbonControlEventArgs e)
        {
            FormMyDigitalForms formMyDigitalForms = new FormMyDigitalForms();
            formMyDigitalForms.Show();
        }

        private void comboBoxFormGroups_TextChanged(object sender, RibbonControlEventArgs e)
        {
            var selectedItem = comboBoxFormGroups.Items.FirstOrDefault(s => s.Label == comboBoxFormGroups.Text);

            var formGroupId = (int)selectedItem.Tag;
            var formGroupFields = AppHelper.restHelper.GetFormDefinationField(formGroupId);
            comboBoxDefinationFieldTags.Items.Clear();
            comboBoxDefinationFieldTags.Text = string.Empty;
            if (formGroupFields.iserror)
            {
                ShowMessage(formGroupFields.message);
                return;
            }
            foreach (var item in formGroupFields.returnobject)
            {
                if (item.ControlType == "CheckBox" || item.ControlType == "RadioBox")
                {
                    var comboItems = AppHelper.restHelper.GetComboBoxItems(item.TagName);
                    foreach (var comboItem in comboItems.returnobject)
                    {
                        RibbonDropDownItem ribbonDropDown = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                        ribbonDropDown.Tag = item.TagName + "_" + comboItem.TagName;
                        ribbonDropDown.Label = item.FieldCaption + "(" + comboItem.Name + ")";

                        comboBoxDefinationFieldTags.Items.Add(ribbonDropDown);
                    }
                }
                else
                {

                    RibbonDropDownItem ribbonDropDown = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                    ribbonDropDown.Tag = item.TagName;
                    ribbonDropDown.Label = item.FieldCaption;
                    comboBoxDefinationFieldTags.Items.Add(ribbonDropDown);
                }

            }
        }

        private void buttonAddTag_Click(object sender, RibbonControlEventArgs e)
        {
            if (comboBoxDefinationFieldTags.Text == String.Empty)
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

                    var selectedItem = comboBoxDefinationFieldTags.Items.FirstOrDefault(s => s.Label == comboBoxDefinationFieldTags.Text);
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

        private void buttonRemoveTag_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var doc = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveDocument);

                var selectedItem = comboBoxDefinationFieldTags.Items.FirstOrDefault(s => s.Label == comboBoxDefinationFieldTags.Text);
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
        private void buttonUnderlineStartTag_Click(object sender, RibbonControlEventArgs e)
        {
            var selectedItem = comboBoxUnderlineRule.Items.FirstOrDefault(s => s.Label == comboBoxUnderlineRule.Text);

            try
            {
                var id = Convert.ToInt32(selectedItem.Tag);

                var selectedRule = softImageChangeFormatRules.SingleOrDefault(s => s.Id == id);
                AddBookMark(selectedRule.StartTag);

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }

        }

        private void buttonUnderlineEndTag_Click(object sender, RibbonControlEventArgs e)
        {
            var selectedItem = comboBoxUnderlineRule.Items.FirstOrDefault(s => s.Label == comboBoxUnderlineRule.Text);

            try
            {
                var id = Convert.ToInt32(selectedItem.Tag);

                var selectedRule = softImageChangeFormatRules.SingleOrDefault(s => s.Id == id);
                AddBookMark(selectedRule.EndTag);

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        private void buttonRule_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var selectedItem = comboBoxFormDefination.Items.FirstOrDefault(s => s.Label == comboBoxFormDefination.Text);

                var formDefinationId = (int)selectedItem.Tag;
                FormSoftImageChangeRule formSoftImageChange = new FormSoftImageChangeRule(formDefinationId);
                if (formSoftImageChange.ShowDialog() == DialogResult.OK)
                {

                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        private void buttonValidateFields_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var doc = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveDocument);
                var selectedItem = comboBoxFormDefination.Items.FirstOrDefault(s => s.Label == comboBoxFormDefination.Text);
                var formdefinationId = Convert.ToInt32(selectedItem.Tag);
                var formFieldList = AppHelper.restHelper.GetFormDefinationAllField(formdefinationId);

                string unSelectedField = "Bu alanları işaretlemediniz\n";
                bool haveUnTagged = false;
                foreach (var oneField in formFieldList.returnobject.Where(s => !s.Deleted.Value))
                {
                    if (oneField.ControlType == "CheckBox" || oneField.ControlType == "RadioBox")
                    {


                        var comboItems = AppHelper.restHelper.GetComboBoxItems(oneField.TagName);
                        foreach (var comboItem in comboItems.returnobject)
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

        private void buttonToday_Click(object sender, RibbonControlEventArgs e)
        {

            AddDateField("Today","Bügün");

        }
        private void AddDateField(string fieldName,string fieldCaption)
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

        private void buttonTodayOne_Click(object sender, RibbonControlEventArgs e)
        {
            AddDateField("Yesterday", "Dün");
        }

        private void buttonTomorrow_Click(object sender, RibbonControlEventArgs e)
        {
            AddDateField("Tomorrow", "Yarın");
        }
    }
}
