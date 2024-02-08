﻿using Microsoft.AspNetCore.Http;
using rivne.booking.Core.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace rivne.booking.Core.Helpers;
public static class ImageWorker
{
	public static async Task<ServiceResponse> SaveImageAsync(IFormFile image)
	{

		// Define the folder path to save the avatars
		var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "images", "apartments");

		// Create the folder if it doesn't exist
		if (!Directory.Exists(uploadFolderPath))
		{
			Directory.CreateDirectory(uploadFolderPath);
		}

		try
		{
			// Generate a unique filename for the image 
			var fileName = Path.GetRandomFileName();

			var webFileName = fileName + ".png";

			var filePath = Path.Combine(uploadFolderPath, webFileName);

			// Save the image file to the server
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await image.CopyToAsync(stream);
			}

			// Process the image if needed (e.g., resizing)
			using (var i = Image.Load(filePath))
			{
				// Resize the image to your desired dimensions
				i.Mutate(x => x.Resize(new ResizeOptions
				{
					Size = new Size(1200, 1200), 
					Mode = ResizeMode.Max
				}));

				//// Save the processed image back to the file
				//var webpEncoder = new WebpEncoder();
				//image.Save(filePath, webpEncoder);

				i.Save(filePath);
			}

			return new ServiceResponse
			{
				Success = true,
				PayLoad = webFileName,
				Message = "Image is save",
			};
		}
		catch (Exception ex)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = "Image is not saved",
			};
		}

	 }

	public static async Task<ServiceResponse> DeleteImageAsync(string imageName)
	{
		var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "images", "apartments");

		var delFilePath = Path.Combine(uploadFolderPath, imageName);

		try
		{
			File.Delete(delFilePath);

			return new ServiceResponse
			{
				Success = true,
				Message = "Image deleted",
			};
		}
		catch (Exception ex)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = ex.Message,
			};
		} 
	}


	//////////////////////
	///
	public static async Task SaveAvatarAsync(IFormFile file, string dirSaveImage)
	{
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

				using (var stream = System.IO.File.Create(dirSaveImage))
				{
					await image.SaveAsync(stream, new WebpEncoder());
				}
			}
		 
		}
	}

	 


}
