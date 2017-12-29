using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace iShop.Web.Server.Core.Models
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        [Required]
        [StringLength(155)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(155)]
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }

        public ApplicationUser()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
