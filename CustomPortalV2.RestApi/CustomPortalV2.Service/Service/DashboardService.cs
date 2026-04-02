using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.Form;
using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class DashboardService : IDashboardService
    {
        IDashboardRepository _dashboardRepository;
        IFormMetaDataRepository _formMetaDataRepository;
        IBranchRepository _branchRepository;

        public DashboardService(IDashboardRepository dashboardRepository, IFormMetaDataRepository formMetaDataRepository, IBranchRepository branchRepository)
        {
            this._dashboardRepository = dashboardRepository;
            _formMetaDataRepository = formMetaDataRepository;
            _branchRepository = branchRepository;
        }
        public DefaultReturn<DashboardReport> GetDashboardReport(int mainCompanyId)
        {
            DefaultReturn<DashboardReport> defaultReturn = new DefaultReturn<DashboardReport>();

                defaultReturn.Data = _dashboardRepository.GetDateRecords(mainCompanyId);
            return defaultReturn;
        }

        public DefaultReturn<List<FormMetaData>> GetLatestForms(int mainCompanyId, int branchId)
        {
            DefaultReturn<List<FormMetaData>> defaultReturn = new DefaultReturn<List<FormMetaData>>();
            try
            {
                var formData = _formMetaDataRepository.GetQueryable();
                var userBranch = _branchRepository.Get(s => s.Id == branchId);

                if (!userBranch.SysAdmin)
                {
                    formData = formData.Where(s => s.MainCompanyId == mainCompanyId);
                }
                if (!userBranch.CompanyAdmin)
                {
                    formData = formData.Where(s => s.CompanyBranchId == branchId);
                }
                 
                formData = formData.Where(s => !s.Deleted && !s.DefaultForm); 

               
                var userLastForms = formData.OrderByDescending(s => s.Id).Take(10).OrderBy(s=>s.Id).ToList();

                defaultReturn.Data = userLastForms.ToList();
            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;
        }
    }
}
