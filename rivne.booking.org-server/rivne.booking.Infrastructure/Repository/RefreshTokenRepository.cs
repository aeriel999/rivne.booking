using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using rivne.booking.Infrastructure.Common.Persistence;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Users;

namespace rivne.booking.Infrastructure.Repository;

public class RefreshTokenRepository : IRefreshTokenRepository
{
	private readonly ApiDbContext _context;
	private readonly DbSet<RefreshToken> _dbSet;

	public RefreshTokenRepository(ApiDbContext context)
	{
		_context = context;
		_dbSet = context.Set<RefreshToken>();
	}
	public async Task InsertAsync(RefreshToken entity)
	{
		await _dbSet.AddAsync(entity);
	}

	public async Task SaveAsync()
	{
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(RefreshToken entityToDelete)
	{
		await Task.Run(
			() =>
			{
				if (_context.Entry(entityToDelete).State == EntityState.Detached)
				{
					_dbSet.Attach(entityToDelete);
				}
				_dbSet.Remove(entityToDelete);
			});
	}

	public async Task<IEnumerable<RefreshToken>> GetAllAsync()
	{
		return await _dbSet.ToListAsync();
	}

	public async Task UpdateAsync(RefreshToken ententityToUpdate)
	{
		await Task.Run
			(
			() =>
			{
				_dbSet.Attach(ententityToUpdate);
				_context.Entry(ententityToUpdate).State = EntityState.Modified;
			});
	}

	public async Task<RefreshToken?> GetRefreshTokenByToken(string token)
	{
		return await _dbSet
		.Where(rt => rt.Token == token)
		.FirstOrDefaultAsync();
	}

	public async Task<IEnumerable<RefreshToken>> GetRefreshTokenListByUserId(string userId)
	{
		return await _dbSet
			.Where(t => t.UserId == userId)
			.ToListAsync();
	}
}
