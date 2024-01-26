using rivne.booking.Core.Interfaces;


namespace rivne.booking.Core.Entities.Apartments;
public class Street : IEntity
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public ICollection<Apartment>? Apartments { get; set; }
}
