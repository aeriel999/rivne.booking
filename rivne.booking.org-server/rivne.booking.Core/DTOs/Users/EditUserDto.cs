using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rivne.booking.Core.DTOs.Users;
public class EditUserDto
{
	public string Id { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public bool EmailConfirmed { get; set; }
	public string PhoneNumber { get; set; } = string.Empty;
	public bool PhoneNumberConfirmed { get; set; }
	public string Role { get; set; } = string.Empty;
	public bool LockoutEnabled { get; set; }
	public List<string> Roles { get; set;} = new List<string>();
}
