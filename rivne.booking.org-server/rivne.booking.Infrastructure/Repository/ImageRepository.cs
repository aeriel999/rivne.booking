using Ardalis.Specification;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using rivne.booking.Infrastructure.Common.Persistence;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Apartments;
using Rivne.Booking.Domain.Users;

namespace rivne.booking.Infrastructure.Repository;

public class ImageRepository : IImageRepository
{
	private readonly ApiDbContext _context;
	private readonly DbSet<Image> _dbSet;

	public ImageRepository(ApiDbContext context)
	{
		_context = context;
		_dbSet = context.Set<Image>();
	}
	public async Task<ErrorOr<Deleted>> DeleteImageByName(string imageName)
	{
		try
		{
			var image = await _dbSet.Where(i => i.Name == imageName).FirstOrDefaultAsync();

			if (image == null)
				return Error.NotFound();

			await Task.Run(() =>
		   {
			   if (_context.Entry(image).State == EntityState.Detached)
			   {
				   _dbSet.Attach(image);
			   }
			   _dbSet.Remove(image);
		   });

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			 return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<List<string>>> GetListOfImageNemesForApartmentIdAsync(int id)
	{
		try
		{
			var imagesList = await _dbSet.Where(i => i.ApartmentId == id).Select(i => i.Name).ToListAsync();

			return imagesList;
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<Created>> InsertImageAsync(Image image)
	{
		try
		{
			await _dbSet.AddAsync(image);

			await _context.SaveChangesAsync();

			return Result.Created;
		}
		catch (Exception ex)
		{
			 return Error.Unexpected(ex.Message.ToString());
		}
	}
}
