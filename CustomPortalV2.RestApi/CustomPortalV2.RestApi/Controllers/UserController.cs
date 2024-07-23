using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.RestApi.Helper;
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
            var userId = User.GetUserId();

            string key = $"UserRoles{userId}";
            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<UserRule>> list))
                return Ok(list);

            var userRoles = _userService.GetUserRoles(User.GetCompanyId(), userId);
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
            var userId = User.GetUserId();
            string key = $"Branchpackage{userId}";
            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<BranchPackage>> list))
                return Ok(list);

            var userRoles = _userService.GetBranchPackage(User.GetCompanyId(), userId);
            _memoryCache.Set(key, userRoles, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(userRoles);
        }

        [HttpGet]
        public ActionResult Get()
        {
            var branchId = User.GetBranchId();
            var companyId = User.GetCompanyId();
            string key = $"BranchUser{branchId}";


            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<User>> list))
                return Ok(list);


            var userList = _userService.GetUsers(companyId, branchId);

            _memoryCache.Set(key, userList, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });


            return Ok(userList);

        }
        [HttpGet("CreateNewUser")]
        public ActionResult CreateNewUser()
        {
            var branchId = User.GetBranchId();
            var companyId = User.GetCompanyId();
            string key = $"BranchUser{branchId}";

            DefaultReturn<User> defaultReturn = new DefaultReturn<User>();
            defaultReturn.Data = new User()
            {
                MainCompanyId = companyId,
                CompanyBranchId = branchId,
                BranchName="",
                FullName="",
                PhoneNumber="",
                UserName="",
                Email="",
            };

            return Ok(defaultReturn);

        }
        
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var branchId = User.GetBranchId();
            var companyId = User.GetCompanyId();
            string key = $"User{id}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<User> list))
                return Ok(list);

            var userReturn = _userService.GetById(companyId, id);

            _memoryCache.Set(key, userReturn, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(userReturn);
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }


    }
}
