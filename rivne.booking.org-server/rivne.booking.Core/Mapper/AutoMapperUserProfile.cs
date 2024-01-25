using AutoMapper;
using rivne.booking.Core.DTOs.Users;
using rivne.booking.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rivne.booking.Core.Mapper;
public class AutoMapperUserProfile : Profile
{
    public AutoMapperUserProfile()
    {
		CreateMap<UpdateProfileDto, User>().ReverseMap();
		CreateMap<EditUserDto, User>().ReverseMap();
		CreateMap<AddUserDto, User>().ReverseMap();

	}
}
