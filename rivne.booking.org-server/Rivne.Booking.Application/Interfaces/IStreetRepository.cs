using ErrorOr;
using Rivne.Booking.Domain.Apartments;

namespace Rivne.Booking.Application.Interfaces;

public interface IStreetRepository
{
	Task<ErrorOr<Street>> GetOrCreateAndGetStreetByStreetNameAsync(string streetName);
	Task<ErrorOr<List<string>>> GetListOfStreetNamesAsync();
}
 