using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.RestApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FormDefinationController : Controller
    {
        IFormDefinationService _formDefinationService = null;
        IMemoryCache _memoryCache;
        public FormDefinationController(IFormDefinationService formDefinationService,
            IMemoryCache memoryCache)
        {
            _formDefinationService = formDefinationService;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var companyId = User.GetCompanyId();
            string key = $"FormDefinationCompany{companyId}";
            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<FormDefination>> list))
                return Ok(list);


            var companyDefination = _formDefinationService.GetCompanyDefinations(companyId);
            _memoryCache.Set(key, companyDefination, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(companyDefination);

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var companyId = User.GetCompanyId();
            string key = $"FormDefination{id}";
            if (_memoryCache.TryGetValue(key, out DefaultReturn<FormDefination> list))
                return Ok(list);


            var companyDefination = _formDefinationService.GetFormDefination(id);
            _memoryCache.Set(key, companyDefination, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(companyDefination);

        }

        [HttpGet("CreateFormDefination")]
        public IActionResult CreateFormDefination()
        {
            DefaultReturn<FormDefination> defaultReturn = new DefaultReturn<FormDefination>();

            defaultReturn.Data = new FormDefination()
            {
                CreatedDate = DateTime.Now,
                CreatedId = User.GetUserId(),
                MainCompanyId = User.GetCompanyId(),
                FormName = "",
                CreatedBy = User.GetUserFullName(),
                CustomSectorName = "",
                TemplatePath = "",
                DesingTemplate = false,
                Deleted = false,


            };
            return Ok(defaultReturn);
        }

        [HttpGet("CreateFormDefinationGroup")]
        public IActionResult CreateFormDefinationGroup(int formDefinationId)
        {
            DefaultReturn<FormGroup> defaultReturn = new DefaultReturn<FormGroup>();

            defaultReturn.Data = new FormGroup()
            {

                Deleted = false,
                AllowEditCustomer = false,
                FormNumber = "",
                GroupTag = "",
                Name = "",
                OrderNumber = 0,
                FormDefinationId = formDefinationId,



            };
            return Ok(defaultReturn);
        }


        [HttpGet("CreateNewGroupField")]
        public IActionResult CreateNewGroupField(int formDefinationId, int formGroupId)
        {
            DefaultReturn<FormDefinationField> defaultReturn = new DefaultReturn<FormDefinationField>();

            defaultReturn.Data = new FormDefinationField()
            {

                Deleted = false,
                AutoComplate = false,
                Bold = false,
                CellName = "",
                ControlType = "TextBox",
                FieldCaption = "",
                FormGroupId = formGroupId,
                DefaultProp = "",
                FontSize = 19,
                Mandatory = false,
                TagName = "",
                OrderNumber = 0,
                TranslateLanguage = "",
                Italic = false,
                FormDefinationId = formDefinationId,
                

            };
            return Ok(defaultReturn);
        }



        [HttpGet("GetSectors")]
        public IActionResult GetSectors()
        {
            var companyId = User.GetCompanyId();
            string key = $"FormSector{companyId}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<CustomSectorDTO>> list))
                return Ok(list);


            var sectorList = _formDefinationService.GetSector(companyId, User.GetUserLangId());
            _memoryCache.Set(key, sectorList, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(sectorList);
        }

        [HttpGet("GetFormGroups")]

        public IActionResult GetFormGroups(int formDefinationId)
        {
            string key = $"FormDefinationGroups{formDefinationId}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<FormGroup>> list))
                return Ok(list);


            var formGroupList = _formDefinationService.GetFormGroups(formDefinationId);

            _memoryCache.Set(key, formGroupList, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(formGroupList);
        }

        [HttpGet("GetFormGroup/{id}")]

        public IActionResult GetFormGroup(int id)
        {
            string key = $"FormDefinationGroup{id}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<FormGroup> list))
                return Ok(list);


            var formGroupList = _formDefinationService.GetFormGroup(id);

            _memoryCache.Set(key, formGroupList, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(formGroupList);
        }


        [HttpGet("GetGroupFields/{formGroupId}")]
        public IActionResult GetGroupFields(int formGroupId)
        {
            string key = $"FormGroupFields{formGroupId}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<FormDefinationField>> list))
                return Ok(list);


            var formGroupList = _formDefinationService.GetFormGroups(formGroupId);

            _memoryCache.Set(key, formGroupList, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(formGroupList);
        }

        [HttpPost]
        public IActionResult Post(FormDefination formDefination)
        {
            string key = $"FormDefinationCompany{User.GetCompanyId()}";
            _memoryCache.Remove(key);

            key = $"FormDefination{formDefination.Id}";
            _memoryCache.Remove(key);

            var formDefinationReturn = _formDefinationService.Save(formDefination);

            return Ok(formDefinationReturn);
        }

        [HttpPost("SaveGroup")]
        public IActionResult SaveGroup(FormGroup formGroup)
        {
            string key = $"FormDefinationGroups{formGroup.FormDefinationId}";
            _memoryCache.Remove(key);

            key = $"FormDefinationGroup{formGroup.Id}";
            _memoryCache.Remove(key);
            var formGroupReturn = _formDefinationService.SaveGroup(formGroup);
            return Ok(formGroupReturn);
        }
        [HttpGet("DeleteGroup")]
        public IActionResult DeleteGroup(int formDefinationId, int groupId)
        {

            var deleteResult = _formDefinationService.DeleteGroup(formDefinationId, groupId, User.GetCompanyId());
            string key = $"FormDefinationGroups{formDefinationId}";
            _memoryCache.Remove(key);
            key = $"FormDefinationGroup{groupId}";
            _memoryCache.Remove(key);

            return Ok(deleteResult);


        }


    }

}
