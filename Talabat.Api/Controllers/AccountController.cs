using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Api.Dtos;
using Talabat.Api.Extensions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.Api.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , IAuthService authService ,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null) return Unauthorized();
            var sign = await _signInManager.CheckPasswordSignInAsync(user, login.Password , false);
            if (sign.Succeeded == false) return Unauthorized();
            return Ok(new UserDto() { DisplayName = user.DisplayName, Email = user.Email, Token = await _authService.CreateTokenAsync(user , _userManager) });
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if(CheckEmailExists(model.Email).Result.Value) 
                return BadRequest(new {Message = "this Email Is Already Exist"});
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split("@")[0]
            };
            var result = await _userManager.CreateAsync(user , model.Password);
            if (result.Succeeded is false) return BadRequest();
            return Ok(new UserDto() { DisplayName = user.DisplayName, Email = user.Email  , Token = await _authService.CreateTokenAsync(user, _userManager) });
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(Email);
            return Ok(new UserDto() { Email = user.Email,DisplayName = user.DisplayName , Token = await _authService.CreateTokenAsync(user , _userManager) });
        }
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            
            var user = await _userManager.FindUserWithAddressAsync(User);
            var address = _mapper.Map<AddressDto>(user.Address);
            return Ok(address);
        }
        //[Authorize]
        //[HttpPut("address")]
        //public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto updatedAddress)
        //{
        //    var address = _mapper.Map<AddressDto, Address>(updatedAddress);
        //    //var email = User.FindFirstValue(ClaimTypes.Email);
        //    var user = await _userManager.FindUserWithAddressAsync(User);
        //    address.Id = user.Address.Id;
        //    user.Address = address; 
        //    //user.Address.FName = address.FName;
        //    //user.Address.LName = address.LName;
        //    //user.Address.Street = address.Street;
        //    //user.Address.City = address.City;
        //    //user.Address.Country = address.Country;
        //    var result  = await _userManager.UpdateAsync(user);
        //    if (!result.Succeeded) return BadRequest();
        //    return Ok(address); 
        //}
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}
