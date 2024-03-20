using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Helper;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.DataAccessLayer.Repository;
using CustomPortalV2.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CustomPortalV2.Business.Service
{
    public class CompanyService : ICompanyService
    {
        ICompanyRepository _companyRepository;
        IUserRepository _userRepository;

        public CompanyService(ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
        }

        public Branch AddBrach(Branch branch)
        {
            branch.SysAdmin = false;
            return _companyRepository.AddBrach(branch);
        }

        public Company AddCompany(Company company,string password)
        {
            if (!company.CompanyName.IsSaveSqlInjection())
            {

            }
           
            var comp = _companyRepository.AddCompany(company);
            Branch branch = new Branch()
            {
                SysAdmin=false,
                CompanyAdmin=true,
                MainCompanyId=company.Id,
                Deleted=false,
                Email=company.Email,
                PhoneNumber=company.PhoneNumber,
                WaitForAllowApp=true ,
                Name = "Main",

                
            };

            _companyRepository.AddBrach(branch);
            User user = new User()
            {
                BranchName= branch.Name,
                CompanyBranchId= branch.Id,
                CreatedDate= DateTime.UtcNow,
                Deleted= false,
                Email=company.Email,
                FullName= company.AuthorizedPersonName,
                Password= password,
                UserName=company.AuthorizedPersonName,
                PhoneNumber= company.PhoneNumber,
                MainCompanyId = company.Id,

            };
            _userRepository.AddUser(user);
            return comp;
        }

        
        public Company? GetCompanyCode(string companycode)
        {
            return _companyRepository.GetCompanyCode(companycode);

        }

        public bool IsExistCompany(Company company)
        {
            var returnCompany = _companyRepository.FindCompany(s => s.TaxNumber == company.TaxNumber);
            return returnCompany != null;
        }
    }
}
