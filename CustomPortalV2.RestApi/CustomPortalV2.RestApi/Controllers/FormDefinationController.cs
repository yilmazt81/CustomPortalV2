using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.RestApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FormDefinationController : Controller
    {
        IFormDefinationService _formDefinationService = null;
        IMemoryCache _memoryCache;
        public FormDefinationController(IFormDefinationService formDefinationService,
            IMemoryCache memoryCache)
        {
            _formDefinationService = formDefinationService;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var companyId = User.GetCompanyId();
            string key = $"FormDefinationCompany{companyId}";
            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<FormDefination>> list))
                return Ok(list);


            var companyDefination = _formDefinationService.GetCompanyDefinations(companyId);
            _memoryCache.Set(key, companyDefination, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(companyDefination);

        }

        [HttpGet("CreateFormDefination")]
        public IActionResult CreateFormDefination()
        {
            DefaultReturn<FormDefination> defaultReturn = new DefaultReturn<FormDefination>();

            defaultReturn.Data = new FormDefination()
            {
                CreatedDate = DateTime.Now,
                CreatedId = User.GetUserId(),
                MainCompanyId = User.GetCompanyId(),
                FormName="",
            };
            return Ok(defaultReturn);
        }
    }
       
}
