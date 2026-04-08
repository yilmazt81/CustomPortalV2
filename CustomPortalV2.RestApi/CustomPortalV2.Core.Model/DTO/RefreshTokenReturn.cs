using System;
using System.Collections.Generic;
using System.Text;

namespace CustomPortalV2.Core.Model.DTO
{
    public class RefreshTokenReturnDTO
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
