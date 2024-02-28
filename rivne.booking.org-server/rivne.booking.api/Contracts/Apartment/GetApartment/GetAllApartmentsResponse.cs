namespace rivne.booking.api.Contracts.Apartment.GetApartment;

public record GetAllApartmentsResponse(List<ApartmentInfo>? ListOfApartments);
 
public class ApartmentInfo
{
	public int Id { get; set; }
	public string? UserName { get; set; }
	public required string DateOfPost { get; set; }
	public string? DateOfUpdate { get; set; }
	public bool IsPosted { get; set; }
	public bool IsBooked { get; set; }
	public required string Address { get; set; }
	public int NumberOfRooms { get; set; }
	public required string TypeOfBooking { get; set; }
	public int? Floor { get; set; }
	public double Area { get; set; }
	public decimal Price { get; set; }
	public string? Image { get; set; }
}
