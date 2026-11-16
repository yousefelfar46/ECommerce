using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOS.Identity
{
    public class LoginDto
    {
        [Required(ErrorMessage = "You Must Enter Email"), EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
    }
}
