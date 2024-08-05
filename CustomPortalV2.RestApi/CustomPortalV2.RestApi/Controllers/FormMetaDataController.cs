using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.Core.Model.Form;
using CustomPortalV2.RestApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FormMetaDataController : ControllerBase
    {

        IFormMetaDataService    _formMetaDataService;
        IFormDefinationService _formDefinationService;
        IMemoryCache _memoryCache;
        public FormMetaDataController(IFormMetaDataService formMetaDataService, IFormDefinationService formDefinationService,IMemoryCache memoryCache) { 
            _formMetaDataService = formMetaDataService;
            _formDefinationService = formDefinationService;
            _memoryCache = memoryCache;
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
