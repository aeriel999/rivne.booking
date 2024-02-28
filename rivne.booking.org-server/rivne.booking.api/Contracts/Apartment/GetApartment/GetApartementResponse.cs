namespace rivne.booking.api.Contracts.Apartment.GetApartment;

public record GetApartementResponse(
	int Id,
	int NumberOfBuilding,
	bool IsPrivateHouse,
	int NumberOfRooms,
	int? Floor,
	double Area,
	decimal Price,
	string Description,
	string TypeOfBooking,
	bool IsBooked,
	bool IsArchived,
	bool IsPosted,
	string StreetName,
	List<string>? Images);
 
