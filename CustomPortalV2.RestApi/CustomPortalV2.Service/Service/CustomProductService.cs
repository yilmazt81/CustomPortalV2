using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class CustomProductService : ICustomProductService
    {
        IBranchRepository _branchRepository;
        ICustomProductRepository _customProductRepository;

        public CustomProductService(ICustomProductRepository customProductRepository, IBranchRepository branchRepository)
        {
            _customProductRepository = customProductRepository;
            _branchRepository = branchRepository;
        }
        public DefaultReturn<List<CustomProduct>> GetCompanyProducts(int mainCompanyId, int branchId)
        {
            DefaultReturn<List<CustomProduct>> defaultReturn = new DefaultReturn<List<CustomProduct>>();
            var branch = _branchRepository.Get(s => s.Id == branchId);
            if (!branch.CompanyAdmin)
            {
                defaultReturn.Data = _customProductRepository.GetCustomProducts(s => s.MainCompanyId == mainCompanyId && s.CompanyBranchId == branchId && !s.Deleted).ToList();
            }
            else
            {
                defaultReturn.Data = _customProductRepository.GetCustomProducts(s => s.MainCompanyId == mainCompanyId && !s.Deleted).ToList();

            }

            return defaultReturn;
        }

        public DefaultReturn<CustomProduct> GetProduct(int mailProductId, int branchId, int id)
        {
            DefaultReturn<CustomProduct> defaultReturn = new DefaultReturn<CustomProduct>();

            defaultReturn.Data = _customProductRepository.Get(id);
            return defaultReturn;
        }

        public DefaultReturn<CustomProduct> Save(CustomProduct customProduct)
        {
            DefaultReturn<CustomProduct> defaultReturn = new DefaultReturn<CustomProduct>();
            if (customProduct.Id == 0)
            {
                defaultReturn.Data = _customProductRepository.Add(customProduct);
            }
            else
            {
                defaultReturn.Data = _customProductRepository.Update(customProduct);
            }

            return defaultReturn;
        }
    }
}
