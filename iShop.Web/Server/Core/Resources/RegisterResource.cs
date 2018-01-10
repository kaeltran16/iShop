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
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }

    }
}
