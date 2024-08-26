using CustomPortalV2.Core.Model.DTO;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        [HttpGet("Schemes")]
        public IActionResult Getschemes()
        {
            var returnList = new List<SchemesDTO>() { new SchemesDTO() { code = "test", tags = "ddd,sss,yyy" } };
            return Ok(returnList);
        }

        [HttpGet("instances")]
        public IActionResult Getinstances()
        {
            List<SchemeInstanceDTO> schemeInstanceDTOs = new List<SchemeInstanceDTO>() { new SchemeInstanceDTO() { Id=1,activityName="test",creationDate=DateTime.Now,scheme="test",stateName="Run"} };


            return Ok(schemeInstanceDTOs);
        }

        [HttpGet]
        public IActionResult Get(string schemecode="",string processid = "",string schemeid = "",string operation = "",string newschemecode="")
        {


            return Ok(new WorkFlowProcessDTO()  );

        }


        [HttpGet("commands/{id}/{name}")]
        public IActionResult Getcommand(int id,string name)
        {


            return Ok(new WorkFlowProcessDTO());

        }

        [HttpGet("createInstance/{code}")]
        public IActionResult createInstance(string code)
        {
            var s = new SchemesDTO() { code = "test", tags = "ddd,sss,yyy" };
            return Ok(s);

        }

     

        [HttpGet("getworkflowengineinfo")]
        public IActionResult getworkflowengineinfo()
        {


            return Ok();

        }

        [HttpPost]
        public IActionResult Post(IFormCollection formdata)
        { 
            return Ok("");
        }
    }
}
