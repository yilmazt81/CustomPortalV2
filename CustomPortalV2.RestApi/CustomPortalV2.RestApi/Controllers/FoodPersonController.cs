using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Service;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.RestApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FoodPersonController : ControllerBase
    {
        IMemoryCache _memoryCache;
        public FoodPersonController( IMemoryCache memoryCache)
        {
           
            _memoryCache = memoryCache;
        }

        [HttpPost("FilterPerson")]
        public IActionResult FilterPerson(DefinationFilterDTO definationFilterDTO)
        {
              
            return Ok("filterReturn");

        }
    }
}
