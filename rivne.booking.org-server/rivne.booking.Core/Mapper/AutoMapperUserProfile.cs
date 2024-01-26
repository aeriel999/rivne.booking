using AutoMapper;
using rivne.booking.Core.DTOs.Users;
using rivne.booking.Core.Entities.Users;


namespace rivne.booking.Core.Mapper;
public class AutoMapperUserProfile : Profile
{
    public AutoMapperUserProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<EditUserDto, User>().ReverseMap(); //
        CreateMap<UserDto, User>().ReverseMap();

    }
}