using Bank.Interfaces;
using Bank.Mappers;
using Bank.Models;
using Bank.Models.AppUserDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bank.Controllers {
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase {
		private readonly UserManager<AppUser> _userManager;
		private readonly ITokenService _tokenService;
		private readonly SignInManager<AppUser> _signInManager;
		public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager) {
			_userManager = userManager;
			_tokenService = tokenService;
			_signInManager = signInManager;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(OpenAppUser openAppUser) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == openAppUser.UserName);

			if (appUser == null)
				return Unauthorized("User doesn't exist.");

			var result = await _signInManager.CheckPasswordSignInAsync(appUser, openAppUser.Password, false);

			if (!result.Succeeded)
				return Unauthorized("Wrong password.");

			var token = await _tokenService.CreateTokenAsync(appUser);
			return Ok(appUser.ToReadAppUser(token));
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] CreateAppUser registerAppUser) {
			try {
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var appUser = new AppUser {
					UserName = registerAppUser.UserName,
					Email = registerAppUser.Email,
				};

				var createdUser = await _userManager.CreateAsync(appUser, registerAppUser.Password ?? "");

				if (createdUser.Succeeded) {
					var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
					if (roleResult.Succeeded) {
						var token = await _tokenService.CreateTokenAsync(appUser);
						return Ok(appUser.ToReadAppUser(token));
					} else {
						return StatusCode(500, roleResult.Errors);
					}
				} else {
					return StatusCode(500, createdUser.Errors);
				}
			} catch (Exception ex) {
				return StatusCode(500, ex);
			}
		}
	}
}
