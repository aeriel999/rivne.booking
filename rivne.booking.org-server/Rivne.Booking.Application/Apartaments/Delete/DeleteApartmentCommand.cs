using ErrorOr;
using MediatR;
using Rivne.Booking.Domain.Apartments;

namespace Rivne.Booking.Application.Apartaments.Delete;
public record DeleteApartmentCommand(int Id) : IRequest<ErrorOr<Apartment>>;
 
