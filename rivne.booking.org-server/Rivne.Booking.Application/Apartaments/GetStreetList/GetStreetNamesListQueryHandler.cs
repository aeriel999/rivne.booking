using ErrorOr;
using MediatR;
using Rivne.Booking.Application.Interfaces;

namespace Rivne.Booking.Application.Apartaments.GetStreetList;

public class GetStreetNamesListQueryHandler(IStreetRepository streetRepository) :
    IRequestHandler<GetStreetNamesListQuery, ErrorOr<List<string>>>
{
	public async Task<ErrorOr<List<string>>> Handle(GetStreetNamesListQuery request, 
		CancellationToken cancellationToken)
	{
		var errorOrStreetNamesList = await streetRepository.GetListOfStreetNamesAsync();

		return errorOrStreetNamesList;
	}
}
