using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.Model.Company;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class CompanyDefinationRepository : ICompanyAdresDefinationRepository
    {
        DBContext _dbContext;
        public CompanyDefinationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CompanyDefination Add(CompanyDefination companyDefination)
        {
            _dbContext.Add(companyDefination);

            _dbContext.SaveChanges();

            return companyDefination;
        }

        public List<CompanyDefination> GetCompanyDefinations(Expression<Func<CompanyDefination, bool>> predicate)
        {

            return _dbContext.CompanyDefination.Include(y => y.CompanyDefinationDefinationType).Where(predicate).ToList();
        }

        public IEnumerable<DefinationType> GetDefinationTypes()
        {

            return _dbContext.DefinationType.ToArray();
        }

        public CompanyDefination Update(CompanyDefination companyDefination)
        {
            var dbdefination = _dbContext.CompanyDefination.Include(s => s.CompanyDefinationDefinationType).Single(s => s.Id == companyDefination.Id);

            dbdefination.Adress = companyDefination.Adress;
            dbdefination.FactoryNumber = companyDefination.FactoryNumber;
            dbdefination.Adress = companyDefination.Adress;
            dbdefination.CompanyName = companyDefination.CompanyName;
            dbdefination.FieldForSearch = companyDefination.FieldForSearch;
            dbdefination.Country = companyDefination.Country;
            dbdefination.City = companyDefination.City;
            dbdefination.DefinationTypeName = companyDefination.DefinationTypeName;
            dbdefination.DefinationTypeId = companyDefination.DefinationTypeId;
            dbdefination.FactoryNumber = companyDefination.FactoryNumber;
            dbdefination.Email = companyDefination.Email;
            dbdefination.FaxNumber = companyDefination.FaxNumber;
            dbdefination.PhoneNumber = companyDefination.PhoneNumber;
            dbdefination.IsoCode = companyDefination.IsoCode;
            dbdefination.Deleted = companyDefination.Deleted;

            var olddefinations = dbdefination.CompanyDefinationDefinationType.ToArray();

            foreach (var item in olddefinations)
            {
                if (!companyDefination.CompanyDefinationDefinationType.Any(s => s.DefinationTypeId == item.DefinationTypeId))
                {
                    dbdefination.CompanyDefinationDefinationType.Remove(item);
                }
            }

            foreach (var definationType in companyDefination.CompanyDefinationDefinationType)
            {
                if (olddefinations.Any(s => s.DefinationTypeId == definationType.DefinationTypeId))
                {
                    continue;
                }
                else
                {
                    dbdefination.CompanyDefinationDefinationType.Add(new CompanyDefinationDefinationType()
                    {
                        DefinationTypeId = definationType.DefinationTypeId,
                        CompanyDefinationId = dbdefination.Id
                    });
                }
            }


            _dbContext.SaveChanges();
            return dbdefination;
        }
    }
}
