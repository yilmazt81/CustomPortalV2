using CustomPortalV2.Core.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface ICountryService
    {

        IList<Country> GetCountries();

        IList<Country> GetCountrysForSaleProduct();

        IList<CountryCity> GetCountryCities(int countryId);
    }
}
