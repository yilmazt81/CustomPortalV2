using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomPortalV2.Model;
using CustomPortalV2.Model.Sale;
using CustomPortalV2.Business;
using CustomPortalV2.Core.Model.DTO;

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
        public ActionResult Get()
        {
            DefaultReturn<SalePackage[]> returnType = new DefaultReturn<SalePackage[]>();
            try
            {
                returnType.Data = _salePackageService.GetSalePackages();
            }
            catch (Exception ex)
            {
                returnType.SetException(ex);
            }
            return Ok(returnType);
        }

        [HttpGet]
        [Route("GetPackageItems/{packageId}")]
        public ActionResult GetPackageItems(int packageId)
        {
            DefaultReturn<SalePackageItem[]> returnType = new DefaultReturn<SalePackageItem[]>();
            try
            {
                returnType.Data = _salePackageService.GetSalePackageItem(packageId);
            }
            catch (Exception ex)
            {
                returnType.SetException(ex);
            }

            return Ok(returnType);
        }
    }
}
