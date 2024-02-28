namespace Rivne.Booking.Domain.Apartments;
public class Street
{ 
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public ICollection<Apartment>? Apartments { get; set; }
}
