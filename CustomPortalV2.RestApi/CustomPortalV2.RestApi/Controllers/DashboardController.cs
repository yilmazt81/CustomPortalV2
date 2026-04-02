using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Service;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.Form;
using CustomPortalV2.RestApi.Helper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        IDashboardService _dashboardService;
        private IMemoryCache _memoryCache;
        public DashboardController(IDashboardService dashboardService, IMemoryCache memoryCache )
        {
            _dashboardService=dashboardService;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public IActionResult Get()
        {

            var companyId = User.GetCompanyId();
            var key = $"DasBoardDayCount{companyId}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<DashboardReport> reportCount))
            {
                return Ok(reportCount);
            }


            var reportCounts = _dashboardService.GetDashboardReport(companyId);

            _memoryCache.Set(key, reportCounts, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal
            });

            return Ok(reportCounts);
        }

        [HttpGet("GetLastForms")]
        public IActionResult GetLastForms()
        {
            var companyId = User.GetCompanyId();
            var branchId = User.GetBranchId();
            var key = $"DasBoardLastDocuments_{companyId}_{branchId}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<FormMetaData>> lastDocuments))
            {
                return Ok(lastDocuments);
            }

            var lastDocumentList = _dashboardService.GetLatestForms(companyId,branchId); 

            _memoryCache.Set(key, lastDocumentList, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal
            });

            return Ok(lastDocumentList);
        }
    }
}
