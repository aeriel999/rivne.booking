using Microsoft.AspNetCore.Identity;
using rivne.booking.Core.Entities.Apartments;


namespace rivne.booking.Core.Entities.Users;
public class User : IdentityUser
{
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Avatar { get; set; } = string.Empty;
	public ICollection<Apartment>? Apartments { get; set; }
}
