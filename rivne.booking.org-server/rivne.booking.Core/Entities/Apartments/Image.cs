using rivne.booking.Core.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace rivne.booking.Core.Entities.Apartments;
public class Image : IEntity
{
	public int Id { get; set; }
	public string Name { get; set; }
	public int ApartmentId { get; set; }

	[ForeignKey(nameof(ApartmentId))]
	public Apartment Apartment { get; set; }

	 
}
