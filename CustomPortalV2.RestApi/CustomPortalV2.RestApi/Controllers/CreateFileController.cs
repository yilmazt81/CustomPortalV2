using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CreateFileController : ControllerBase
    {

        [HttpGet("CreateFormVersion/{id}/{versionid}")]
        public IActionResult CreateFormVersion(int id,int versionid) { 
        
            return Ok();
        }

        [HttpGet("CreateFormAttachment/{id}/{attachmentId}")]
        public IActionResult CreateFormAttachment(int id,int attachmentId)
        {
            Thread.Sleep(1000);
            return Ok("");
        }
    }
}
