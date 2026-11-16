using ECommerce.Application.Common;
using ECommerce.Application.DTOS.Identity;

namespace ECommerce.Application.Contracts
{
    public interface IAuthenticationService
    {

        Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default);
        Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default);

        Task<Result<bool>> CheckEmailExsistsAsync(string email, CancellationToken ct = default);
        Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken ct = default);

        Task<Result<AddressDto>> GetUserAddressAsync(string email, CancellationToken ct = default);
        Task<Result<AddressDto>> UpSertAddressAsync(string email, AddressDto addressDto, CancellationToken ct = default);


    }
}
