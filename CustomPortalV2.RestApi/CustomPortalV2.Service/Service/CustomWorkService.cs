using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.Model.Custom;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class CustomWorkService : ICustomWorkService
    {
        ICustomWorkRepository _repositories;
        IBranchRepository _branchRepository;
        public CustomWorkService(ICustomWorkRepository customWorkRepository, IBranchRepository branchRepository)
        {
            _repositories = customWorkRepository;
            _branchRepository = branchRepository;
        }
        public DefaultReturn<bool> DeleteWork(int companyId, int id)
        {
            DefaultReturn<bool> defaultReturn = new DefaultReturn<bool>();

            return defaultReturn;
        }

        public DefaultReturn<CustomWork> GetCompanyWork(int companyId, int id)
        {
            DefaultReturn<CustomWork> defaultReturn = new DefaultReturn<CustomWork>();
            var workList = _repositories.GetCustomWorks(s => s.Id == id && s.MainCompanyId == companyId && !s.Deleted.Value);
            if (workList == null)
            {
                defaultReturn.ReturnMessage = "WorkNotFound";

            }
            else
            {
                defaultReturn.Data = workList.FirstOrDefault();
            }
            return defaultReturn;
        }

        public DefaultReturn<List<CustomWork>> GetCompanyWorks(int companyId, int branchId, int userId)
        {

            var branch = _branchRepository.Get(s => s.Id == branchId);

            DefaultReturn<List<CustomWork>> defaultReturn = new DefaultReturn<List<CustomWork>>();
            var workList = _repositories.GetCustomWorks(s => s.MainCompanyId == companyId && !s.Deleted.Value);
            if (workList == null)
            {
                defaultReturn.ReturnMessage = "WorkNotFound";

            }
            else
            {
                if (branch.CompanyAdmin)
                {
                    defaultReturn.Data = workList;
                }
                else
                {
                    defaultReturn.Data = workList.Where(s => s.CompanyBranchId == branchId).ToList();
                }

            }
            return defaultReturn;
        }

        public DefaultReturn<CustomWork> Save(CustomWork customWork)
        {
            DefaultReturn<CustomWork> defaultReturn = new DefaultReturn<CustomWork>();

            if (customWork.Id == 0)
            {
                defaultReturn.Data = _repositories.AddCustomWork(customWork);
            }
            else
            {
                defaultReturn.Data = _repositories.UpdateCustomWork(customWork);
            }

            return defaultReturn;
        }
    }
}
