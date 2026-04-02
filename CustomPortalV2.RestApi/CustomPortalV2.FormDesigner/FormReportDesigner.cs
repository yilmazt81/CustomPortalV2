using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
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
    public partial class FormReportDesigner : Form
    {
        string _layoutFileName = string.Empty;
        FormVersion _formVersion = null; 
        public FormReportDesigner(XtraReport report,string layout,FormVersion formVersion)
        {
            _layoutFileName= layout;
            _formVersion= formVersion;
            InitializeComponent();
            reportDesigner1.DesignPanelLoaded += ReportDesigner1_DesignPanelLoaded;
            reportDesigner1.OpenReport(report);
        }

        private void ReportDesigner1_DesignPanelLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {
            XRDesignPanel panel = (XRDesignPanel)sender;
            panel.AddCommandHandler(new SaveCommandHandler(panel, _layoutFileName, _formVersion));
        }
    }
}
