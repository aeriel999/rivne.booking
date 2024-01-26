using AutoMapper;
using rivne.booking.Core.DTOs.Apartments;
using rivne.booking.Core.DTOs.Users;
using rivne.booking.Core.Entities.Apartments;
using rivne.booking.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rivne.booking.Core.Mapper;
public class AutoMapperApartmentProfile : Profile
{
    public AutoMapperApartmentProfile()
    {
        CreateMap<AddApartmentDto, Apartment>().ReverseMap();
    }
}
