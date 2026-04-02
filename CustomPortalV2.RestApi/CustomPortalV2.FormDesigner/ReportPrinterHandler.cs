using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.XtraReports.UI; 
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraReports.UserDesigner;
using CustomPortalV2.Core.Model.FDefination;

namespace CustomPortalV2.FormDesigner
{
    internal class ReporPrinterHandler<T>
    {
        private XtraReport _xtraReport = null;
        private string _layoutFileName = string.Empty;

        public ReporPrinterHandler(XtraReport report)
        {
            _xtraReport = report;
            var r = _xtraReport.GetType();

            _layoutFileName = Path.Combine(Application.StartupPath, r.Name + ".xml");

            if (File.Exists(_layoutFileName))
            {
                _xtraReport.LoadLayout(_layoutFileName);
            }
        }

        public ReporPrinterHandler(XtraReport report, string layoutFile)
        {
            _xtraReport = report;
            _layoutFileName = layoutFile;
        }

        public void PrintReport(List<T> dataList)
        {
            if (File.Exists(_layoutFileName))
            {
                _xtraReport.LoadLayout(_layoutFileName);
            }
            _xtraReport.DataSource = dataList;
            using (ReportPrintTool printTool = new ReportPrintTool(_xtraReport))
            {
                printTool.ShowRibbonPreviewDialog();
            }
        }

        public void StartDesing()
        {
            /*  FormReportDesigner frmFormReportDesigner = new FormReportDesigner(_xtraReport,string.Empty);
              frmFormReportDesigner.ShowDialog();
              */
        }

        public void StartDesing(string layoutfile)
        {
            if (File.Exists(layoutfile))
            {
                _xtraReport.LoadLayout(layoutfile);
            }

            /* FormReportDesigner frmFormReportDesigner = new FormReportDesigner(_xtraReport, layoutfile);
              frmFormReportDesigner.ShowDialog();
              */
        }
    }

    public class SaveCommandHandler : DevExpress.XtraReports.UserDesigner.ICommandHandler
    {
        XRDesignPanel panel;
        private string _layoutFileName = string.Empty;
        FormVersion _formversion = null;
        public SaveCommandHandler(XRDesignPanel panel, string layoutFileName, FormVersion formVersion)
        {
            _layoutFileName = layoutFileName;
            _formversion = formVersion;
            this.panel = panel;
        }

        public void HandleCommand(DevExpress.XtraReports.UserDesigner.ReportCommand command,
        object[] args)
        {
            // Save the report.
            Save();
        }

        public bool CanHandleCommand(DevExpress.XtraReports.UserDesigner.ReportCommand command,
        ref bool useNextHandler)
        {
            useNextHandler = !(command == ReportCommand.SaveFile ||
                command == ReportCommand.SaveFileAs);
            return !useNextHandler;
        }

        void Save()
        {
            // Write your custom saving here.
            // ...
            // For instance:
            if (_layoutFileName == String.Empty)
            {
                _layoutFileName = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".xml");
            }
            panel.Report.SaveLayoutToXml(_layoutFileName);
            if (_formversion.Id == 0)
            {
                FormFormName formFormName = new FormFormName();
                if (formFormName.ShowDialog() == DialogResult.OK)
                {
                    _formversion.FormLanguage = formFormName.FormName;
                }
                else
                {
                    return;
                }
            }

            var saveReturn = AppHelper.restHelper.SaveVersion(_formversion, File.ReadAllBytes(_layoutFileName), Path.GetFileName(_layoutFileName));

            if (saveReturn.iserror)
            {



                MessageBox.Show(saveReturn.message, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormLogin formLogin = new FormLogin();
                if (formLogin.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                return;
            }
            // Prevent the "Report has been changed" dialog from being shown.
            panel.ReportState = ReportState.Saved;
        }
    }
}
