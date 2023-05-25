using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class CountryService: ICountryService
    {
        ICountryService _countryService;
        public CountryService(ICountryService countryService)
        {
            _countryService=countryService;
        }

        public IList<Country> GetCountries()
        {
             return _countryService.GetCountries();
        }

        public IList<CountryCity> GetCountryCities(int countryId)
        {
           return _countryService.GetCountryCities(countryId);
        }
    }
}
