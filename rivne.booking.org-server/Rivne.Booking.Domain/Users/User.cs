using Microsoft.AspNetCore.Identity;
//using rivne.booking.Core.Entities.Apartments;


namespace Rivne.Booking.Domain.Users;
public class User : IdentityUser
{
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Avatar { get; set; }

	//public ICollection<Apartment>? Apartments { get; set; }
}
