using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Service;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.Definations;
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
    public class CustomProductController : ControllerBase
    {
        ICustomProductService _customProductService;
        IMemoryCache _memoryCache;

        public CustomProductController(ICustomProductService customProductService, IMemoryCache memoryCache)
        {
            _customProductService = customProductService;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public IActionResult Get()
        {

            var key = $"CustomProduct{User.GetCompanyId()}_{User.GetBranchId()}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<CustomProduct>> list))
                return Ok(list);
            var branchList = _customProductService.GetCompanyProducts(User.GetCompanyId(), User.GetBranchId());

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
            var product = _customProductService.GetProduct(User.GetCompanyId(), User.GetBranchId(), id);

            return Ok(product);
        }

        [HttpGet("CreateProduct")]
        public IActionResult CreateProduct()
        {
            DefaultReturn<CustomProduct> defaultReturn = new DefaultReturn<CustomProduct>();

            defaultReturn.Data = new CustomProduct()
            {
                CompanyBranchId = User.GetBranchId(),
                MainCompanyId = User.GetCompanyId(),
                CreatedBy = User.GetUserFullName(),
                CreatedDate = DateTime.Now,
                CreatedId = User.GetUserId(),
                ProductCulture = "",
                ProductName = "",
                ProductName_TRK = "",
                ScientificName = "",
                Transfercondition = "",
                IntendedUse = "",
                TransferTemperature = "",
                GtipCode = "",

            };
            return Ok(defaultReturn);
        }

        [HttpPost]
        public IActionResult Post(CustomProduct customProduct)
        {
            var key = $"CustomProduct{User.GetCompanyId()}_{User.GetBranchId()}";
            _memoryCache.Remove(key);
            if (customProduct.Id != 0)
            {
                customProduct.EditedBy = User.GetUserFullName();
                customProduct.EditedId = User.GetUserId();
            }
            var defaultReturn = _customProductService.Save(customProduct);

            return Ok(defaultReturn);
        }

        [HttpGet("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var key = $"CustomProduct{User.GetCompanyId()}_{User.GetBranchId()}";
            _memoryCache.Remove(key);
            var productReturn = _customProductService.Delete(User.GetCompanyId(),User.GetUserId() ,id);


            return Ok(productReturn);

        }

        [HttpPost("FilterProduct")]
        public IActionResult FilterProduct(DefinationFilterDTO definationFilterDTO)
        {
            var filterReturn = _customProductService.Filter(definationFilterDTO,User.GetBranchId());

            return Ok(filterReturn);

        }


        [HttpGet("GetAutoComplateProduct/{formdefinationId}/{productIdlist}")]
        public IActionResult GetAutoComplateProduct(int formdefinationId, string productIdlist)
        {
            string key = $"CompanyDefination{formdefinationId}_{productIdlist}";

            var autoComplateDefinationFieldlist = _customProductService.GetAutoComplateDefinationValues(User.GetCompanyId(), formdefinationId, productIdlist);
            return Ok(autoComplateDefinationFieldlist);
        }

    }
}
