using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class CountryService: ICountryService
    {
        ICountryRepository _countryRepository;
        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public IList<Country> GetCountries()
        {
             return _countryRepository.GetCountrys();
        }

        public IList<CountryCity> GetCountryCities(int countryId)
        {
           return _countryRepository.GetCountryCities(countryId);
        }

        public IList<Country> GetCountrysForSaleProduct()
        {
            
            return _countryRepository.GetCountrysForSaleProduct();
        }
    }
}
