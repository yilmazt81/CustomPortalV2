using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Model.Company;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        ICompanyService companyService;
        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        // GET: api/<CompanyController>
        [HttpGet]
        public IActionResult Get()
        {
            //return new string[] { "value1", "value2" };
            string data = "{\"Ad\": \"Harun\" , \"Soyad\": \"Aydin\"}";
            return Ok(JObject.Parse(data));
        }

        // GET api/<CompanyController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CompanyController>
        [HttpPost]
        public IActionResult Post([FromBody] Company company)
        {

            var returnV = companyService.AddCompany(company);

            return Ok(returnV);

        }
    }
}
