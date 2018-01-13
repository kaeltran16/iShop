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
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
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
