﻿using CustomPortalV2.Business.Concrete;
using CustomPortalV2.RestApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CreateFileController : ControllerBase
    {
        IFileCreateService _fileCreateService;
        Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public CreateFileController(IFileCreateService fileCreateService, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _fileCreateService = fileCreateService;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet("CreateFormVersion/{id}/{versionid}")]
        public IActionResult CreateFormVersion(int id, int versionid)
        {

            var formConvertReturn = _fileCreateService.ConvertFormVersion(id, versionid, User.GetCompanyId(), User.GetUserId(), _hostingEnvironment.ContentRootPath + @"\TempFolder\");
            return Ok(formConvertReturn);
        }

        [HttpGet("CreateFormAttachment/{id}/{attachmentId}")]
        public IActionResult CreateFormAttachment(int id, int attachmentId)
        {
            var formConvertReturn = _fileCreateService.ConvertAttachment(id, attachmentId, User.GetCompanyId(), User.GetUserId(), _hostingEnvironment.ContentRootPath + @"\TempFolder\");
            return Ok(formConvertReturn);
        }
    }
}
