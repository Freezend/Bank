﻿using Bank.Interfaces;
using Bank.Models;
using Bank.Models.AppUserDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers {
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase {
		private readonly UserManager<AppUser> _userManager;
		private readonly ITokenService _tokenService;
		public AccountController(UserManager<AppUser> userManager, ITokenService tokenService) {
			_userManager = userManager;
			_tokenService = tokenService;
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
						return Ok(new ReadAppUser {
							UserName = appUser.UserName ?? "",
							Email = appUser.Email ?? "",
							Token = _tokenService.CreateToken(appUser)
						});
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