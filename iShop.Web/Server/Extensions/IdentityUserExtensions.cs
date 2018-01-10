using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace iShop.Web.Server.Extensions
{
    public static class IdentityUserExtensions
    {
        // Just an extension method for getting UserId
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return id;
        }
    }
}
