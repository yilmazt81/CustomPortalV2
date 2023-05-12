using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomPortalV2.Model;
using CustomPortalV2.Model.Sale;
using CustomPortalV2.Business;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleProductController : ControllerBase
    {
        private ISalePackageService _salePackageService;
        // private ILogger _logger;
        public SaleProductController(
            ISalePackageService salePackageService)
        {
            _salePackageService = salePackageService;
        }

        [HttpGet]
        public ReturnType<SalePackage> Get()
        {
            ReturnType<SalePackage> returnType = new ReturnType<SalePackage>();
            try
            {
                returnType.MultiValue = _salePackageService.GetSalePackages().ToList();
            }
            catch (Exception ex)
            {
                returnType.SetException(ex);
            }
            return returnType;
        }

        [HttpGet]
        [Route("GetPackageItems/{packageId}")]
        public ReturnType<SalePackageItem> GetPackageItems(int packageId)
        {
            ReturnType<SalePackageItem> returnType = new ReturnType<SalePackageItem>();
            try
            {
                returnType.MultiValue = _salePackageService.GetSalePackageItem(packageId).ToList();
            }
            catch (Exception ex)
            {
                returnType.SetException(ex);
            }

            return returnType;
        }
    }
}
