using CustomPortalV2.Model;
using CustomPortalV2.Model.DTO;
using CustomPortalV2.Business;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.App;

using User = CustomPortalV2.Core.Model.App.User;
using CustomPortalV2.Core.Model.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private IUserService _userservice;
        // private ILogger _logger;
        public LoginController(
            IUserService userService,
            IConfiguration configuration)
        {
            _userservice = userService;
            Configuration = configuration;
        }
        private string generateJwtToken(User user)
        {


            var claims = new List<Claim> {
                              new Claim(ClaimTypes.Name,user.FullName.ToString()),
                              new Claim(ClaimTypes.GroupSid, user.MainCompanyId.ToString()),
                              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                              new Claim(ClaimTypes.PrimarySid, user.CompanyBranchId.ToString()),
                 };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"])
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);

            /*
            var secretKey = Configuration.GetSection("ApplicationSettings:JWT_Secret");
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey.Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                         {
                              new Claim(ClaimTypes.Name,user.FullName.ToString()),
                              new Claim(ClaimTypes.GroupSid, user.MainCompanyId.ToString()),
                              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                              new Claim(ClaimTypes.PrimarySid, user.CompanyBranchId.ToString()),
                         }
                         ),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

            */
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
        [AllowAnonymous]
        public IActionResult Post([FromBody] LoginRequest loginRequest)
        {
            LoginReturn returnType = new LoginReturn();

            try
            {
                var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
                User? user = null;
                var loginReturn = _userservice.Login(remoteIpAddress.ToString(),
                    loginRequest.CompanyCode,
                    loginRequest.UserName,
                    loginRequest.password, ref user);
                if (loginReturn == Business.Helper.Enums.enumLoginReturn.Success)
                {
                    if (user != null)
                    {
                        returnType.token = generateJwtToken(user);
                        returnType.UserId = user.Id;
                        returnType.IsLogin = true;
                    }
                }
                else
                {
                    returnType.ReturnMessage = loginReturn.ToString();

                }
            }
            catch (Exception ex)
            {
                returnType.ReturnMessage = ex.Message;
            }
            return Ok(returnType);

        }


    }
}
