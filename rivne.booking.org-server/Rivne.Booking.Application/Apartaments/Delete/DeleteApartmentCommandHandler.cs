using ErrorOr;
using MediatR;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Apartments;

namespace Rivne.Booking.Application.Apartaments.Delete;
public class DeleteApartmentCommandHandler(IApartmentsRepository apartmentsRepository) :
    IRequestHandler<DeleteApartmentCommand, ErrorOr<Apartment>>
{
	public async Task<ErrorOr<Apartment>> Handle(DeleteApartmentCommand request, 
		CancellationToken cancellationToken)
	{
		var errorOrDelete = await apartmentsRepository.DeleteApartmentAsync(request.Id);

		return errorOrDelete;
	}
}
