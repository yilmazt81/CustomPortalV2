using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomPortalV2.Model;
using CustomPortalV2.Model.Sale;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleProductController : ControllerBase
    {
          

        [HttpGet]
        public ReturnType<SalePackage> Get()
        {
            ReturnType<SalePackage> returnType = new ReturnType<SalePackage>();
            try
            {

            }
            catch (Exception ex)
            {
                returnType.SetException(ex);
            }

            return returnType;
        }
    }
}
