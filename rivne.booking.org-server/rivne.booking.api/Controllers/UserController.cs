﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rivne.booking.Core.DTOs.Users;
using rivne.booking.Core.Services;
using rivne.booking.Core.Validations;

namespace rivne.booking.api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
	private readonly UserService _userService;

	public UserController(UserService userService)
	{
		_userService = userService;
	}

	[AllowAnonymous]
	[HttpPost("login")]
	public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto model)
	{
		var result = await _userService.LoginUserAsync(model);

		if(result.Success)
			return Ok(result);
		else
			return BadRequest(result.Message);
	}

	[HttpGet("logout")]
	public async Task<IActionResult> LogoutUserAsync(string userID)
	{
		var result = await _userService.LogoutUserAsync(userID);

		if (result.Success)
		{
			return Ok(result);
		}

		return BadRequest(result);
	}

	[HttpPost("updateProfile")]
	public async Task<IActionResult> ProfileUpdate(UpdateProfileDto model)
	{
		var validator = new UpdateProfileValidation();

		var validationResult = await validator.ValidateAsync(model);

		if (validationResult.IsValid)
		{
			var result = await _userService.UpdateProfileAsync(model);

			if (result.Success) return Ok(result); 
			else return BadRequest(result.Message); 
		}
		else
		{
			return BadRequest(validationResult.Errors);
		}
	}
 
	[HttpGet("getAll")]
	public async Task<IActionResult> GetAll()
	{
		var result = await _userService.GetAllUsersAsync();

		if (result.Success) return Ok(result);
		else return BadRequest(result.Message);
	}

	[AllowAnonymous]
	[HttpPost("register")]
	public async Task<IActionResult> Register(RegisterUserDto model)
	{
		var validator = new RegisterUserValidation();

		var result = validator.Validate(model);

		if (result.IsValid)
		{
			var newUserResult = await _userService.RegisterUserAsync(model);

			if (newUserResult.Success) { return Ok(newUserResult); }
			else { return BadRequest(newUserResult); }
		}
		else
		{
			return BadRequest(result.Errors[0].ToString());
		}
	}

	[HttpPost("deleteUser")]
	public async Task<IActionResult> DeleteUser([FromBody] string id)
	{
		var result = await _userService.DeleteUserAsync(id);

		if (result.Success) return Ok(result);
		else return BadRequest(result.Message);
	}

	[HttpGet("getUser")]
	public async Task<IActionResult> GetUser(string userID)
	{
		var result = await _userService.GetUserAsync(userID);

		if (result.Success)
		{
			return Ok(result);
		}
		return BadRequest(result);
	}

	[HttpPost("editUser")]
	public async Task<IActionResult> EditUser(EditUserDto model)
	{
		var validator = new EditUserValidation();

		var result = validator.Validate(model);

		if (result.IsValid)
		{
			var editResult = await _userService.EditUserAsync(model);

			if (editResult.Success)
			{
				return Ok(editResult);
			}
			else
			{
				return BadRequest(editResult);
			}
		}
		else
		{
			return BadRequest(result.Errors[0].ToString());
		}
	}

	[HttpPost("addUser")]
	public async Task<IActionResult> AddUser(AddUserDto model)
	{
		var validator = new AddUserValidation();

		var result = validator.Validate(model);

		if (result.IsValid)
		{
			var newUserresult = await _userService.CreateUserAsync(model);

			if (newUserresult.Success)
			{
				return Ok(newUserresult);
			}
			else
			{
				return BadRequest(newUserresult);
			}
		}
		else
		{
			return BadRequest(result.Errors[0].ToString());
		}
	}

	[HttpPost("addAvatar")]
	public async Task<IActionResult> AddAvatar(AddAvatarDto model)
	{
		string id = User.Claims.First().Value;

		var result = await _userService.AddAvatarAsync(model);

		if (result.Success)
		{
			return Ok(result);
		}
		else
		{
			return BadRequest(result);
		}
	}
}
