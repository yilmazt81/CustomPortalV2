using CustomPortalV2.Business.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CustomPortalV2.RestApi.Controllers
{
    public class CountryController : ControllerBase
    {
        ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var countryList = _countryService.GetCountries();

            return Ok(countryList);
        }

        [HttpGet]
        public IActionResult GetCityList(int countryId)
        {
            var cityList= _countryService.GetCountryCities(countryId);
            
            return Ok(cityList);

        }

    }
}
