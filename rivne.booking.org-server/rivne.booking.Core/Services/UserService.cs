﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using rivne.booking.Core.DTOs.Users;
using rivne.booking.Core.Entities;
using rivne.booking.Core.Entities.Users;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Webp;
using Microsoft.IdentityModel.Tokens;


namespace rivne.booking.Core.Services;
public class UserService
{
	private readonly UserManager<User> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly SignInManager<User> _signInManager;
	private readonly IConfiguration _config;
	private readonly JwtServise _jwtService;
	private readonly IMapper _mapper;

	public UserService( RoleManager<IdentityRole> roleManager, IConfiguration config,
		UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, JwtServise jwtService)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_config = config;
		_roleManager = roleManager;
		_mapper = mapper;
		_jwtService = jwtService;
	}

	public async Task<ServiceResponse> LoginUserAsync(LoginUserDto model)
	{
		var user = await _userManager.FindByEmailAsync(model.Email);

		if (user == null)
		{
			return new ServiceResponse
			{
				Message = "Login or password incorrect.",
				Success = false
			};
		}

		var signinResult = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: true, lockoutOnFailure: true);

		if (signinResult.Succeeded)
		{
			var tokens = await _jwtService.GenerateJwtTokensAsync(user);
			return new ServiceResponse
			{
				AccessToken = tokens.Token,
				RefreshToken = tokens.RefreshToken.Token,
				Message = "User signed-in successfully.",
				Success = true
			};
		}

		if (signinResult.IsNotAllowed)
		{
			return new ServiceResponse
			{
				Message = "Confirm your email please.",
				Success = false
			};
		}

		if (signinResult.IsLockedOut)
		{
			return new ServiceResponse
			{
				Message = "User is blocked connect to support.",
				Success = false
			};
		}

		return new ServiceResponse
		{
			Message = "Login or password incorrect.",
			Success = false
		};
	}

	public async Task<ServiceResponse> LogoutUserAsync(string userId)
	{
		try
		{
			IEnumerable<RefreshToken> tokens = await _jwtService.GetTokensByUserId(userId);

			foreach (RefreshToken token in tokens)
			{
				await _jwtService.Delete(token);
			}

			await _signInManager.SignOutAsync();

			return new ServiceResponse
			{
				Message = "User Logged out",
				Success = true
			};

		}
		catch (Exception ex)
		{
			return new ServiceResponse
			{
				Message = ex.Message,
				Success = false
			};
		}
	}

	public async Task<ServiceResponse> UpdateProfileAsync(UpdateProfileDto model)
	{
		var user = await _userManager.FindByIdAsync(model.Id);

		if (user == null)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User is not found",
			};
		}

		if (user.FirstName != model.FirstName)
		{
			user.FirstName = model.FirstName;
		}

		if (user.LastName != model.LastName)
		{
			user.LastName = model.LastName;
		}

		if (user.Email != model.Email)
		{
			user.Email = model.Email;
			user.EmailConfirmed = false;
		}

		if (user.PhoneNumber != model.PhoneNumber)
		{
			user.PhoneNumber = model.PhoneNumber;
			user.PhoneNumberConfirmed = false;
		}

		var result = await _userManager.UpdateAsync(user);

		if (result.Succeeded)
		{
			return new ServiceResponse
			{
				Success = true,
				//PayLoad = user,
				Message = "User is update"
			};
		}
		else
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User is not update",
			};
		}
	}

	public async Task<ServiceResponse> GetAllUsersAsync()
	{
		var users = await _userManager.Users.ToListAsync();

		if (users == null)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User aren`t loaded",
			};
		}

		var mappedUsers = new List<UserDto>();

		foreach (var user in users)
		{
			var newUser = _mapper.Map<User, UserDto>(user);
			newUser.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
			mappedUsers.Add(newUser);
		}

		if (mappedUsers != null)
		{
			return new ServiceResponse
			{
				Success = true,
				Message = "Users are loaded",
				PayLoad = mappedUsers
			};
		}
		else
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User aren`t loaded",
			};
		}
	}

	public async Task<ServiceResponse> RegisterUserAsync(RegisterUserDto model)
	{
		var user = new User
		{
			Email = model.Email,
			UserName = model.Email,
		}; 

		var result = await _userManager.CreateAsync(user, model.Password);

		if (result.Succeeded)
		{
			var roleResult = _userManager.AddToRoleAsync(user, "User").Result;

			if (roleResult.Succeeded)
			{
				return new ServiceResponse
				{
					Success = true,
					Message = "User is created"
				};
			}
			else
			{
				return new ServiceResponse
				{
					Success = false,
					Message = "User is not created",
				};
			}
		}
		else
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User is not created",
			};
		}
	}

	public async Task<ServiceResponse> DeleteUserAsync(string id)
	{
		var user = await _userManager.FindByIdAsync(id);

		if (user == null)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User is not found",
			};
		}

		if (!user.Avatar.IsNullOrEmpty())
		{
			var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "images", "avatars");

			var delFilePath = Path.Combine(uploadFolderPath, user.Avatar);

			if (File.Exists(delFilePath)) { File.Delete(delFilePath); }
		}

		var result = await _userManager.DeleteAsync(user);

		if (result.Succeeded)
		{
			return new ServiceResponse
			{
				Success = true,
				Message = "User is deleted"
			};
		}
		else
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User is not delete",
			};
		}
	}

	public async Task<ServiceResponse> GetUserAsync(string userID)
	{
		var user = await _userManager.FindByIdAsync(userID);

		if (user == null)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User is not found",
			};
		}
		else
		{
			var mappedUser = _mapper.Map<User, EditUserDto>(user);

			mappedUser.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()!;

			var roles = _roleManager.Roles.ToList();

			foreach (var role in roles)
			{
				mappedUser.Roles.Add(role.Name!);
			}

			return new ServiceResponse
			{
				Success = true,
				Message = "User is loaded",
				PayLoad = mappedUser
			};
		}
	}

	public async Task<ServiceResponse> EditUserAsync(EditUserDto model)
	{
		var user = await _userManager.FindByIdAsync(model.Id);

		if (user == null)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User is not found",
			};
		}

		if (user.FirstName != model.FirstName)
		{
			user.FirstName = model.FirstName;
		}

		if (user.LastName != model.LastName)
		{
			user.LastName = model.LastName;
		}

		if (user.Email != model.Email)
		{
			user.Email = model.Email;
			user.EmailConfirmed = false;
		}

		if (user.PhoneNumber != model.PhoneNumber)
		{
			user.PhoneNumber = model.PhoneNumber;
			user.PhoneNumberConfirmed = false;
		}

		if (user.LockoutEnabled != model.LockoutEnabled)
		{
			user.LockoutEnabled = model.LockoutEnabled;
		}

		var role = await _userManager.GetRolesAsync(user);

		if (role == null)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "Error in finding of role of user"
			};
		}

		if (role[0] != model.Role)
		{
			await _userManager.RemoveFromRoleAsync(user, role[0]);

			await _userManager.AddToRoleAsync(user, model.Role);
		}

		var result = await _userManager.UpdateAsync(user);

		if (result.Succeeded)
		{
			return new ServiceResponse
			{
				Success = true,
				Message = "User is update"
			};
		}
		else
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User is not update",
			};
		}
	}

	public async Task<ServiceResponse> CreateUserAsync(AddUserDto model)
	{
		var mappedUser = _mapper.Map<AddUserDto, User>(model);

		mappedUser.UserName = model.Email;

		var result = await _userManager.CreateAsync(mappedUser, model.Password);
		 
		if (result.Succeeded)
		{
			_userManager.AddToRoleAsync(mappedUser, model.Role).Wait();

			return new ServiceResponse
			{
				Success = true,
				Message = "New user created successfully"
			};
		}
		else
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "Error in creating"
			};
		}
	}

	public async Task<ServiceResponse> AddAvatarAsync(AddAvatarDto model)
	{
		var user = await _userManager.FindByIdAsync(model.Id);

		if (user == null)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "User is not found",
			};
		}

		// Check if the file is null
		if (model.Image == null || model.Image.Length == 0)
		{
			return new ServiceResponse { Success = false, Message = "Error in avatar loading" };
		}

		// Define the folder path to save the avatars
		var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "images", "avatars");

		// Create the folder if it doesn't exist
		if (!Directory.Exists(uploadFolderPath))
		{
			Directory.CreateDirectory(uploadFolderPath);
		}

		if (!user.Avatar.IsNullOrEmpty())
		{
			var delFilePath = Path.Combine(uploadFolderPath, user.Avatar);

			if (File.Exists(delFilePath)) { File.Delete(delFilePath); }
		}

		try
		{
			// Generate a unique filename for the avatar 
			var fileName = Path.GetRandomFileName();
			var webFileName = fileName + ".png";  

			var filePath = Path.Combine(uploadFolderPath, webFileName);

			// Save the avatar file to the server
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await model.Image.CopyToAsync(stream);
			}

			// Process the image if needed (e.g., resizing)
			using (var image = Image.Load(filePath))
			{
				// Resize the image to your desired dimensions
				image.Mutate(x => x.Resize(new ResizeOptions
				{
					Size = new Size(150, 150), // Adjust the dimensions as needed
					Mode = ResizeMode.Max
				}));

				//// Save the processed image back to the file
				//var webpEncoder = new WebpEncoder();
				//image.Save(filePath, webpEncoder);

				image.Save(filePath);
			}

			user.Avatar = webFileName;

			var result = await _userManager.UpdateAsync(user);

			if (result.Succeeded)
			{
				return new ServiceResponse { Success = true, Message = "Avatar added successfully." };
			}
			else
			{
				return new ServiceResponse
				{
					Success = false,
					Message = "Avater is not upload",
				};
			}
		}
		catch (Exception ex)
		{
			return new ServiceResponse { Success = false, Message = $"Error adding avatar: {ex.Message}" };
		}
	}

}
