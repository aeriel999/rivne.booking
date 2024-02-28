namespace Rivne.Booking.Application.Interfaces;

public interface IJwtService
{
	Task Create(RefreshToken token);
	Task Delete(RefreshToken token);
	Task Update(RefreshToken token);
//	Task<RefreshToken?> Get(string token);
	Task<IEnumerable<RefreshToken>> GetAll();
	Task<ErrorOr<UserTokens>> VerifyTokenAsync(string token, string refreshToken);
	Task<ErrorOr<UserTokens>> GenerateJwtTokensAsync(User domainUser);
	Task<ErrorOr<IEnumerable<RefreshToken>>> GetTokensByUserId(string userId);
}
