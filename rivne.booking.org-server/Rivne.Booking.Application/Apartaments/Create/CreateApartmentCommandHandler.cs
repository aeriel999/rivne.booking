using ErrorOr;
using MapsterMapper;
using MediatR;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Apartments;

namespace Rivne.Booking.Application.Apartaments.Create;

public class CreateApartmentCommandHandler(
    IApartmentsRepository _apartmentsRepository, IMapper _mapper) :
    IRequestHandler<CreateApartmentCommand, ErrorOr<Apartment>>
{
    public async Task<ErrorOr<Apartment>> Handle(CreateApartmentCommand request, CancellationToken cancellationToken)
    {
        var errorOrCreated = await _apartmentsRepository.AddApartmentAsync(
            _mapper.Map<Apartment>(request), request.StreetName, request.Images);

        return errorOrCreated;
    }
}
