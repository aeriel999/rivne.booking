using System.ComponentModel.DataAnnotations.Schema;

namespace Rivne.Booking.Domain.Apartments;

public class Image
{
	public int Id { get; set; }
	public required string Name { get; set; }
	public int ApartmentId { get; set; }

	//ToDo ??? required does not fit
	[ForeignKey(nameof(ApartmentId))]
	public Apartment Apartment { get; set; }
}
