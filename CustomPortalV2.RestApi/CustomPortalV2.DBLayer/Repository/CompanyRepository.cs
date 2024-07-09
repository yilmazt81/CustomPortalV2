using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        DBContext _dbContext;
        public CompanyRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Branch AddBrach(Branch branch)
        {
            _dbContext.Branches.Add(branch);
            _dbContext.SaveChanges();

            return branch;
        }

        public Company AddCompany(Company company)
        {
            _dbContext.Companies.Add(company);
            _dbContext.SaveChanges();
            return company;
        }

        public string CreateCompanyCode(string countryid)
        {
           var maxCompanyCount= _dbContext.Companies.Count();

            var newCompanyCode = countryid.PadLeft(2, '0') + maxCompanyCount.ToString().PadLeft(3, '0');

            return newCompanyCode;

        }

        public Company? FindCompany(Expression<Func<Company, bool>> predicate)
        {
            return _dbContext.Companies.FirstOrDefault(predicate);
        }

        public Company? GetCompanyCode(string companycode)
        {
            return _dbContext.Companies.FirstOrDefault(s => s.CompanyCode == companycode);

        }
    }
}
