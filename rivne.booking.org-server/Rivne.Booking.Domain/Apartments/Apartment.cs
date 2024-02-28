namespace Rivne.Booking.Domain.Apartments;

public class Apartment
{
	public int Id { get; set; }
	public int NumberOfBuilding { get; set; }
	public bool IsPrivateHouse { get; set; }
	public int NumberOfRooms { get; set; }
	public int? Floor { get; set; }
	public double Area { get; set; }
	public decimal Price { get; set; }
	public required string Description { get; set; } 
	public required string TypeOfBooking { get; set; } 
	public DateTime DateOfPost { get; set; }
	public DateTime? DateOfUpdate { get; set; }
	public bool IsBooked { get; set; } = false;
	public bool IsArchived { get; set; }
	public bool IsPosted { get; set; }
	public int StreetId { get; set; }
	public required string UserId { get; set; }
	//ToDo Delete
	public required string UserName { get; set; }

	//ToDo ???  If Street make required i need to set it in new Apartments, but nulable it cant be
	public required Street Street { get; set; }
	public ICollection<Image> Images { get; set; } = [];
}

