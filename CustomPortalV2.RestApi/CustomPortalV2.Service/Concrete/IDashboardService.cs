using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface IDashboardService
    {

        DefaultReturn<Core.Model.DTO.DashboardReport> GetDashboardReport(int mainCompanyId);

        DefaultReturn<List<FormMetaData>> GetLatestForms(int mainCompanyId, int branchId);
    }
}
