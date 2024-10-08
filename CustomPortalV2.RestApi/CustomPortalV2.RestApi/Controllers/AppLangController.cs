﻿using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppLangController : ControllerBase
    {
        IAppLangService _appLangService;
        public AppLangController(IAppLangService appLangService)
        {
            _appLangService = appLangService;
        }

        [HttpGet]
        public IActionResult Get()
        {


            var langs = _appLangService.GetAppLangs();
            return Ok(langs);
        }

        [HttpPost("TranslateText")]
        public async Task<IActionResult> TranslateText(TranslateTextDTO translateText)
        {
            var translateReturn = await _appLangService.TranslateText(translateText);
            return Ok(translateReturn);
        }
    }
}
