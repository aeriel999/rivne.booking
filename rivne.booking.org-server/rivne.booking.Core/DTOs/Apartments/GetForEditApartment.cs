 

namespace rivne.booking.Core.DTOs.Apartments;
public class GetForEditApartment
{
	public int Id { get; set; }
	public int NumberOfBuilding { get; set; }
	public bool IsPrivateHouse { get; set; }
	public int NumberOfRooms { get; set; }
	public int? Floor { get; set; }
	public double Area { get; set; }
	public decimal Price { get; set; }
	public string Description { get; set; } = String.Empty;
	public string TypeOfBooking { get; set; } = String.Empty;
	public bool IsBooked { get; set; } = false;
	public bool IsArchived { get; set; }
	public bool IsPosted { get; set; }
	public string StreetName { get; set; } = string.Empty;
	public string UserId { get; set; } = string.Empty;
	public string UserName { get; set; } = string.Empty;
	public List<string>? Images { get; set; }
}
