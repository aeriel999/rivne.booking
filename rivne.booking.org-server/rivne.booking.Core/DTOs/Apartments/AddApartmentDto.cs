using rivne.booking.Core.Entities.Apartments;
using rivne.booking.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rivne.booking.Core.DTOs.Apartments;
public class AddApartmentDto
{
	public int NumberOfBuilding { get; set; }
	public bool IsPrivateHouse { get; set; }
	public int NumberOfRooms { get; set; }
	public int? Floor { get; set; }
	public double Area { get; set; }
	public decimal Price { get; set; }
	public string Description { get; set; } = String.Empty;
	public string TypeOfBooking { get; set; } = String.Empty;
	public DateTime DateOfPost { get; set; } = DateTime.Now.ToUniversalTime();
	public bool IsBooked { get; set; } 
	public bool IsArchived { get; set; }
	public bool IsPosted { get; set; }
	public int StreetId { get; set; }
	public string UserId { get; set; } = string.Empty;

	
}
