using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
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
	public int StreetId { get; set; }
	public string StreetName { get; set; } = String.Empty;

	[BindProperty(Name = "images[]")]
	public List<IFormFile>? Images { get; set; }
}

//public enum TypeOfBooking { ForHour, ForDay, ForMonth };