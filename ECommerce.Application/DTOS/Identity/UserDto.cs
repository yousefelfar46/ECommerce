namespace ECommerce.Application.DTOS.Identity
{
    public class UserDto
    {
        public string Email { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
