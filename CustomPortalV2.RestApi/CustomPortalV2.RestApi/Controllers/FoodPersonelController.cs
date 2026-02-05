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
    public class FoodPersonelController : ControllerBase
    {
        IMemoryCache _memoryCache;
        IFoodPersonelService _foodPersonelService;
        public FoodPersonelController(IMemoryCache memoryCache, IFoodPersonelService foodPersonelService)
        {

            _memoryCache = memoryCache;
            _foodPersonelService = foodPersonelService;
        }

        [HttpPost("FilterPersonel")]
        public IActionResult FilterPerson(DefinationFilterDTO definationFilterDTO)
        {
            var returnV = _foodPersonelService.Filter(definationFilterDTO);
            return Ok(returnV);

        }


        [HttpGet("GetAutoComplatePersonel/{formdefinationId}/{personelid}")]
        public IActionResult GetAutoComplatePersonel(int formdefinationId, int personelid)
        {
            string key = $"PersonelDefination{formdefinationId}_{personelid}";

            var autoComplateDefinationFieldlist = _foodPersonelService.GetAutoComplateDefinationValues(formdefinationId, personelid);
            return Ok(autoComplateDefinationFieldlist);
        }
    }
}
