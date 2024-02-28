namespace rivne.booking.api.Contracts.Apartment;

public record EditApartmentRequest(
	int Id,
	int NumberOfBuilding,
	bool IsPrivateHouse,
	int NumberOfRooms,
	int? Floor,
	double Area,
	decimal Price,
	string TypeOfBooking,
	string Description,
	bool IsBooked,
	bool IsArchived,
	string StreetName,
	List<string>? ImagesForDelete,
	List<IFormFile>? Images);
 