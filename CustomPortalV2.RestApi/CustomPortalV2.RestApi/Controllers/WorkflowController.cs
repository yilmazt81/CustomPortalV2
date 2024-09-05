using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Model.Work;
using CustomPortalV2.RestApi.Helper;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        IWorkFlowService _workFlowService;
        public WorkflowController(IWorkFlowService workFlowService)
        {
            _workFlowService = workFlowService;
        }
        [HttpGet()]
        public IActionResult Get()
        {
            var workFlowList = _workFlowService.GetCompanyWorkFlow(User.GetCompanyId(), User.GetBranchId());
            return Ok(workFlowList);
        }
        [HttpGet("CreateWorkFlow")]
        public IActionResult CreateWorkFlow()
        {
            DefaultReturn<WorkFlow> defaultReturn = new DefaultReturn<WorkFlow>();

            defaultReturn.Data = new WorkFlow()
            {
                CompanyBranchId = User.GetBranchId(),
                MainCompanyId = User.GetCompanyId(),
                CreatedBy = User.GetUserFullName(),
                CreatedDate = DateTime.Now,
                CreatedId = User.GetUserId(),
            };
            return Ok(defaultReturn);
        }

        [HttpPost("SaveWorkFlow")]
        public IActionResult SaveWorkFlow(WorkFlow workFlow)
        {

            workFlow.EditedId = User.GetUserId();
            workFlow.MainCompanyId = User.GetCompanyId();
            workFlow.CreatedDate = DateTime.Now;
            var workFlowSave = _workFlowService.Save(workFlow);

            return Ok(workFlow);

        }

        [HttpPost("CreateWorkFlowDocument")]
        public IActionResult CreateWorkFlowDocument(IFormCollection formdata)
        {


            return Ok("");
        }


    }
}
