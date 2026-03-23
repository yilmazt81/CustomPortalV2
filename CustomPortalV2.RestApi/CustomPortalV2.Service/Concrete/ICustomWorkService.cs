using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Model.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface ICustomWorkService
    {
        DefaultReturn<List<CustomWork>> GetCompanyWorks(int companyId, int branchId, int userId);
        DefaultReturn<CustomWork> GetCompanyWork(int companyId, int id);

        DefaultReturn<bool> DeleteWork(int companyId, int id);

        DefaultReturn<CustomWork> Save(CustomWork customWork);
    }
}
