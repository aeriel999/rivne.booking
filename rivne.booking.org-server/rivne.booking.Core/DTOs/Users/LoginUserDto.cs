using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rivne.booking.Core.DTOs.Users;
public class LoginUserDto
{
	public string Email { get; set; } = String.Empty;
	public string Password { get; set; } = String.Empty;
}
