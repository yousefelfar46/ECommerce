using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOS.Identity;
using ECommerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Service
{
    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Result<bool>.Fail(Error.NotFound("User Not Found", $"User With Email {email} Is Not Found"));
            else
                return await _userManager.CheckPasswordAsync(user, password);
        }



        public async Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Result<IdentityUserResult>.Fail(Error.NotFound("User Not Found", $"User With Email {email} Is Not Found"));
            else
                return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id, user.DisplayName, user.Email, user.UserName));


        }


        public async Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
                DisplayName = registerDto.DisplayName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => new Error(e.Code, e.Description)).ToList();
                return Result<IdentityUserResult>.Fail(errors);

            }
            return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id, user.DisplayName, user.Email, user.UserName));
        }

        public async Task<Result<IReadOnlyList<string>>> GetUserRolesAsync(string email, CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return Error.NotFound("User Not Found", $"User With Email {email} Is Not Found");

            var roles = await _userManager.GetRolesAsync(user);

            return roles.ToList();

        }

        public async Task<Result<bool>> EmailExistAsync(string email, CancellationToken ct = default)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

        public async Task<Result<AddressDto>> GetUserAddressByEmailAsync(string email, CancellationToken ct = default)
        {

            var user = await _userManager.Users.Include(u => u.Address)
                                               .FirstOrDefaultAsync(u => u.Email == email);

            if (user?.Address == null)
                return Result<AddressDto>.Fail(Error.NotFound("Address Is Not Founf", $"User {email} Has No Address"));


            var address = user.Address;
            return new AddressDto()
            {
                FirstName = address.FirstName,
                LastName = address.LastName,
                Country = address.Country,
                City = address.City,
                Street = address.Street
            };
        }

        public async Task<Result<AddressDto>> UpdateOrInsertAddressAsync(string email, AddressDto address, CancellationToken ct = default)
        {

            var user = await _userManager.Users.Include(u => u.Address)
                                               .FirstOrDefaultAsync(u => u.Email == email);


            if (user?.Address == null)
            {//Insert
                user.Address = new Address()
                {
                    FirstName = address.FirstName,
                    LastName = address.LastName,
                    Country = address.Country,
                    City = address.City,
                    Street = address.Street
                };
            }
            else
            {//Update

                user.Address.FirstName = address.FirstName;
                user.Address.LastName = address.LastName;
                user.Address.Country = address.Country;
                user.Address.City = address.City;
                user.Address.Street = address.Street;
            }

            var res = await _userManager.UpdateAsync(user);
            if (res.Succeeded) return address;
            else return Error.Failure("Failure", string.Join(';', res.Errors.Select(e => e.Description)));


        }
    }
}
