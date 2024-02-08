using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rivne.booking.Core.DTOs.Users;
using rivne.booking.Core.Services;
using rivne.booking.Core.Validations;
using rivne.booking.Core.DTOs;
using rivne.booking.api.Contracts;
using Rivne.Booking.Application.Users.Commands;
using ErrorOr;
using rivne.booking.Infrastructure.Repository;
using rivne.booking.api.Validation;
using System.Web;
using Microsoft.AspNetCore.Http.Extensions;


namespace rivne.booking.api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
	private readonly UserService _userService;
	private readonly UserRepository _userRepository;
	private readonly IHttpContextAccessor _httpContext;

	public UserController(UserService userService, UserRepository userRepository, IHttpContextAccessor httpContext)
	{
		_userService = userService;
		_userRepository = userRepository;
		_httpContext = httpContext;
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

	[AllowAnonymous]
	[HttpPost("RefreshToken")]
	public async Task<IActionResult> RefreshTokenAsync([FromBody] TokenRequestDto model)
	{
		var validator = new TokenRequestValidation();
		var validation = validator.Validate(model);

		if (validation.IsValid) 
		{
			//var res = await _userService.RefreshTokenAsync(model);

			//if (!res.Success) { return Ok(res); }
			//else { return BadRequest(res.Message); }

			return Ok();
		}
		else { return BadRequest(validation.Errors[0].ToString()); }
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
	public async Task<IActionResult> Register(RegisterUserRequest request)
	{
		var validator = new RegisterUserRequestValidation();

		var registerUserRequestValidation = await validator.ValidateAsync(request);

		if (!registerUserRequestValidation.IsValid) 
		{
			return BadRequest(registerUserRequestValidation.Errors[0]);
		}

		var command = new RegisterUserCommand {
			Email = request.Email,
			Password = request.Password,
			ConfirmPassword = request.ConfirmPassword,
			BaseUrl = _httpContext.HttpContext.Request.Host.Value
	};

		var commandHandler = new RegisterUserCommandHandler(_userRepository);

		var errorOrUser = await commandHandler.Execute(command);

		if (errorOrUser.IsError)
		{
			return BadRequest();
		}

		return Ok();
		
	}

	[AllowAnonymous]
	[HttpGet("ConfirmEmail")]
	public async Task<IActionResult> ConfirmEmailAsync(string userId, string token)
	{
		if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
			return NotFound();

		var result = await _userService.ConfirmEmailAsync(userId, token);

		if (result.Success)
		{
			return Ok(result);
		}
		return BadRequest(result);
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
	public async Task<IActionResult> AddAvatar([FromForm]IFormFile file)
	{
		string userId = User.Claims.First().Value;

		var result = await _userService.AddAvatarAsync(file,userId);

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
