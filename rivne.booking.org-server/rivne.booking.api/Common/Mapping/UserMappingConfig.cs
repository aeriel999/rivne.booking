using Mapster;
using rivne.booking.api.Contracts.User;
using rivne.booking.api.Contracts.User.GetUser;
using Rivne.Booking.Application.Users.Create;
using Rivne.Booking.Application.Users.Delete;
using Rivne.Booking.Application.Users.Edit;
using Rivne.Booking.Application.Users.SetAvatar;
using Rivne.Booking.Application.Users.Update;
using Rivne.Booking.Domain.Users;

namespace rivne.booking.api.Common.Mapping;

public class UserMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<User, GetUserResponse>();

		config.NewConfig<List<User>, List<GetUserResponse>>();

		config.NewConfig<(UpdateUserProfileRequest updateUserProfileRequest, string userId),
			UpdateUserProfileCommand>()
			.Map(dest => dest.UserId, src => src.userId)
			.Map(dest => dest, src => src.updateUserProfileRequest);

		config.NewConfig<EditUserRequest, EditUserCommand>();

		config.NewConfig<CreateUserRequest, CreateUserCommand>();

		config.NewConfig<DeleteUserRequest, DeleteUserCommand>();

		config.NewConfig<(AddAvatarRequest addAvatarRequest, string userId),
			AddAvatarCommand>()
			.Map(dest => dest.UserId, src => src.userId)
			.Map(dest => dest, src => src.addAvatarRequest);

	}
}