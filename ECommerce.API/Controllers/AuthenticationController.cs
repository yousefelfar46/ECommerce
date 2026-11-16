using Ecommerce.API.Controllers.BaseController;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOS.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{

    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
            => ToActionResult(await _authenticationService.LoginAsync(loginDto));

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
            => ToActionResult(await _authenticationService.RegisterAsync(registerDto));


        //Check Email Exist 

        [HttpGet("emailexist")]
        public async Task<ActionResult<bool>> CheckEmail([FromQuery] string email, CancellationToken ct)
        {
            return ToActionResult(await _authenticationService.CheckEmailExsistsAsync(email, ct));
        }
        //Get Current User 
        [Authorize]//have token
        [HttpGet("currentuser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser(CancellationToken ct)
        {



            return ToActionResult(await _authenticationService.GetCurrentUserAsync(GetEmailFromToken(), ct));
        }

        //Get Current Address
        [Authorize]//have token
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetCurrentAddress(CancellationToken ct)
        {


            return ToActionResult(await _authenticationService.GetUserAddressAsync(GetEmailFromToken(), ct));
        }

        //Update Current User Address
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto, CancellationToken ct)
        {

            return ToActionResult(await _authenticationService.UpSertAddressAsync(GetEmailFromToken(), addressDto, ct));
        }

    }
}
