using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOS.Identity;

namespace ECommerce.Application.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService _identityService;
      

        public async Task<Result<bool>> CheckEmailExsistsAsync(string email, CancellationToken ct = default)
        {
            return await _identityService.EmailExistAsync(email, ct);
        }

        public async Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken ct = default)
        {
            var userRes = await _identityService.FindUserByEmailAsync(email, ct);
            var user = userRes.Data;


            return new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
            };

        }



        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            //get user by email
            var userRes = await _identityService.FindUserByEmailAsync(loginDto.Email);
            if (!userRes.IsSuccess) return Result<UserDto>.Fail(userRes.Errors);
            //check password

            var passwordRes = await _identityService.CheckPasswordAsync(loginDto.Email, loginDto.Password);
            if (!passwordRes.IsSuccess) return Result<UserDto>.Fail(passwordRes.Errors);
            if (!passwordRes.Data) return Result<UserDto>.Fail(Error.Unauthorized("Invalid Email OR Password"));



            var user = userRes.Data;


            return new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
            };
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var userRes = await _identityService.CreateUserAsync(registerDto);
            if (!userRes.IsSuccess) return Result<UserDto>.Fail(userRes.Errors);


            var user = userRes.Data;
            return Result<UserDto>.Ok(new UserDto()
            {

                Email = user.Email,
                DisplayName = user.DisplayName,
            });


        }

    



        public async Task<Result<AddressDto>> GetUserAddressAsync(string email, CancellationToken ct = default)
        {
            return await _identityService.GetUserAddressByEmailAsync(email, ct);
        }

        public async Task<Result<AddressDto>> UpSertAddressAsync(string email, AddressDto addressDto, CancellationToken ct = default)
        {

            return await _identityService.UpdateOrInsertAddressAsync(email, addressDto, ct);
        }
    }
}
