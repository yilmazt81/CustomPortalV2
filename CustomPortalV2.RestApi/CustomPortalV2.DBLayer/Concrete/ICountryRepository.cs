using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Concrete
{
    public interface ICountryRepository
    {
        IList<Country> GetCountrys();
        IList<CountryCity> GetCountryCities(int countryId);

    }
}
