using CustomPortalV2.Core.Model.App;
using System.Security.Claims;

namespace CustomPortalV2.RestApi.Helper
{
    public static class ApiExtention
    {

        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return int.Parse(userId);
        }
        public static int GetBranchId(this ClaimsPrincipal user)
        {
            var branchId = user.FindFirst(ClaimTypes.PrimarySid)?.Value;
            return int.Parse(branchId);
        }
        public static int GetCompanyId(this ClaimsPrincipal user)
        {

            var companyId = user.FindFirst(ClaimTypes.GroupSid)?.Value;
            return int.Parse(companyId);
        }

    }
}
