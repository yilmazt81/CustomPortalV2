﻿using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.Core.Model.Form;
using CustomPortalV2.RestApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FormMetaDataController : ControllerBase
    {

        IFormMetaDataService _formMetaDataService;
        IFormDefinationService _formDefinationService;
        IMemoryCache _memoryCache;
        public FormMetaDataController(IFormMetaDataService formMetaDataService,
            IFormDefinationService formDefinationService,
            IMemoryCache memoryCache)
        {
            _formMetaDataService = formMetaDataService;
            _formDefinationService = formDefinationService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult Get()
        {

            var formMetaList = _formMetaDataService.GetBranchFormMetaData(User.GetCompanyId(), User.GetBranchId());


            return Ok(formMetaList);
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
            {
                var defaultReturn = new DefaultReturn<FormMetaData>();
                defaultReturn.Data = new FormMetaData()
                {
                    MainCompanyId = User.GetCompanyId(),
                    CompanyBranchId = User.GetBranchId(),
                    CreatedDate = DateTime.Now,
                };

                return Ok(defaultReturn);
            }
            else
            {
                var key = $"FormMetaData{id}";

                if (_memoryCache.TryGetValue(key, out DefaultReturn<FormMetaData> formData))
                {
                    return Ok(formData);
                }


                var formmetaData = _formMetaDataService.GetCompanyFormMetaData(User.GetCompanyId(), User.GetBranchId(), id);

                _memoryCache.Set(key, formmetaData, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    Priority = CacheItemPriority.Normal
                });

                return Ok(formmetaData);

            }
        }

        [HttpGet("CloneForm/{sourceFormid}")]
        public IActionResult CloneForm(int sourceFormid)
        {
            var cloneResult = _formMetaDataService.CloneForm(User.GetCompanyId(), User.GetBranchId(), User.GetUserId(), User.GetUserFullName(), sourceFormid);

            return Ok(cloneResult);
        }

        [HttpGet("GetConvertFileList/{id}")]
        public IActionResult GetConvertFileList(int id)
        {
            var key = $"GetConvertFileList{id}";

            if (_memoryCache.TryGetValue(key, out DefaultReturn<FormConvertContainerDTO> formData))
            {
                return Ok(formData);
            }
            var formmetaData = _formMetaDataService.GetFormConvertList(id, User.GetCompanyId());

            _memoryCache.Set(key, formmetaData, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            });

            return Ok(formmetaData);
        }

        [HttpGet("DeleteForm/{formid}")]
        public IActionResult DeleteForm(int formid)
        {
            var deleteReturn = _formMetaDataService.DeleteForm(User.GetCompanyId(), User.GetBranchId(), formid);


            return Ok(deleteReturn);
        }

        
        [HttpPost]
        public IActionResult Post(FormMetaDataDTO formMetaDataDTO)
        {
            formMetaDataDTO.UserId = User.GetUserId();
            formMetaDataDTO.CompanyId = User.GetCompanyId();
            formMetaDataDTO.UserName = User.GetUserFullName();
            formMetaDataDTO.BrachId = User.GetBranchId();

            var returnV = _formMetaDataService.Save(formMetaDataDTO);
            if (returnV.ReturnCode == 1)
            {
                var key = $"FormMetaData{returnV.Data.Id}";

                _memoryCache.Remove(key);

            }

            return Ok(returnV);
        }



    }
}
