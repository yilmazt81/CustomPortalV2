using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class CountryRepository : ICountryRepository
    {
        DBContext _dbContext;
        public CountryRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Country> GetCountrys()
        {
            return _dbContext.Countrys.OrderBy(s => s.OrderNumber).ToList();
        }

        public IList<CountryCity> GetCountryCities(int countryId)
        {
            return _dbContext.CountryCitys.Where(s => s.CountryId == countryId).ToList();
        }

        public IList<Country> GetCountrysForSaleProduct()
        {

            return _dbContext.Countrys.Where(s => s.ForSaleProduct.HasValue && s.ForSaleProduct.Value).OrderBy(s => s.OrderNumber).ToList();
        }
    }
}
