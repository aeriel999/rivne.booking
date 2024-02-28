using Ardalis.Specification;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using rivne.booking.Infrastructure.Common.Persistence;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Apartments;
using Rivne.Booking.Domain.Users;

namespace rivne.booking.Infrastructure.Repository;

public class StreetRepository(ApiDbContext context) : IStreetRepository
{
	private readonly DbSet<Street> _dbSet = context.Set<Street>();

	public async Task<ErrorOr<List<string>>> GetListOfStreetNamesAsync()
	{
		try
		{
			var lisOfStreetNames = await _dbSet.Select(s => s.Name).ToListAsync();

			if (lisOfStreetNames == null)
				return Error.NotFound();

			return lisOfStreetNames;
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<Street>> GetOrCreateAndGetStreetByStreetNameAsync(string streetName)
	{
		//ToDo Make cheking registr before searching and creating of street
		var street = await _dbSet.FirstOrDefaultAsync(s => s.Name == streetName);

		if (street != null)
			return street;

		try
		{
			var newStreet = new Street();
			newStreet.Name = streetName;

			await _dbSet.AddAsync(newStreet);

			await context.SaveChangesAsync();

			return newStreet;
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}
}
