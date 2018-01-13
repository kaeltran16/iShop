using System;
using System.Security.Claims;

namespace iShop.Web.Server.Commons.Extensions
{
    public static class IdentityUserExtensions
    {
        // Just an extension method for getting UserId
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var id = principal.FindFirst("sub")?.Value;

            return Guid.Parse(id);
        }
    }
}
