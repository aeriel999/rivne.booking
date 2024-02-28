using Mapster;
using Rivne.Booking.Application.Users.Create;
using Rivne.Booking.Application.Users.Edit;
using Rivne.Booking.Application.Users.Update;
using Rivne.Booking.Domain.Users;

namespace Rivne.Booking.Application.Common.Mapping;

public class UserMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<UpdateUserProfileCommand, User>()
			.Map(dest => dest.Id, src => src.UserId)
			.Map(dest => dest.FirstName, src => src.FirstName)
			.Map(dest => dest.LastName, src => src.LastName)
			.Map(dest => dest.Email, src => src.Email)
			.Map(dest => dest.PhoneNumber, src => src.PhoneNumber);

		config.NewConfig<EditUserCommand, User>()
			.Ignore(dest => dest.Avatar);

		config.NewConfig<CreateUserCommand, User>()
			.Map(dest => dest.FirstName, src => src.FirstName)
			.Map(dest => dest.LastName, src => src.LastName)
			.Map(dest => dest.Email, src => src.Email)
			.Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
			.Map(dest => dest.Role, src => src.Role);
	}
}
