using ErrorOr;
using MediatR;

namespace Rivne.Booking.Application.Apartaments.GetStreetList;

public record GetStreetNamesListQuery() : IRequest<ErrorOr<List<string>>>;
 