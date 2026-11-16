using ECommerce.Application.Common;
using ECommerce.Application.DTOS.Identity;

namespace ECommerce.Application.Contracts
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default);
        Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default);
        Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken ct = default);
        Task<Result<IReadOnlyList<string>>> GetUserRolesAsync(string email, CancellationToken ct = default);


        Task<Result<bool>> EmailExistAsync(string email, CancellationToken ct = default);

        Task<Result<AddressDto>> GetUserAddressByEmailAsync(string email, CancellationToken ct = default);
        Task<Result<AddressDto>> UpdateOrInsertAddressAsync(string email, AddressDto addressDto, CancellationToken ct = default);




    }
}
