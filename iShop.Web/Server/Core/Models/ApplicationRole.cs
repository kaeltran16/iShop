using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace iShop.Web.Server.Core.Models
{
    public class ApplicationRole: IdentityRole<Guid>
    {
        [StringLength(155)]
        public string Description { get; set; }
    }
}
