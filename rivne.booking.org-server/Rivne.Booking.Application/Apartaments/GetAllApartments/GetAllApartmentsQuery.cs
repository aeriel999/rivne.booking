using ErrorOr;
using MediatR;
using Rivne.Booking.Domain.Apartments;

namespace Rivne.Booking.Application.Apartaments.GetAllApartments;

public class GetAllApartmentsQuery() : IRequest<ErrorOr<List<Apartment>>>;
