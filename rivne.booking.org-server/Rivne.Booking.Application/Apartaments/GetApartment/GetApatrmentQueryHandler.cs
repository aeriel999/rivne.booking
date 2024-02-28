using ErrorOr;
using MapsterMapper;
using MediatR;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Apartments;

namespace Rivne.Booking.Application.Apartaments.GetApartment;

public class GetApatrmentQueryHandler(IApartmentsRepository apartmentsRepository, IMapper _mapper) :
    IRequestHandler<GetApatrmentQuery, ErrorOr<Apartment>>
{
	public async Task<ErrorOr<Apartment>> Handle(GetApatrmentQuery request, CancellationToken cancellationToken)
	{
		var errorOrList = await apartmentsRepository.GetApartmentByIdAsync(request.Id);

		return errorOrList;
	}
}
