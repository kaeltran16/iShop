using System;
using Microsoft.AspNetCore.Identity;

namespace iShop.Web.Server.Core.Models
{
    public class ApplicationRole: IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
