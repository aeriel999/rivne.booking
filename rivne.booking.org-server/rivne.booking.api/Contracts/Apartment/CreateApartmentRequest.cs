namespace rivne.booking.api.Contracts.Apartment;

public record CreateApartmentRequest(
	int NumberOfBuilding,
	bool IsPrivateHouse,
	int NumberOfRooms,
	int? Floor,
	double Area,
	decimal Price,
	string Description,
	string TypeOfBooking,
	string StreetName,
	List<IFormFile>? Images);
//ToDo How to do a binding of image array
//[BindProperty(Name = "images[]")]