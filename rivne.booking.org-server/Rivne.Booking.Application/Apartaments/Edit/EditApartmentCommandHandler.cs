using ErrorOr;
using MapsterMapper;
using MediatR;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Application.Users.Edit;
using Rivne.Booking.Domain.Apartments;

namespace Rivne.Booking.Application.Apartaments.Edit;

public class EditApartmentCommandHandler(IApartmentsRepository apartmentsRepository, IMapper mapper)
    : IRequestHandler<EditApartmentCommand, ErrorOr<Apartment>>
{
	public async Task<ErrorOr<Apartment>> Handle(EditApartmentCommand request, 
		CancellationToken cancellationToken)
	{
		var errorOrUpdate = await apartmentsRepository.EditApartmentAsync(
			mapper.Map<Apartment>(apartmentsRepository), request.StreetName, 
			request.Images, request.ImagesForDelete);

		return errorOrUpdate;
	}
}
