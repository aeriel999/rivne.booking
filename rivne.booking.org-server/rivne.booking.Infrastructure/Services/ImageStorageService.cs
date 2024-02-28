using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using rivne.booking.Infrastructure.Common.Identity;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Users;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace rivne.booking.Infrastructure.Services;

public class ImageStorageService(UserManager<AppUser> userManager) : IImageStorageService
{
	public async Task<ErrorOr<User>> AddAvatarAsync(User user, IFormFile file)
	{
		try
		{
			string imageName = Path.GetRandomFileName() + ".webp";

			var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "images", "avatars");

			if (!Directory.Exists(uploadFolderPath))
			{
				Directory.CreateDirectory(uploadFolderPath);
			}

			if (!user.Avatar.IsNullOrEmpty())
			{
				var delFilePath = Path.Combine(uploadFolderPath, user.Avatar!);

				if (File.Exists(delFilePath))
					File.Delete(delFilePath);
			}

			string dirSaveImage = Path.Combine(uploadFolderPath, imageName);

			using (MemoryStream ms = new MemoryStream())
			{
				await file.CopyToAsync(ms);

				using (var image = Image.Load(ms.ToArray()))
				{
					image.Mutate(x =>
					{
						x.Resize(new ResizeOptions
						{
							Size = new Size(1200, 1200),
							Mode = ResizeMode.Max
						});
					});

					using (var stream = File.Create(dirSaveImage))
					{
						await image.SaveAsync(stream, new WebpEncoder());
					}
				}
			}

			var appUser = await userManager.FindByIdAsync(user.Id);

			appUser!.Avatar = imageName;

			var avatarSaveResult = await userManager.UpdateAsync(appUser);

			if (!avatarSaveResult.Succeeded)
				return Error.Failure("Avatar is not upload");

			return user;
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<string>> SaveImageAsync(IFormFile image)
	{
		var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "images", "apartments");

		if (!Directory.Exists(uploadFolderPath))
			Directory.CreateDirectory(uploadFolderPath);

		try
		{
			var fileName = Path.GetRandomFileName();

			//ToDo MAke web format

			var webFileName = fileName + ".png";

			var filePath = Path.Combine(uploadFolderPath, webFileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
				await image.CopyToAsync(stream);

			using (var i = Image.Load(filePath))
			{
				i.Mutate(x => x.Resize(new ResizeOptions
				{
					Size = new Size(1200, 1200),
					Mode = ResizeMode.Max
				}));

				i.Save(filePath);
			}

			return webFileName;
		}
		catch (Exception ex)
		{
			return Error.Unexpected("Image is not saved ", ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteImageAsync(string imageName)
	{
		try
		{
			var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "images", "apartments");

			var delFilePath = Path.Combine(uploadFolderPath, imageName);

			File.Delete(delFilePath);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

}
