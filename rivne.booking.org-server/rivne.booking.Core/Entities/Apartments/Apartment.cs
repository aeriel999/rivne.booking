 
using rivne.booking.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace rivne.booking.Core.Entities.Apartments;

public class Apartment : IEntity 
{
	public int Id { get; set; }
	public int NumberOfBuilding { get; set; }
	public bool IsPrivateHouse { get; set; }
	public int NumberOfRooms { get; set; }
	public int? Floor { get; set; }
	public double Area { get; set; }
	public decimal Price { get; set; }
	public string Description { get; set; }	= String.Empty;
	public string TypeOfBooking { get; set; } = String.Empty;
	public DateTime DateOfPost { get; set; }
	public DateTime? DateOfUpdate { get; set; }
	public bool IsBooked { get; set; } = false;
	public bool IsArchived { get; set; }
	public bool IsPosted { get; set; }
	public int StreetId { get; set; }
	public string UserId { get; set; } = string.Empty;

	[ForeignKey(nameof(StreetId))]
	public Street Street { get; set; }
	
	//[ForeignKey(nameof(UserId))]
	//public User User { get; set; }
	public ICollection<Image>? Images { get; set; }
}

