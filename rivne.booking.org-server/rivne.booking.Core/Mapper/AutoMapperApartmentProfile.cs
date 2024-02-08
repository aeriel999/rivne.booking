using AutoMapper;
using rivne.booking.Core.DTOs.Apartments;
using rivne.booking.Core.Entities.Apartments;

namespace rivne.booking.Core.Mapper;
public class AutoMapperApartmentProfile : Profile
{
    public AutoMapperApartmentProfile()
    {
		CreateMap<AddApartmentDto, Apartment>()
				.ForMember(x => x.Images, opt => opt.Ignore());

		//CreateMap<Apartment, EditApartment>()
		//	.ForMember(x => x.Images, opt => opt.Ignore());
		 

		CreateMap<Apartment, GetForEditApartment>()
				//.ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.FirstName + " " + x.User.LastName))
				.ForMember(x => x.StreetName, opt => opt.MapFrom(x => x.Street.Name))
				.ForMember(x => x.Images, opt => opt.MapFrom(x => x.Images.Select(y => y.Name)));
				//.ForMember(x => x.Images, opt => opt.Ignore());

		CreateMap<Apartment, ListApartmentDto>()
				//.ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.FirstName + " " + x.User.LastName))
				.ForMember(x => x.Address, opt => opt.MapFrom(x => x.Street.Name + " street, " + x.NumberOfBuilding))
				.ForMember(x => x.Image, opt => opt.MapFrom(x => x.Images.FirstOrDefault().Name))
				.ForMember(x => x.DateOfPost, opt => opt.MapFrom(x => x.DateOfPost.ToShortDateString()))
				.ForMember(x => x.DateOfUpdate, opt => opt.MapFrom(x => x.DateOfUpdate.GetValueOrDefault().ToShortDateString()));

	}
}
