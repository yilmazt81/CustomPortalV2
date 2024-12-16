using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.Work;
using CustomPortalV2.Model.Work;
using CustomPortalV2.RestApi.Helper;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        IWorkFlowService _workFlowService;
        private IMemoryCache _memoryCache;
        public WorkflowController(IWorkFlowService workFlowService, IMemoryCache memoryCache)
        {
            _workFlowService = workFlowService;
            _memoryCache = memoryCache;
        }
        [HttpGet()]
        public IActionResult Get()
        {
            var key = $"WorkFlowCompany{User.GetCompanyId()}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<WorkFlow>> workFlowList))
            {
                return Ok(workFlowList);
            }
            workFlowList = _workFlowService.GetCompanyWorkFlow(User.GetCompanyId(), User.GetBranchId());
            _memoryCache.Set(key, workFlowList, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });


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
                EditedBy="",
                EditedDate=null,
                EditedId=0,
                FlowType="",
                WorkFlowEdges = new List<WorkFlowEdge>(),
                WorkFlowName="",
                WorkFlowNodes = new List<WorkFlowNode>(),
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
