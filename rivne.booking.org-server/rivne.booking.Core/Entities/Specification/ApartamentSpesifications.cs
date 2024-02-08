using Ardalis.Specification;
using rivne.booking.Core.Entities.Apartments;


namespace rivne.booking.Core.Entities.Specification;
public static class Apartaments 
{
	//public class GetAllApartmentsWithDetails : Specification<Apartment>
	//{ 
	//	public GetAllApartmentsWithDetails() 
	//	{
	//		Query.Include(a => a.Street)
	//		.Include(a => a.User)
	//		.Include(a => a.Images);
	//	}
	//}

	//public class GetApartmentWithDetails : Specification<Apartment>
	//{
	//	public GetApartmentWithDetails(int id)
	//	{
	//		Query
	//			.Where(a => a.Id == id)
	//			.Include(a => a.Street)
	//		.Include(a => a.User)
	//		.Include(a => a.Images);
	//	}
	//}

	public class GetImageByName : Specification<Image>
	{
		public GetImageByName(string name)
		{
			Query
				.Where(a => a.Name == name);
		}
	}
}
