using Microsoft.Extensions.FileProviders;

namespace rivne.booking.api;

public static class RequestPipelineExtensions
{
	public static void UseCustomStaticFiles(this WebApplication application)
	{
		var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");

		if (!Directory.Exists(dir))
		{
			Directory.CreateDirectory(dir);
		}

		application.UseStaticFiles(new StaticFileOptions
		{
			FileProvider = new PhysicalFileProvider(dir),
			RequestPath = "/images"
		});
	}
}
