 
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bookmark = Microsoft.Office.Tools.Word.Bookmark;

namespace CustomPortalV2.OfficeAddIn
{
    public partial class FormFieldList : Form
    {
        int formDefinationId;
        public FormFieldList(int pformdefinationId)
        {
            InitializeComponent();
            formDefinationId = pformdefinationId;

            var formGroups = AppHelper.restHelper.GetFormDefinationGroup(formDefinationId);

            foreach (var formg in formGroups.returnobject)
            {
                ListViewItem item = new ListViewItem();
                item.Text = formg.FormNumber;
                item.Tag= formg;
                item.SubItems.Add(formg.Name);
                listViewGroupList.Items.Add(item);
            }
        }

        private void LoadFormField(int formGroupId)
        {
            listViewFieldList.Items.Clear();

            var fieldList = AppHelper.restHelper.GetFormDefinationField(formGroupId);
            foreach (var formg in fieldList.returnobject.Where(s => !s.Deleted.Value))
            {
                ListViewItem item = new ListViewItem();
                item.Text = formg.FieldCaption;
                item.Tag = formg;
                item.SubItems.Add(formg.TagName);
                listViewFieldList.Items.Add(item);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {

            var doc = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveDocument);
            try
            {
                Bookmarks bookmarks = null;
                Bookmark myBookmark = null;
                object bookmarkRange = null;
                Selection selection = null;

                try
                {
                    selection = Globals.ThisAddIn.Application.Selection;
                    bookmarkRange = (object)selection.Range;
                    bookmarks = doc.Bookmarks;
                    var ddd = bookmarks.Add("MyBookmark", ref bookmarkRange);
                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void listViewGroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewGroupList.SelectedItems.Count == 0)
                return;

            try
            {
                var selectedGroup=(FormDefinationGroup)listViewGroupList.SelectedItems[0].Tag;
                LoadFormField(selectedGroup.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
