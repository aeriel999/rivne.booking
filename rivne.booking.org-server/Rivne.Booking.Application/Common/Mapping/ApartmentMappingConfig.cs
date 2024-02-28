using Mapster;
using Microsoft.AspNetCore.Http;
using Rivne.Booking.Application.Apartaments.Create;
using Rivne.Booking.Application.Apartaments.Edit;
using Rivne.Booking.Domain.Apartments;

namespace Rivne.Booking.Application.Common.Mapping;
public class ApartmentMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateApartmentCommand, Apartment>()
			.Map(dest => dest.NumberOfBuilding, src => src.NumberOfBuilding)
			.Map(dest => dest.IsPrivateHouse, src => src.IsPrivateHouse)
			.Map(dest => dest.NumberOfRooms, src => src.NumberOfRooms)
			.Map(dest => dest.Floor, src => src.Floor)
			.Map(dest => dest.Area, src => src.Area)
			.Map(dest => dest.Price, src => src.Price)
			.Map(dest => dest.Description, src => src.Description)
			.Map(dest => dest.TypeOfBooking, src => src.TypeOfBooking)
			.Map(dest => dest.Images, src => src.Images)
			.Map(dest => dest.UserId, src => src.UserId);

		config.NewConfig<EditApartmentCommand, Apartment>()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.NumberOfBuilding, src => src.NumberOfBuilding)
			.Map(dest => dest.IsPrivateHouse, src => src.IsPrivateHouse)
			.Map(dest => dest.NumberOfRooms, src => src.NumberOfRooms)
			.Map(dest => dest.Floor, src => src.Floor)
			.Map(dest => dest.Area, src => src.Area)
			.Map(dest => dest.Price, src => src.Price)
			.Map(dest => dest.Description, src => src.Description)
			.Map(dest => dest.TypeOfBooking, src => src.TypeOfBooking)
			.Map(dest => dest.IsBooked, src => src.IsBooked)
			.Map(dest => dest.IsArchived, src => src.IsArchived)
			.Map(dest => dest.Images, src => src.Images);
	}
}
