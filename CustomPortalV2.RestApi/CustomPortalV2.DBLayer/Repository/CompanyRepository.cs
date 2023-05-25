using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        DBContext _dbContext;
        public CompanyRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Company AddCompany(Company company)
        {
            _dbContext.Companies.Add(company);
            _dbContext.SaveChanges();

            return company;
        }

        public Company? GetCompanyCode(string companycode)
        {
            return _dbContext.Companies.FirstOrDefault(s => s.CompanyCode == companycode);

        }
    }
}
