using rivne.booking.Core.Entities.Users;
using rivne.booking.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rivne.booking.Core.Entities.Apartments;
public class Image : IEntity
{
	public int Id { get; set; }
	public string Name { get; set; }
	public int ApartmentId { get; set; }

	[ForeignKey(nameof(ApartmentId))]
	public Apartment Apartment { get; set; }

	 
}
