using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Service;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.RestApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CustomPortalV2.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MailController : ControllerBase
    {
        IMailService _mailService;
        IMemoryCache _cache;
        public MailController(IMailService mailService, IMemoryCache cache)
        {
            _mailService = mailService;
            _cache = cache;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {

            var mailList = _mailService.GetCompanyMails(User.GetCompanyId(), User.GetBranchId());
            return Ok(mailList);
        }

        [HttpPost]
        public IActionResult Post(AttachmentSendMailDTO attachmentSendMailDTO)
        {

            var sendMail= _mailService.SendMail(attachmentSendMailDTO);
            return Ok(sendMail);
        }
    }
}
