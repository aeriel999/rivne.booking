using Rivne.Booking.Domain.Apartments;

namespace Rivne.Booking.Domain.Users;

public class User  
{
	public required string Id { get; set; }	

	public required string Email { get; set; }

	public bool IsEmailConfirmed { get; set; }

	public string? FirstName { get; set; }

	public string? LastName { get; set; }

	public string? PhoneNumber { get; set; }

	public bool PhoneNumberConfirmed { get; set; }

	public required bool LockoutEnabled { get; set; }

	public string? Avatar { get; set; }

	public List<Apartment> Apartments { get; set; } = [];

	public required string Role { get; set; }
}
