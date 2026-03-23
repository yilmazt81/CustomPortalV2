using System;
using System.Collections.Generic;
using System.Text;

namespace CustomPortalV2.Core.Model.App
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string TokenHash { get; set; }

        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }

        public DateTime? AbsoluteExpiration { get; set; }

        public bool IsRevoked { get; set; }
        public DateTime? RevokedAt { get; set; }

        public int AppUserId { get; set; }
    }
}
