using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Autocomplete;
using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.RestApi.Helper;
using Firebase.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FormDefinationController : Controller
    {
        IFormDefinationService _formDefinationService = null;
        IMemoryCache _memoryCache;
        IFirebaseStorage _firebaseStorage;
        public FormDefinationController(IFormDefinationService formDefinationService,
            IMemoryCache memoryCache,
            IFirebaseStorage firebaseStorage)
        {
            _formDefinationService = formDefinationService;
            _memoryCache = memoryCache;
            _firebaseStorage = firebaseStorage;
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

        [HttpGet("GetFormDefinationField/{id}")]
        public IActionResult GetFormDefinationField(int id)
        {
            var key = $"FormDefinationFieldId{id}";
            if (_memoryCache.TryGetValue(key, out DefaultReturn<FormDefinationField> list))
                return Ok(list);

            var formDefinationField = _formDefinationService.GetFormDefinationField(id);

            _memoryCache.Set(key, formDefinationField, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });


            return Ok(formDefinationField);


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
        [HttpGet("CreateComboBoxItem/{tagName}")]
        public IActionResult CreateComboBoxItem(string tagName)
        {
            DefaultReturn<ComboBoxItem> defaultReturn = new DefaultReturn<ComboBoxItem>();

            defaultReturn.Data = new ComboBoxItem()
            {
                MainCompanyId = User.GetCompanyId(),
                ItemType = tagName,
                Name = "",
                TagName = "",
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

        [HttpGet("GetFieldTypes")]
        public IActionResult GetFieldTypes()
        {
            var companyId = User.GetCompanyId();
            string key = $"CompanyFieldTypes{companyId}";


            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<FieldType>> list))
                return Ok(list);


            var fieldTypeList = _formDefinationService.GetFielTypes(companyId);
            _memoryCache.Set(key, fieldTypeList, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(fieldTypeList);
        }


        [HttpGet("CreateNewGroupField")]
        [AllowAnonymous]
        public IActionResult CreateNewGroupField(int formDefinationId, int formGroupId)
        {
            DefaultReturn<FormDefinationField> defaultReturn = new DefaultReturn<FormDefinationField>();

            var grupList = GetGroupFieldsReturnList(formGroupId);
            var maxOrder = (grupList.Data.Count == 0 ? 0 : grupList.Data.Max(s => s.OrderNumber));
            maxOrder = maxOrder + 10;
            defaultReturn.Data = new FormDefinationField()
            {

                Deleted = false,
                AutoComplate = false,
                Bold = false,
                CellName = "",
                ControlType = "Text",
                FieldCaption = "",
                FormGroupId = formGroupId,
                DefaultProp = "",
                FontSize = 20,
                Mandatory = false,
                TagName = "",
                OrderNumber = maxOrder,
                TranslateLanguage = "",
                Italic = false,
                FormDefinationId = formDefinationId,
                FontFamily = "Times New Roman",
                AutoComlateType = "",

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

        private DefaultReturn<List<FormDefinationField>> GetGroupFieldsReturnList(int formGroupId)
        {
            string key = $"FormGroupFields{formGroupId}";
            DefaultReturn<List<FormDefinationField>> defaultReturn = null;
            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<FormDefinationField>> list))
                return list;


            defaultReturn = _formDefinationService.GetFormDefinationFields(formGroupId);

            _memoryCache.Set(key, defaultReturn, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return defaultReturn;
        }

        [HttpGet("GetGroupFields/{formGroupId}")]
        public IActionResult GetGroupFields(int formGroupId)
        {
            return Ok(GetGroupFieldsReturnList(formGroupId));
        }

        [HttpGet("GetFormDefinationVersions/{formdefinationid}")]

        public IActionResult GetFormDefinationVersions(int formdefinationid)
        {
            string key = $"FormDefinationVersions{formdefinationid}";
            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<FormVersion>> list))
                return Ok(list);

            var defaultReturn = _formDefinationService.GetDefinationFormVersions(formdefinationid);


            _memoryCache.Set(key, defaultReturn, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(defaultReturn);
        }

        [HttpGet("GetFormDefinationVersion/{id}")]

        public IActionResult GetFormDefinationVersion(int id)
        {
            string key = $"FormDefinationVersion{id}";
            if (_memoryCache.TryGetValue(key, out DefaultReturn<FormVersion> list))
                return Ok(list);

            var defaultReturn = _formDefinationService.GetFormDefinationVersion(id);


            _memoryCache.Set(key, defaultReturn, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(defaultReturn);
        }

        [HttpPost("SaveFormVersion")]
        public async Task<IActionResult> SaveFormVersion(IFormCollection data)
        {
            FormVersion formVersion = new FormVersion()
            {
                Id = Convert.ToInt32(data["Id"]),
                Active = Convert.ToBoolean(data["Active"]),
                FormLanguage = data["FormLanguage"].ToString(),
                FormDefinationId = Convert.ToInt32(data["FormDefinationId"]),
                CreatedBy = User.GetUserFullName(),
                CreatedDate = DateTime.Now,
                CreatedId = User.GetUserId()
            };
            if (formVersion.Id != 0)
            {
                formVersion.EditedBy = User.GetUserFullName();
                formVersion.EditedDate = DateTime.Now;
                formVersion.EditedId = User.GetUserId();
            }

            if (HttpContext.Request.Form.Files.Count != 0)
            {
                var file = HttpContext.Request.Form.Files[0];
                formVersion.FileName = file.FileName;
                var fileStream = file.OpenReadStream();
                formVersion.FilePath = await _firebaseStorage.SaveFileToStorageAsync("Template", Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName), fileStream);
            }

            var formDefinationReturn = _formDefinationService.Save(formVersion);

            string key = $"FormDefinationVersions{formVersion.FormDefinationId}";
            _memoryCache.Remove(key);
        

            key = $"FormDefinationVersion{formVersion.Id}";
            _memoryCache.Remove(key);

            return Ok(formDefinationReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormCollection data)
        {
            string key = $"FormDefinationCompany{User.GetCompanyId()}";
            _memoryCache.Remove(key);
            FormDefination formDefination = new FormDefination()
            {
                Id = Convert.ToInt32(data["Id"]),
                FormName = data["FormName"].ToString(),
                CustomSectorId = Convert.ToInt32(data["customSectorId"]),
                Deployed = Convert.ToBoolean(data["Deployed"]),
                MainCompanyId = User.GetCompanyId(),
                DesingTemplate = Convert.ToBoolean(data["DesingTemplate"]),
                TemplateFileName = "",


            };
            key = $"FormDefination{formDefination.Id}";
            _memoryCache.Remove(key);

            if (HttpContext.Request.Form.Files.Count != 0)
            {
                var file = HttpContext.Request.Form.Files[0];
                formDefination.TemplateFileName = file.FileName;
                var fileStream = file.OpenReadStream();
                formDefination.TemplatePath = await _firebaseStorage.SaveFileToStorageAsync("Template", Guid.NewGuid().ToString("N") + ".html", fileStream);
            }

            var formDefinationReturn = _formDefinationService.Save(formDefination);
            key = $"FormDefinationBySector_{User.GetCompanyId()}_{formDefination.CustomSectorId}";
            _memoryCache.Remove(key);


            return Ok(formDefinationReturn);
        }


        /*
        [HttpGet("SaveFileStorageTest")]
        [AllowAnonymous]
        public async Task<IActionResult> SaveFileStorageTestAsync()
        {
            var stream = System.IO.File.OpenRead("FormTemlate/OnSaglik.html");

            //var fff = await _firebaseStorage.Save();

            var sss = await _firebaseStorage.SaveFileToStorageAsync("Template", Guid.NewGuid().ToString("N") + ".html", stream);


            return  Ok(sss);
        }*/

        [HttpPost("SaveGroup")]
        public IActionResult SaveGroup(FormGroup formGroup)
        {

            var formGroupReturn = _formDefinationService.SaveGroup(formGroup);
            string key = $"FormDefinationGroups{formGroup.FormDefinationId}";
            _memoryCache.Remove(key);

            key = $"FormDefinationGroup{formGroup.Id}";
            _memoryCache.Remove(key);

            key = $"FormDefinationGroupDTO{formGroup.FormDefinationId}";
            _memoryCache.Remove(key);

            return Ok(formGroupReturn);
        }
        [HttpPost("SaveFormDefinationField")]
        [AllowAnonymous]
        public IActionResult SaveFormDefinationField(FormDefinationField formDefinationField)
        {


            var formGroupReturn = _formDefinationService.SaveFormDefinationField(formDefinationField);

            string key = $"FormGroupFields{formDefinationField.FormGroupId}";
            _memoryCache.Remove(key);
            key = $"FormDefinationFieldId{formDefinationField.Id}";
            _memoryCache.Remove(key);
            key = $"FormDefinationGroupDTO{formDefinationField.FormDefinationId}";
            _memoryCache.Remove(key);

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
        [HttpGet("GetFonts")]
        public ActionResult GetFonts()
        {
            string key = $"FontTypes";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<FontType>> list))
                return Ok(list);


            var fontTypeslist = _formDefinationService.GetFontTypes();

            _memoryCache.Set(key, fontTypeslist, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(2),
                Priority = CacheItemPriority.Normal
            });

            return Ok(fontTypeslist);
        }
        [HttpGet("GetComboBoxItems/{fieldTag}")]
        public IActionResult GetComboBoxItems(string fieldTag)
        {
            string key = $"ComboBoxItems_{fieldTag}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<ComboBoxItem>> list))
                return Ok(list);

            var fieldTags = _formDefinationService.GetComboBoxItems(User.GetCompanyId(), fieldTag);
            _memoryCache.Set(key, fieldTags, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(2),
                Priority = CacheItemPriority.Normal
            });

            return Ok(fieldTags);


        }

        [HttpGet("GetFormDefinationBySector/{sectorid}")]
        public IActionResult GetFormDefinationBySector(int sectorid)
        {
            string key = $"FormDefinationBySector_{User.GetCompanyId()}_{sectorid}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<FormDefination>> list))
                return Ok(list);


            var formDefinations = _formDefinationService.GetCompanyDefination(User.GetCompanyId(), sectorid);
            _memoryCache.Set(key, formDefinations, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(2),
                Priority = CacheItemPriority.Normal
            });

            return Ok(formDefinations);

        }

        [HttpPost("SaveComboBoxItem")]
        public IActionResult SaveComboBoxItem(ComboBoxItem comboBoxItem)
        {
            comboBoxItem.MainCompanyId = User.GetCompanyId();
            var comboBoxReturn = _formDefinationService.Save(comboBoxItem);
            string key = $"ComboBoxItems_{comboBoxItem.ItemType}";
            _memoryCache.Remove(key);
            return Ok(comboBoxReturn);
        }

        [HttpGet("GetFormGroupFormApp/{formdefinationId}")]

        public IActionResult GetFormGroupFormApp(int formdefinationId)
        {
            string key = $"FormDefinationGroupDTO{formdefinationId}";
            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<GroupDTO>> list))
                return Ok(list);


            var formGroups = _formDefinationService.GetFormGroupDTOs(formdefinationId);
            _memoryCache.Set(key, formGroups, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal
            });

            return Ok(formGroups);
        }

        [HttpGet("GetAutoComlateField/{formDefinationId}")]
        public IActionResult GetAutoComlateField(int formdefinationId)
        {
            string key = $"GetAutoComlateField{formdefinationId}";
            if (_memoryCache.TryGetValue(key, out DefaultReturn<AutocompleteField> list))
                return Ok(list);

            var formAutoComplate = _formDefinationService.GetAutoComplateField(formdefinationId);
            _memoryCache.Set(key, formAutoComplate, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal
            });

            return Ok(formAutoComplate);
        }

        [HttpGet("GetAutoComlateFieldMaps/{formDefinationId}")]
        public IActionResult GetAutoComlateFieldMaps(int formdefinationId)
        {
            string key = $"GetAutoComlateFieldMaps{formdefinationId}";
            if (_memoryCache.TryGetValue(key, out DefaultReturn<List<AutocompleteFieldMap>> list))
                return Ok(list);

            var formAutoComplate = _formDefinationService.GetAutoComplateFieldMaps(formdefinationId);
            _memoryCache.Set(key, formAutoComplate, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal
            });

            return Ok(formAutoComplate);
        }
        /*
        [HttpGet("CreateAutoComplateField/{formdefinationFieldId}/{complateObject}/{filterValue}")]
        public IActionResult CreateAutoComplateField(int formdefinationFieldId,string complateObject,string filterValue)
        {
            DefaultReturn<AutocompleteField> defaultReturn = new DefaultReturn<AutocompleteField>();
            var formDefinationField = _formDefinationService.GetFormDefinationField(formdefinationFieldId);
            defaultReturn.Data = new AutocompleteField()
            {
                FormDefinationFieldId = formdefinationFieldId,
                ComplateObject = complateObject,
                FilterValue= filterValue,
                FieldName= formDefinationField.Data.TagName,

            };

            return Ok(defaultReturn);
        }*/

        [HttpGet("GetReflectionFields")]
        public IActionResult GetReflectionFields(string complateObject)
        {
            var objectListReturn = _formDefinationService.GetObjectFieldList(complateObject, User.GetUserLangId());


            return Ok(objectListReturn);
        }


        [HttpPost("SaveAutoComplateField")]
        public IActionResult SaveAutoComplateField(SaveAutoComplateDTO saveAutoComplateDTO)
        {
            var defaultReturn = _formDefinationService.SaveAutoComplate(saveAutoComplateDTO);
            string key = $"GetAutoComlateFieldMaps{saveAutoComplateDTO.Complate.FormDefinationFieldId}";
            _memoryCache.Remove(key);

            key = $"GetAutoComlateField{saveAutoComplateDTO.Complate.FormDefinationFieldId}";

            _memoryCache.Remove(key);
            return Ok(defaultReturn);
        }

        [HttpGet("DeleteAutoComplateFieldMap")]
        public IActionResult DeleteAutoComplateFieldMap(int formdefinationId, int autoComplateMapid)
        {
            var deleteReturn = _formDefinationService.DeleteAutoComplate(autoComplateMapid);

            string key = $"GetAutoComlateFieldMaps{formdefinationId}";
            _memoryCache.Remove(key);

            return Ok(deleteReturn);

        }
        [HttpGet("GetFormDefinationTemplate/{formdefinationid}")]
        public IActionResult GetFormDefinationTemplate(int formdefinationid)
        {

            var formTemplate = _formDefinationService.GetTemplateForm(formdefinationid, User.GetCompanyId());


            return Ok(formTemplate);
        }

    }

}
