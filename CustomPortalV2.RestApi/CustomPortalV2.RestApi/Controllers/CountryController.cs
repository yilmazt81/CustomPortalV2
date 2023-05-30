using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        ICountryService _countryService;
        private IMemoryCache _cache;
        public CountryController(ICountryService countryService, IMemoryCache cache)
        {
            _countryService = countryService;
            _cache = cache;
        }


        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {

            if (_cache.TryGetValue("ListCountry", out IList<Country> countryList))
            {

            }
            else
            {
                countryList = _countryService.GetCountries();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                _cache.Set("ListCountry", countryList, cacheEntryOptions);

            }
            return Ok(countryList);
        }

        [HttpGet("GetForSaleProduct")]
        public IActionResult GetForSale()
        {

            if (_cache.TryGetValue("ListCountrySale", out IList<Country> countryList))
            {

            }
            else
            {
                countryList = _countryService.GetCountrysForSaleProduct();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                _cache.Set("ListCountrySale", countryList, cacheEntryOptions);

            } 
            return Ok(countryList);
        }

        [HttpGet("GetContryCity")]

        public IActionResult GetContryCity(int countryId)
        {
            var cacheName = $"CountryCity{countryId}";


            if (_cache.TryGetValue(cacheName, out IList<CountryCity> cityList))
            {

            }
            else
            {
                cityList = _countryService.GetCountryCities(countryId);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                _cache.Set(cacheName, cityList, cacheEntryOptions);

            }
            
            return Ok(cityList);

        }

    }
}
