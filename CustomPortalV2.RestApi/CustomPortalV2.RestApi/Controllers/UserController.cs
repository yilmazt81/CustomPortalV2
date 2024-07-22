using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        IMemoryCache _memoryCache;
        public UserController(IUserService userService, IMemoryCache memoryCache)
        {
            _userService = userService;
            _memoryCache = memoryCache;
        }

        [HttpGet("GetUserMenu")] 
        public IActionResult GetUserMenu()
        {


            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var branchId = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            //Todo: badge için ekleme yapılacak. cachedende dönse db sen sayi alinacak.

            string key = $"UserMenu{userId}";
            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<UserRuleMenuDTO>> list))
                return Ok(list);

            var applicationMenu = _userService.GetUserManu(int.Parse(userId), int.Parse(branchId));
            _memoryCache.Set(key, applicationMenu, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(applicationMenu);
        }
        [HttpGet("GetUserRoles")]
        public IActionResult GetUserRoles()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            string key = $"UserRoles{userId}";
            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<UserRule>> list))
                return Ok(list);

            var userRoles = _userService.GetUserRoles(int.Parse(userId));
            _memoryCache.Set(key, userRoles, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(userRoles);
        }

        [HttpGet("GetBranchpackages")]
        public IActionResult GetBranchpackages()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            string key = $"Branchpackage{userId}";
            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<BranchPackage>> list))
                return Ok(list);

            var userRoles = _userService.GetBranchPackage(int.Parse(userId));
            _memoryCache.Set(key, userRoles, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(userRoles);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        
    }
}
