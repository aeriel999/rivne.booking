using Microsoft.AspNetCore.Http;


namespace rivne.booking.Core.DTOs.Users;
public  class AddAvatarDto
{
	public string Id { get; set; } = string.Empty;

	public IFormFile? Image { get; set; }
}
