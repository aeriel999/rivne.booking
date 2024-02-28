using ErrorOr;
using MapsterMapper;
using MediatR;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Apartments;

namespace Rivne.Booking.Application.Apartaments.GetAllApartments;

public class GetAllApartmentsQueryHandler(IApartmentsRepository apartmentsRepository) :
    IRequestHandler<GetAllApartmentsQuery, ErrorOr<List<Apartment>>>
{
    public async Task<ErrorOr<List<Apartment>>> Handle(GetAllApartmentsQuery request,
        CancellationToken cancellationToken)
    {
        var errorOrListOfApartments = await apartmentsRepository.GetAllApattmentsAsync();

        return errorOrListOfApartments;
    }
}
