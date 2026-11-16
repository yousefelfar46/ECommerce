using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOS.Identity
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "You Must Enter Email"), EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string? PhoneNumber { get; set; }
    }
}
