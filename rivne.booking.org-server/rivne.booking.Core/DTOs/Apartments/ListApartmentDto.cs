using rivne.booking.Core.Entities.Apartments;
using rivne.booking.Core.Entities.Users;
using rivne.booking.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rivne.booking.Core.DTOs.Apartments;
public class ListApartmentDto 
{
	public int Id { get; set; }
	public string UserName { get; set; } = String.Empty;
	public string DateOfPost { get; set; }
	public string? DateOfUpdate { get; set; }
	public bool IsPosted { get; set; }
	public bool IsBooked { get; set; }
	public string Address { get; set; } = String.Empty;
	public int NumberOfRooms { get; set; }
	public string TypeOfBooking { get; set; } = String.Empty;
	public int? Floor { get; set; }
	public double Area { get; set; }
	public decimal Price { get; set; }
	public string Image { get; set; } = String.Empty;
}
