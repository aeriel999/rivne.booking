using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rivne.booking.Core.DTOs.Users;
public class UserDto
{
	public string Id { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public bool EmailConfirmed { get; set; }
	public string Email { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public bool PhoneNumberConfirmed { get; set; }
	public string LockedEnd { get; set; } = string.Empty;
	public string Role { get; set; } = string.Empty;
	public string Avatar { get; set; } = string.Empty;
}
