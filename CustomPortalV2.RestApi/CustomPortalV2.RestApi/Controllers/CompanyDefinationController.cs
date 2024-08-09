using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Service;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.RestApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyDefinationController : ControllerBase
    {
        ICompanyDefinationService _companyDefinationService;
        IMemoryCache _memoryCache;
        public CompanyDefinationController(ICompanyDefinationService companyDefinationService, IMemoryCache memoryCache)
        {
            _companyDefinationService = companyDefinationService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var branchId = User.GetBranchId();
            string key = $"CompanyAdresList{branchId}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<CompanyDefination>> list))
                return Ok(list);

            var brachCompanyList = _companyDefinationService.GetCompanyDefinations(User.GetCompanyId(), User.GetBranchId());
            _memoryCache.Set(key, brachCompanyList, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(brachCompanyList);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            string key = $"CompanyDefination{id}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<CompanyDefination> list))
                return Ok(list);

            var companyDefination = _companyDefinationService.GetCompanyDefination(User.GetCompanyId(), User.GetBranchId(), id);
            _memoryCache.Set(key, companyDefination, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(companyDefination);
        }

        [HttpGet("DeleteCompanyDefination/{id}")]
        public IActionResult DeleteCompanyDefination(int id)
        {
            var companyId = User.GetCompanyId();

            string key = $"CompanyDefination{id}";
    
            var deleteResult = _companyDefinationService.DeleteCompany(companyId, id);
            _memoryCache.Remove(key);
            RemoveCache();

            return Ok(deleteResult);
        }


        [HttpGet("GetDefinationTypes")]
        public IActionResult GetDefinationTypes()
        {

            string key = $"AdresDefination";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<DefinationType>> list))
                return Ok(list);


            var defaultReturn = _companyDefinationService.GetDefinationTypes();

            _memoryCache.Set(key, defaultReturn, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });
            return Ok(defaultReturn);
        }
        [HttpGet("NewDefination")]
        public IActionResult NewDefination()
        {

            DefaultReturn<CompanyDefination> defaultReturn = new DefaultReturn<CompanyDefination>()
            {
                Data = new CompanyDefination()
                {
                    MainCompanyId = User.GetCompanyId(),
                    CompanyBranchId = User.GetBranchId(),
                    CountryCityId = 0,
                    CountryId = 0,
                    FieldForSearch = "",
                    PhoneNumber = "",
                    Adress = "",
                    City = "",
                    CompanyName = "",
                    Country = "",
                    DefinationTypeName = "",
                    DefinationTypeId = "",
                    Email = "",
                    FactoryNumber = "",
                    FaxNumber = "",
                    IsoCode = "",


                }
            };

            return Ok(defaultReturn);
        }

        [HttpPost]
        public ActionResult Post(CompanyDefination companyDefination)
        {
            companyDefination.MainCompanyId = User.GetCompanyId();
            companyDefination.CompanyBranchId = User.GetBranchId();
            var returnT = _companyDefinationService.Save(companyDefination);

            RemoveCache();

            return Ok(returnT);
        }
        private void RemoveCache()
        {
            var branchId = User.GetBranchId();
            string key = $"CompanyAdresList{branchId}";
            _memoryCache.Remove(key);
        }

        [HttpPost("UpdateAdress")]
        public ActionResult UpdateAdress(CompanyDefination companyDefination)
        {
            companyDefination.MainCompanyId = User.GetCompanyId();
            companyDefination.CompanyBranchId = User.GetBranchId();
            var returnT = _companyDefinationService.Save(companyDefination);

            string key = $"CompanyDefination{companyDefination.Id}";
             
            _memoryCache.Remove(key);


            RemoveCache();


            return Ok(returnT);
        }

        [HttpPost("FilterCompanyDefination")]
        public IActionResult FilterCompanyDefination(DefinationFilterDTO companyDefinationFilter)
        {
           var filterReturn= _companyDefinationService.Filter(companyDefinationFilter,User.GetBranchId());

            return Ok(filterReturn);
        }

        [HttpGet("GetAutoComplateAdress/{formdefinationId}/{adressId}")]
        public IActionResult GetAutoComplateAdress(int formdefinationId,int adressId)
        {
            string key = $"CompanyDefination{formdefinationId}_{adressId}";

            var autoComplateDefinationFieldlist=_companyDefinationService.GetAutoComplateDefinationValues(formdefinationId,adressId);
            return Ok(autoComplateDefinationFieldlist);
        }

    }
}
