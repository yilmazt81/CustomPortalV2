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
        IFormDefinationRepository _formDefinationRepository;

        public CustomProductService(ICustomProductRepository customProductRepository,
            IBranchRepository branchRepository,
            IFormDefinationRepository formDefinationRepository)
        {
            _customProductRepository = customProductRepository;
            _branchRepository = branchRepository;
            _formDefinationRepository = formDefinationRepository;
        }

        public DefaultReturn<bool> Delete(int mainCompanyId, int userId, int id)
        {

            DefaultReturn<bool> defaultReturn = new DefaultReturn<bool>();
            var product = _customProductRepository.Get(id);
            product.Deleted = true;
            product.EditedId = userId;
            product.EditedDate = DateTime.Now;
            _customProductRepository.Update(product);

            return defaultReturn;
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

            var sector = _formDefinationRepository.GetCompanySector(customProduct.CustomSectorId);
            customProduct.CustomSectorName = sector.Name;
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
