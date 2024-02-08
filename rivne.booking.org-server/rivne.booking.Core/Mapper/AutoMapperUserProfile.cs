using AutoMapper;
using rivne.booking.Core.DTOs.Users;
using Rivne.Booking.Domain.Users;


namespace rivne.booking.Core.Mapper;
public class AutoMapperUserProfile : Profile
{
    public AutoMapperUserProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<EditUserDto, User>().ReverseMap(); //
        CreateMap<UserDto, User>().ReverseMap();
		CreateMap<AddUserDto, User>().ReverseMap();
	}
}