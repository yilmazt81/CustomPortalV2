using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Model.Custom;
using CustomPortalV2.RestApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomWorkController : ControllerBase
    {
        IMemoryCache _memoryCache;
        ICustomWorkService _customWorkService;
        public CustomWorkController(ICustomWorkService customWorkService, IMemoryCache memoryCache)
        {

            _memoryCache = memoryCache;
            _customWorkService = customWorkService;
        }
        [HttpGet]
        public IActionResult Get()
        {

            var key = $"CustomWorks{User.GetCompanyId()}_{User.GetBranchId()}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<CustomWork>> list))
                return Ok(list);
            var workList = _customWorkService.GetCompanyWorks(User.GetCompanyId(), User.GetBranchId(), User.GetUserId());

            _memoryCache.Set(key, workList, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });
            return Ok(workList);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var key = $"CustomWork{User.GetCompanyId()}_{id}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<CustomWork> list))
                return Ok(list);
            var work  = _customWorkService.GetCompanyWork(User.GetCompanyId(), id);

            _memoryCache.Set(key, work, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });
            return Ok(work);
        }
         
    }
}
