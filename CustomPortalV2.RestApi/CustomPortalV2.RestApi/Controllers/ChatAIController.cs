using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatAIController : ControllerBase
    {
        IOpenAIService _openAIService;

        public ChatAIController(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Ready");
        }
        [HttpPost("Chat")]
        public IActionResult Post(AIPostRequest request)
        {
           var data =  _openAIService.SendPromtAsync(request);

            return Ok(data.Result);
        }

    }
}
