using Mapster;
using rivne.booking.api.Contracts.Apartment;
using rivne.booking.api.Contracts.Apartment.GetApartment;
using Rivne.Booking.Application.Apartaments.Create;
using Rivne.Booking.Application.Apartaments.Delete;
using Rivne.Booking.Application.Apartaments.Edit;
using Rivne.Booking.Application.Apartaments.GetApartment;
using Rivne.Booking.Domain.Apartments;

namespace rivne.booking.api.Common.Mapping;

public class ApartmentMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<(CreateApartmentRequest createApartmentRequest, string userId),
			CreateApartmentCommand>()
			.Map(dest => dest.UserId, src => src.userId)
			.Map(dest => dest, src => src.createApartmentRequest);

		config.NewConfig<Apartment, ApartmentInfo>()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.UserName, src => src.UserName)
			.Map(dest => dest.DateOfPost, src => src.DateOfPost)
			.Map(dest => dest.DateOfUpdate, src => src.DateOfUpdate)
			.Map(dest => dest.IsPosted, src => src.IsPosted)
			.Map(dest => dest.IsBooked, src => src.IsBooked)
			.Map(dest => dest.Address, src => src.Street.Name + " " + src.NumberOfBuilding)
			.Map(dest => dest.NumberOfRooms, src => src.NumberOfRooms)
			.Map(dest => dest.TypeOfBooking, src => src.TypeOfBooking)
			.Map(dest => dest.Floor, src => src.Floor)
			.Map(dest => dest.Area, src => src.Area)
			.Map(dest => dest.Price, src => src.Price)
			.Map(dest => dest.Image, src => src.Images.FirstOrDefault());

		config.NewConfig<List<Apartment>, List<ApartmentInfo>>();

		config.NewConfig<Apartment, GetApartementResponse>();

		config.NewConfig<DeleteApartamentRequest, DeleteApartmentCommand>();

		config.NewConfig<GetApartmentRequest, GetApatrmentQuery>();
 
		config.NewConfig<Apartment, GetApartementResponse>()
		.Map(dest => dest.Id, src => src.Id)
		.Map(dest => dest.NumberOfBuilding, src => src.NumberOfBuilding)
		.Map(dest => dest.IsPrivateHouse, src => src.IsPrivateHouse)
		.Map(dest => dest.Description, src => src.Description)
		.Map(dest => dest.IsPosted, src => src.IsPosted)
		.Map(dest => dest.IsBooked, src => src.IsBooked)
		.Map(dest => dest.IsArchived, src => src.IsArchived)
		.Map(dest => dest.NumberOfRooms, src => src.NumberOfRooms)
		.Map(dest => dest.TypeOfBooking, src => src.TypeOfBooking)
		.Map(dest => dest.Floor, src => src.Floor)
		.Map(dest => dest.Area, src => src.Area)
		.Map(dest => dest.StreetName, src => src.Street.Name)
		.Map(dest => dest.Price, src => src.Price)
		.Map(dest => dest.Images, src => src.Images);

		config.NewConfig<EditApartmentRequest, EditApartmentCommand>();
	}
}
