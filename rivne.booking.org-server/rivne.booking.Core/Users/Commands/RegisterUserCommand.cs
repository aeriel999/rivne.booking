using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rivne.booking.Core.Users.Commands;
public class RegisterUserCommand
{
	public required string Email { get; set; }
	public required string Password { get; set; }
	public required string ConfirmPassword { get; set; }
	public required string BaseUrl { get; set; } 

}
