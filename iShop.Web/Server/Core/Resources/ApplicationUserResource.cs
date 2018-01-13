using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iShop.Web.Server.Core.Resources
{
    public class ApplicationUserResource
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Ward { get; set; }
        [StringLength(50)]
        public string District { get; set; }   
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }
    }
}
