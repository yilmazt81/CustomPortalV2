using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface ICustomProductService
    {
        DefaultReturn<List<CustomProduct>> GetCompanyProducts(int mainCompanyId, int branchId);
        DefaultReturn<CustomProduct> GetProduct(int mainCompanyId, int branchId, int id);
        DefaultReturn<CustomProduct> Save(CustomProduct customProduct);

        DefaultReturn<bool> Delete(int mainCompanyId,int userId, int id);


    }
}
