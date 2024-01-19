using rivne.booking.Core.Entities.Users;
using rivne.booking.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rivne.booking.Core.Entities;
public class RefreshToken : IEntity
{
	public int Id { get; set; }
	public string Token { get; set; } = string.Empty;
	public string UserId { get; set; } = string.Empty;
	public string JwtId { get; set; } = string.Empty;
	public bool IsUsed { get; set; }
	public bool IsRevoked { get; set; }
	public DateTime AddedDate { get; set; }
	public DateTime ExpireDate { get; set; }

	[ForeignKey(nameof(UserId))]
	public User User { get; set; }
}
