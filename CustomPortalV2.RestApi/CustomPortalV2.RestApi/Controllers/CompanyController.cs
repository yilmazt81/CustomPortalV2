using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Model.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        ICompanyService companyService;
        IBranchService branchService;
        public CompanyController(ICompanyService companyService, IBranchService branchService)
        {
            this.companyService = companyService;
            this.branchService = branchService; 
        }  

        [HttpPost]
        public IActionResult Post([FromBody] CreateCompanyRequest createCompany)
        {
            var company = new Company()
            {
                CompanyName = createCompany.CompanyName,
                AuthorizedPersonName = createCompany.AuthorizedPersonName,
                CityId = createCompany.City,
                Email = createCompany.Email,
                MersisNo = createCompany.MersisNo,
                PhoneNumber = createCompany.PhoneNumber,
                TaxNumber = createCompany.TaxNumber,
                Enable = true,
                CountryId = createCompany.Country ,
                Adress= createCompany.Adress,

            };
            if (companyService.IsExistCompany(company))
            {
                return Ok(new DefaultReturn<Company>(9, "CompanyExist"));
            }

            var returnV = companyService.AddCompany(company, createCompany.Password);

            return Ok(returnV);

        }
    }
}
