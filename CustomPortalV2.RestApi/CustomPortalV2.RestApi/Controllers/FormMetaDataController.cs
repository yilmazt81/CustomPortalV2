using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.Form;
using CustomPortalV2.RestApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FormMetaDataController : ControllerBase
    {

        IFormMetaDataService    _formMetaDataService;
        public FormMetaDataController(IFormMetaDataService formMetaDataService) { 
            _formMetaDataService = formMetaDataService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            
           var formMetaList= _formMetaDataService.GetBranchFormMetaData(User.GetCompanyId(), User.GetBranchId());

            
            return Ok(formMetaList);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var formmetaData = _formMetaDataService.GetCompanyFormMetaData(User.GetCompanyId(), User.GetBranchId(),id);


            return Ok(formmetaData);
        }

    }
}
