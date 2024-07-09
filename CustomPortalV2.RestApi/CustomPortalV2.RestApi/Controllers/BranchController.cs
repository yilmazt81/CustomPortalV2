using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Service;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Model.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomPortalV2.RestApi.Controllers
{
    public class BranchController : ControllerBase
    {
        ICompanyService companyService;
        IBranchService branchService;
        public BranchController(ICompanyService companyService, IBranchService branchService)
        {
            this.companyService = companyService;
            this.branchService = branchService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var branchList = branchService.GetCompanyBraches(1);

            return Ok(branchList);
        }
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {

            var branch = branchService.GetBranch(id);


            return Ok(branch);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Branch branchDefination)
        {
           
            var returnV = branchService.AddBranch(branchDefination);

            return Ok(returnV);

        }

    }
}
