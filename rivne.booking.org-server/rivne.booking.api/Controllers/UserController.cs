namespace rivne.booking.api.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class UserController(ISender mediatr, IHttpContextAccessor httpContext, IMapper mapper) : Controller
{
	//ToDo ???? Maybe make Authentication Controller
	[AllowAnonymous]
	[HttpPost("register")]
	public async Task<IActionResult> RegisterUserAsync(RegisterUserRequest request)
	{ 
		//ToDo ??? Is it wright
		var baseUrl = httpContext.HttpContext!.Request.Host.Value;

		var authResult = await mediatr.Send(mapper.Map<RegisterUserCommand>((request, baseUrl)));

		//ToDo ??? Maybe make override of controller for extend of retern result
		return authResult.Match(
			authResult => Ok(authResult),
			errors => Problem(errors[0].ToString()));
	}

	[AllowAnonymous]
	[HttpGet("ConfirmEmail")]
	public async Task<IActionResult> ConfirmEmailAsync([FromQuery] ConfirmEmailRequest request)
	{
		//ToDo ???? How to make redirect to frontend
		var confirmEmailResult = await mediatr.Send(mapper.Map<ConfirmEmailQuery>(request));

		return confirmEmailResult.Match(
			authResult => Ok(confirmEmailResult),
			errors => Problem(errors[0].ToString()));
	}

	[AllowAnonymous]
	[HttpPost("login")]
	public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserRequest request)
	{
		var loginResult = await mediatr.Send(mapper.Map<LoginUserQuery>(request));

		return loginResult.Match(
			loginResult => Ok(mapper.Map<LoginUserResponse>(loginResult)),
			errors => Problem(errors[0].ToString()));
	}

	[HttpGet("logout")]
	public async Task<IActionResult> LogoutUserAsync()
	{
		//ToDo validate userId
		//Global ErrorHandling
		string userId = User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value;

		var logOutResult = await mediatr.Send(new LogoutUserQuery(userId));

		return logOutResult.Match(
			logOutResult => Ok(logOutResult),
			errors => Problem(errors[0].ToString()));
	}

	[AllowAnonymous]
	[HttpPost("RefreshToken")]
	public async Task<IActionResult> RefreshToken([FromBody] TokenRefreshRequest request)
	{
		var refreshTokenResult = await mediatr.Send(mapper.Map<TokenRefreshCommand>(request));

		return refreshTokenResult.Match(
			refreshTokenResult => Ok(mapper.Map<TokenRefreshResponse>(refreshTokenResult)),
			errors => Problem(errors[0].ToString()));
	}

	[AllowAnonymous]
	[HttpPost("ForgotPassword")]
	public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest request)
	{
		var forgotPasswordResult = await mediatr.Send(mapper.Map<ForgotPasswordQuery>(request));

		return forgotPasswordResult.Match(
			forgotPasswordResult => Ok(forgotPasswordResult),
			errors => Problem(errors[0].ToString()));
	}

	[HttpGet("getAll")]
	//ToDo Make pagination
	public async Task<IActionResult> GetAllUserAsync()
	{
		var getAllUsersResult = await mediatr.Send(new GetAllUsersQuery());

		return getAllUsersResult.Match(
		   getAllUsersResult => Ok(mapper.Map<List<GetUserResponse>>(getAllUsersResult)),
		   errors => Problem(errors[0].ToString()));
	}

	 
	[HttpPost("updateProfile")]
	public async Task<IActionResult> UpdateUserProfileAsync(UpdateUserProfileRequest request)
	{
		string userId = User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value;

		var command = mapper.Map<UpdateUserProfileCommand>((request, userId));

		var updateUserProfileResult = await mediatr.Send(command);

		return updateUserProfileResult.Match(
			refreshTokenResult => Ok(updateUserProfileResult.Value),
			errors => Problem(errors[0].ToString()));
	}
 
	[HttpPost("deleteUser")]
	public async Task<IActionResult> DeleteUserAsync([FromBody] DeleteUserRequest request)
	{
		var command = mapper.Map<DeleteUserCommand>(request);

		var deleteUserResult = await mediatr.Send(command);

		return deleteUserResult.Match(
			refreshTokenResult => Ok(deleteUserResult),
			errors => Problem(errors[0].ToString()));
	}

	[HttpGet("getUser")]
	public async Task<IActionResult> GetUserAsync(string userId)
	{
		
		var command = new GetUserQuery(userId);

		var getUserResult = await mediatr.Send(command);

		return getUserResult.Match(
			refreshTokenResult => Ok(mapper.Map<GetUserResponse>(getUserResult.Value)),
			errors => Problem(errors[0].ToString()));
	}

	[HttpPost("editUser")]
	public async Task<IActionResult> EditUserAsync(EditUserRequest request)
	{

		//Avatar email confirm
		var command = mapper.Map<EditUserCommand>(request);

		var editUserResult = await mediatr.Send(command);

		return editUserResult.Match(
			refreshTokenResult => Ok(editUserResult),
			errors => Problem(errors[0].ToString()));
	}

	[HttpPost("addUser")]
	public async Task<IActionResult> AddUserAsync(CreateUserRequest model)
	{
		var command = mapper.Map<CreateUserCommand>(model);

		var addUserResult = await mediatr.Send(command);

		return addUserResult.Match(
			refreshTokenResult => Ok(addUserResult),
			errors => Problem(errors[0].ToString()));
	}

	[HttpPost("addAvatar")]
	public async Task<IActionResult> AddAvatarAsync([FromForm] AddAvatarRequest request)
	{
		string userId = User.Claims.First().Value;

		//ToDo Find mapping variant

		var command = new AddAvatarCommand(userId, request.File);

		var addAvatarResult = await mediatr.Send(command);

		return addAvatarResult.Match(
			refreshTokenResult => Ok(addAvatarResult),
			errors => Problem(errors[0].ToString()));
	}
}

