using System.ComponentModel.DataAnnotations;

namespace iShop.Web.Server.Core.Resources
{
    public class RegisterResource
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [StringLength(100)]
        public string District { get; set; }
        [StringLength(100)]
        public string Ward { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }

    }
}
