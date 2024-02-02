using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rivne.booking.Core.Entities.Apartments;
using rivne.booking.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rivne.booking.Core.DTOs.Apartments;
public class EditApartment
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
	public int StreetId { get; set; }
	public string StreetName { get; set; } = String.Empty;

	[BindProperty(Name = "images[]")]
	public ICollection<IFormFile>? Images  { get; set; }

	public List<string>? ImagesForDelete { get; set;}
	
}
