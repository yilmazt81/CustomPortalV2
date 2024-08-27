using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        IWorkFlowService _workFlowService;
        public WorkflowController(IWorkFlowService workFlowService) {
            _workFlowService = workFlowService; 
        }
        [HttpGet()]
        public IActionResult Get()
        {
            return Ok();
        } 

        [HttpPost("CreateWorkFlowDocument")]
        public IActionResult CreateWorkFlowDocument(IFormCollection formdata)
        {
            return Ok("");
        }
    }
}
