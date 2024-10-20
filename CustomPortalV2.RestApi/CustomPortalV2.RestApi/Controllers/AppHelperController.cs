using Microsoft.AspNetCore.Mvc;

namespace CustomPortalV2.RestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AppHelperController : ControllerBase
    {

        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
