using Rivne.Booking.Domain.Users;

namespace Rivne.Booking.Application.Interfaces;

public interface IRefreshTokenRepository
{
	Task InsertAsync(RefreshToken entity);
	Task SaveAsync();
	Task DeleteAsync(RefreshToken entityToDelete);
	Task<IEnumerable<RefreshToken>> GetAllAsync();
	Task UpdateAsync(RefreshToken ententityToUpdate);
	Task<RefreshToken?> GetRefreshTokenByToken(string token);
	Task<IEnumerable<RefreshToken>> GetRefreshTokenListByUserId(string userId);
}
