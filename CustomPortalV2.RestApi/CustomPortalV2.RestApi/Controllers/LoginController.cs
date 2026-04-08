using CustomPortalV2.Business;
using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Helper;
using CustomPortalV2.Business.Service;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Model;
using CustomPortalV2.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User = CustomPortalV2.Core.Model.App.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private IUserService _userservice;
        Helper.JwtService jwtService;
        // private ILogger _logger;
        public LoginController(
            IUserService userService,
            IConfiguration configuration)
        {
            _userservice = userService;
            Configuration = configuration;
            jwtService = new Helper.JwtService(configuration);
        }
        /*  private string generateJwtToken(User user)
          {


              var claims = new List<Claim> {
                                new Claim(ClaimTypes.Name,user.FullName.ToString()),
                                new Claim(ClaimTypes.GroupSid, user.MainCompanyId.ToString()),
                                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                                new Claim(ClaimTypes.PrimarySid, user.CompanyBranchId.ToString()),
                                new Claim(ClaimTypes.Locality, user.AppLangId.ToString())

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

          }*/
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


        [HttpPost("RefreshToken")]
        public async Task<IActionResult> Refresh(TokenDto dto)
        {


            try
            {
                var token = _userservice.GenerateRefreshToken(dto.refreshToken);

                var user = _userservice.GetById(token.Data.MainCompanyId, token.Data.AppUserId);

                var newAccess = jwtService.GenerateAccessToken(user.Data);
                var newRefresh = jwtService.GenerateRefreshToken();

                var newToken = new RefreshToken
                {
                    TokenHash = TokenHasher.Hash(newRefresh),
                    Created = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddDays(14), // Sliding
                    AbsoluteExpiration = token.Data.AbsoluteExpiration, // Değişmez
                    AppUserId = user.Data.Id,
                    MainCompanyId= user.Data.MainCompanyId
                };

                _userservice.AddRefreshToken(newToken);

                DefaultReturn<RefreshTokenReturnDTO> defaultReturn = new DefaultReturn<RefreshTokenReturnDTO>()
                {
                    Data = new RefreshTokenReturnDTO()
                    {
                        accessToken = newAccess,
                        refreshToken = newRefresh
                    }
                };
                return Ok(defaultReturn);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
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
                        var accessToken = jwtService.GenerateAccessToken(user);
                        var refreshToken = jwtService.GenerateRefreshToken();


                        var refToken = new RefreshToken
                        {
                            TokenHash = TokenHasher.Hash(refreshToken),
                            Expires = DateTime.UtcNow.AddDays(7),
                            Created = DateTime.UtcNow,
                            AppUserId= user.Id,
                            AbsoluteExpiration = DateTime.UtcNow.AddDays(8),
                            MainCompanyId= user.MainCompanyId
                        };
                        _userservice.AddRefreshToken(refToken);
                        returnType.token = accessToken;
                        returnType.refreshtoken = refreshToken;
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
