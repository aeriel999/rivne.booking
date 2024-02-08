using Rivne.Booking.Domain.Users;

namespace Rivne.Booking.Application.Interfaces;

public interface IJwtTokenService
{
	Task Create(RefreshToken token);
	Task Delete(RefreshToken token);
	Task Update(RefreshToken token);
	Task<RefreshToken?> Get(string token);
	Task<IEnumerable<RefreshToken>> GetAll();
}
