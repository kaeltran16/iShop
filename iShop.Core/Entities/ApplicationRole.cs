using System;
using Microsoft.AspNetCore.Identity;

namespace iShop.Core.Entities
{
    public class ApplicationRole: IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
