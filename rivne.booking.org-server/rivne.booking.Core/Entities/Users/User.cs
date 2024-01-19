using Microsoft.AspNetCore.Identity;
 

namespace rivne.booking.Core.Entities.Users;
public class User : IdentityUser
{
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
}
