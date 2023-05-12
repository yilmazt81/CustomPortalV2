using CustomPortalV2.Model;
using CustomPortalV2.Model.DTO;
using CustomPortalV2.Business;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private IUserService _userservice;
        // private ILogger _logger;
        public LoginController(
            IUserService userService)
        {
            _userservice = userService;
        }

        // GET: api/<LoginController>
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(new LoginReturn()
            {
                IsLogin = true,
                token = Guid.NewGuid().ToString()
            });
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoginController>
        [HttpPost]
        public ReturnType<LoginReturn> Post(LoginRequest loginRequest)
        {
            ReturnType<LoginReturn> returnType = new ReturnType<LoginReturn>();

            try
            {


            }
            catch (Exception ex)
            {
                returnType.SetException(ex);
            }
            return returnType;

        }




    }
}
