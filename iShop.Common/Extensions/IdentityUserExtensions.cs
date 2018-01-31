using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace iShop.Common.Extensions
{
    public static class IdentityUserExtensions
    {
        // Just an extension method for getting UserId
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var id = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            return Guid.Parse(id);
        }
    }
}
