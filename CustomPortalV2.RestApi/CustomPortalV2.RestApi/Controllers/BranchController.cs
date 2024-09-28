using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Service;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Model.Company;
using CustomPortalV2.RestApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Security.Claims;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BranchController : ControllerBase
    {
        ICompanyService companyService;
        IBranchService branchService;
        IMemoryCache _memoryCache;
        public BranchController(ICompanyService companyService, IBranchService branchService, IMemoryCache memoryCache)
        {
            this.companyService = companyService;
            this.branchService = branchService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var branchId = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            var companyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;

            string key = $"CompanyBranch{companyId}";
             
          if (_memoryCache.TryGetValue(key, out DefaultReturn<List<Branch>> list))
                return Ok(list);

            var branchList = branchService.GetCompanyBraches(User.GetCompanyId(), int.Parse(branchId));

            _memoryCache.Set(key, branchList, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });
            
            return Ok(branchList);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        { 
            var branch = branchService.GetBranch(User.GetCompanyId(), id);


            return Ok(branch);
        }
        [HttpGet("CreateNewBranch")]
        public IActionResult CreateNewBranch()
        {
            DefaultReturn<Branch> branch = new DefaultReturn<Branch>();
            branch.Data = new Branch()
            {
                BranchPackageName="",
                CompanyAdmin=false,
                Email="",
                EMailPassword="",
                MainCompanyId=User.GetCompanyId(),
                Name="",
                BranchPackageId=0,
                PhoneNumber="",
                SysAdmin=false,
                UserRuleName="",
                
                
            };
            return Ok(branch);
        }

        [HttpGet("DeleteBranch/{id}")]
        public IActionResult DeleteBranch(int id)
        {
            
            string key = $"CompanyBranch{User.GetCompanyId()}";
            _memoryCache.Remove(key);
            var deleteResult = branchService.Delete(User.GetCompanyId(), id);


            return Ok(deleteResult);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Branch branchDefination)
        { 
           

            branchDefination.MainCompanyId =User.GetCompanyId();
           
            var returnV = branchService.Save(branchDefination);
            string key = $"CompanyBranch{User.GetCompanyId()}";
            _memoryCache.Remove(key);

            return Ok(returnV);

        }

    }
}
