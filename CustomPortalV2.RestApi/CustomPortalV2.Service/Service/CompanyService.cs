using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Helper;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.DTO;
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
        ICompanyDefinationRepository _companyRepository;
        IUserRepository _userRepository;
        Encryption encryption = null;

        public CompanyService(ICompanyDefinationRepository companyRepository, IUserRepository userRepository)
        {
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            encryption = new Encryption("usr_9189f3f");
        }

        public Branch AddBrach(Branch branch)
        {
            branch.SysAdmin = false;
            return _companyRepository.AddBrach(branch);
        }

        public DefaultReturn<CreateCompanyReturn> AddCompany(Company company, string password)
        {
            DefaultReturn<CreateCompanyReturn> defaultReturn = new DefaultReturn<CreateCompanyReturn>();
            if (!company.CompanyName.IsSaveSqlInjection())
            {

            }

            defaultReturn.Data = new CreateCompanyReturn();


            company.CompanyCode = _companyRepository.CreateCompanyCode(company.CountryId.Value.ToString());
            defaultReturn.Data.CompanyCode = company.CompanyCode;
           var comp = _companyRepository.AddCompany(company);
            Branch branch = new Branch()
            {
                SysAdmin = false,
                CompanyAdmin = true,
                MainCompanyId = company.Id,
                Deleted = false,
                Email = company.Email,
                PhoneNumber = company.PhoneNumber,
                WaitForAllowApp = true,
                Name = "Main",
                UserRuleId=1,
                UserRuleName="Yönetici",
                BranchPackageId=1,
                BranchPackageName="Ücretsiz",
            };

            _companyRepository.AddBrach(branch);
            User user = new User()
            {
                BranchName = branch.Name,
                CompanyBranchId = branch.Id,
                CreatedDate = DateTime.UtcNow,
                Deleted = false,
                Email = company.Email, 
                FullName = company.AuthorizedPersonName,
                Password = encryption.Encrypt(password),
                UserName = "Admin",
                PhoneNumber = company.PhoneNumber,
                MainCompanyId = company.Id,
                AppLangId = 1,
                
            };
            _userRepository.AddUser(user);

            defaultReturn.Data.CreateUserName = user.UserName;

            return defaultReturn;
        }


       

        public bool IsExistCompany(Company company)
        {
            var returnCompany = _companyRepository.FindCompany(s => s.TaxNumber == company.TaxNumber);
            return returnCompany != null;
        }
    }
}
