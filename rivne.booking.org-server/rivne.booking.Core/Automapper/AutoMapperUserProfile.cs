using AutoMapper;
using rivne.booking.Core.DTOs.Users;
using rivne.booking.Core.Entities.Users;


namespace rivne.booking.Core.Automapper;
public class AutoMapperUserProfile : Profile
{
	public AutoMapperUserProfile()
	{
		CreateMap<UserDto, User>().ReverseMap();
		//CreateMap<EditUserDto, ApiUser>().ReverseMap(); //
		//CreateMap<UserDto, ApiUser>().ReverseMap();

	}
}