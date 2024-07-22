using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Service;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Model.Company;
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

            var branchList = branchService.GetCompanyBraches(int.Parse(companyId), int.Parse(branchId));

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
            var companyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
            var branch = branchService.GetBranch(int.Parse(companyId), id);


            return Ok(branch);
        }

        [HttpGet("DeleteBranch/{id}")]
        public IActionResult DeleteBranch(int id)
        {
            var companyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;

            string key = $"CompanyBranch{companyId}";
            _memoryCache.Remove(key);
            var deleteResult = branchService.Delete(int.Parse(companyId), id);


            return Ok(deleteResult);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Branch branchDefination)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var branchId = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            var companyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;

            branchDefination.MainCompanyId =int.Parse(companyId);
           
            var returnV = branchService.Save(branchDefination);
            string key = $"CompanyBranch{companyId}";
            _memoryCache.Remove(key);

            return Ok(returnV);

        }

    }
}
