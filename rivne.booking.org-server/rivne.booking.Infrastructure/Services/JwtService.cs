namespace rivne.booking.Infrastructure.Services;

public class JwtService(
	IConfiguration configuration,
	IRefreshTokenRepository tokenRepo,
	UserManager<AppUser> userManager,
	TokenValidationParameters validationParameters) : IJwtService
{
	public async Task Create(RefreshToken token)
	{
		await tokenRepo.InsertAsync(token);
		await tokenRepo.SaveAsync();
	}

	public async Task Delete(RefreshToken token)
	{
		await tokenRepo.DeleteAsync(token);
		await tokenRepo.SaveAsync();
	}

	//public async Task<RefreshToken?> Get(string token)
	//{
	//	var result = await tokenRepo.GetListBySpecAsync(new RefreshTokenSpecification.GetRefreshToken(token));
	//	return result.FirstOrDefault();
	//}

	public async Task<IEnumerable<RefreshToken>> GetAll()
	{
		IEnumerable<RefreshToken> result = await tokenRepo.GetAllAsync();
		return result;
	}

	public async Task Update(RefreshToken token)
	{
		await tokenRepo.UpdateAsync(token);
		await tokenRepo.SaveAsync();
	}

	public async Task<ErrorOr<UserTokens>> GenerateJwtTokensAsync(User domainUser)
	{
		try
		{
			var appUser = await userManager.FindByEmailAsync(domainUser.Email);

			if (appUser == null)
				return Error.NotFound(domainUser.Email);

			var roles = await userManager.GetRolesAsync(appUser);

			if (roles == null)
				return Error.NotFound("Role of user is not found");

			var jwtTokenHandler = new JwtSecurityTokenHandler();

			var key = Encoding.ASCII.GetBytes(configuration["JwtConfig:Secret"]!);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.NameIdentifier, appUser.Id),
					new Claim(ClaimTypes.GivenName, appUser.FirstName ?? ""),
					new Claim("Lastname", appUser.LastName ?? ""),
					new Claim(ClaimTypes.Email, appUser.Email!),
					new Claim("EmailConfirm", appUser.EmailConfirmed.ToString()),
					new Claim(ClaimTypes.MobilePhone, appUser.PhoneNumber ?? ""),
					new Claim("Avatar", appUser.Avatar ?? ""),
					new Claim(ClaimsIdentity.DefaultRoleClaimType, roles[0]),
					new Claim(JwtRegisteredClaimNames.Aud, configuration["JwtConfig:Audience"]!),
					new Claim(JwtRegisteredClaimNames.Iss, configuration["JwtConfig:Issuer"]!),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
					new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
				}),

				Expires = DateTime.UtcNow.AddDays(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
				SecurityAlgorithms.HmacSha512Signature)
			};

			var token = jwtTokenHandler.CreateToken(tokenDescriptor);
			var jwtToken = jwtTokenHandler.WriteToken(token);

			var refreshToken = new RefreshToken()
			{
				JwtId = token.Id,
				IsUsed = false,
				UserId = appUser.Id,
				AddedDate = DateTime.UtcNow,
				ExpireDate = DateTime.UtcNow.AddDays(1),
				IsRevoked = false,
				Token = RandomString(25) + Guid.NewGuid(),
			};

			await Create(refreshToken);

			var tokens = new UserTokens(jwtToken, refreshToken);

			return tokens;
		}
		catch (Exception ex)
		{
			return Error.Failure(ex.Message.ToString());
		}
	}

	//public async Task<RefreshToken?> GetRefreshToken(string token)
	//{
	//	var result = await tokenRepo.GetListBySpecAsync(new RefreshTokenSpecification.GetRefreshToken(token));
	//	return (RefreshToken?)result.FirstOrDefault();
	//}

	public async Task<ErrorOr<UserTokens>> VerifyTokenAsync(string token, string refreshToken)
	{
		var jwtTokenHandler = new JwtSecurityTokenHandler();

		try
		{
			validationParameters.ValidateLifetime = false;

			var principal = jwtTokenHandler.ValidateToken(token, validationParameters,
				out var validatedToken);

			if (validatedToken is JwtSecurityToken jwtSecurityToken)
			{
				var result = jwtSecurityToken.Header.Alg
					.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);

				if (result == false)
				{
					return Error.Validation();
				}
			}

			var utcExpireDate = long.Parse(principal.Claims
				.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)!.Value);

			var expDate = UnixTimeStampToDateTime(utcExpireDate);

			if (expDate < DateTime.UtcNow)
			{
				return Error.Failure("Cannot refresh token. Token expired.");
			}

			var storedToken = await tokenRepo.GetRefreshTokenByToken(refreshToken);

			if (storedToken == null)
			{
				return Error.Failure("Refrest token not found.");
			}

			if (DateTime.UtcNow > storedToken.ExpireDate)
			{
				return Error.Failure("Token has been expired.");
			}

			if (storedToken.IsUsed)
			{
				return Error.Failure("Token has been used.");
			}

			if (storedToken.IsRevoked)
			{
				return Error.Failure("Token has been revoked.");
			}

			var jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)!.Value;

			if (storedToken.JwtId != jti)
			{

				return Error.Failure("Token doesn't match the saved token.");
			}

			storedToken.IsUsed = true;

			await Update(storedToken);

			//ToDo ??? Making of DomainUser method
			var appUser = await userManager.FindByIdAsync(storedToken.UserId);

			if (appUser == null)
				return Error.NotFound("User is not found");

			var roles = await userManager.GetRolesAsync(appUser);

			if (roles == null)
				return Error.NotFound("User role is not found");

			var domainUser = AppUser.ToDomainUser(appUser, roles.FirstOrDefault()!);

			var tokens = await GenerateJwtTokensAsync(domainUser!);

			return tokens;
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
	{
		DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
		return dtDateTime;
	}
	private string RandomString(int length)
	{
		var random = new Random();
		var chars = "qwertyuiopasdfghjklzxcvbnm0987654321";
		return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
	}

	public async Task<ErrorOr<IEnumerable<RefreshToken>>> GetTokensByUserId(string userId)
	{
		try
		{
			IEnumerable<RefreshToken> getTokensResult = await tokenRepo.GetRefreshTokenListByUserId(userId);

			if (getTokensResult == null)
				return Error.NotFound();

			return getTokensResult.ToList();
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

}



