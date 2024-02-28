using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using rivne.booking.Infrastructure.Common.Identity;
using rivne.booking.Infrastructure.Common.Persistence;
using Rivne.Booking.Application.Interfaces;
using Rivne.Booking.Domain.Apartments;

namespace rivne.booking.Infrastructure.Repository;

public class ApartmentsRepository(ApiDbContext context, IStreetRepository streetRepository,
	IImageRepository imageRepository, IImageStorageService imageStorageService,
	UserManager<AppUser> userManager) : IApartmentsRepository
{
	private readonly DbSet<Apartment> _dbSet = context.Set<Apartment>();

	public async Task<ErrorOr<Apartment>> AddApartmentAsync(Apartment apartment, 
		string streetName, List<IFormFile> fileList)
	{
		try
		{
			apartment.DateOfPost = DateTime.Now.ToUniversalTime();
			apartment.IsBooked = false;
			apartment.IsArchived = false;
			apartment.IsPosted = false;

			var appUser = await userManager.FindByIdAsync(apartment.UserId);

			if (appUser == null)
				return Error.NotFound(nameof(appUser));

			//ToDo ??? Is It Wright UserName

			if (appUser.LastName == null && appUser.FirstName == null)
			{
				apartment.UserName = appUser.UserName!.Split("@").ToString()!;
			}

			apartment.UserName = ((appUser.FirstName == null ? "" :  (appUser.FirstName + " ")) + 
				appUser.LastName == null ? "" : appUser.LastName)!;

			var errorOrStreet = await streetRepository.GetOrCreateAndGetStreetByStreetNameAsync(streetName);

			if (errorOrStreet.IsError)
				return Error.Unexpected(errorOrStreet.FirstError.Description);

			apartment.StreetId = errorOrStreet.Value.Id;

			await _dbSet.AddAsync(apartment);
			await context.SaveChangesAsync();

			if (apartment.Images != null)
			{
				foreach (var image in fileList)
				{
					if (image != null)
					{
						var errorOrImageName = await imageStorageService.SaveImageAsync(image);

						if (errorOrImageName.IsError)
							return Error.Failure("Error in saving of image");

						Image newImage = new Image
						{
							Name = errorOrImageName.Value,
							ApartmentId = apartment.Id
						};

						var errorOrImageInsert = await imageRepository.InsertImageAsync(newImage);

						if (errorOrImageInsert.IsError)
							return Error.Unexpected(errorOrImageInsert.FirstError.Description);
					}
				}
			}

			return apartment;
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<Apartment>> DeleteApartmentAsync(int id)
	{
		try
		{
			var entityToDelete = await _dbSet.FindAsync(id);

			if (entityToDelete == null)
				return Error.NotFound();

			var imagesNameListOrNull = await imageRepository.GetListOfImageNemesForApartmentIdAsync(id);

			if (imagesNameListOrNull.IsError)
				return Error.Unexpected(imagesNameListOrNull.FirstError.Description);

			if (imagesNameListOrNull.Value != null)
			{
				foreach (var image in imagesNameListOrNull.Value)
				{
					var imgDelResult = await imageStorageService.DeleteImageAsync(image);

					if (imgDelResult.IsError)
						return Error.Failure(imgDelResult.FirstError.Description);
				}
			}

			await Task.Run(() =>
			{
				if (context.Entry(entityToDelete).State == EntityState.Detached)
				{
					_dbSet.Attach(entityToDelete);
				}
				_dbSet.Remove(entityToDelete);
			});

			await context.SaveChangesAsync();

			return entityToDelete;
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<Apartment>> EditApartmentAsync(Apartment newApartmentModel, string streetName,
		List<IFormFile> newImageList, List<string> imageForDelete)
	{
		try
		{
			var apartment = await _dbSet.Where(a => a.Id == newApartmentModel.Id)
										.Include(a => a.Street)
										.FirstOrDefaultAsync(); 

			if (apartment == null)
				return Error.NotFound();

			apartment.NumberOfBuilding = newApartmentModel.NumberOfBuilding;
			apartment.IsPrivateHouse = newApartmentModel.IsPrivateHouse;
			apartment.NumberOfRooms = newApartmentModel.NumberOfRooms;
			apartment.Floor = newApartmentModel.Floor;
			apartment.Area = newApartmentModel.Area;
			apartment.Price = newApartmentModel.Price;
			apartment.Description = newApartmentModel.Description;
			apartment.TypeOfBooking = newApartmentModel.TypeOfBooking;
			apartment.IsBooked = newApartmentModel.IsBooked;
			apartment.IsArchived = newApartmentModel.IsArchived;
			apartment.IsPosted = false;
			apartment.DateOfUpdate = DateTime.Now.ToUniversalTime();

			if (apartment.Street.Name != streetName)
			{
				var errorOrStreet = await streetRepository.GetOrCreateAndGetStreetByStreetNameAsync(streetName);

				if (errorOrStreet.IsError)
					return Error.Failure(errorOrStreet.FirstError.Description);

				apartment.StreetId = errorOrStreet.Value.Id;
			}

			//ToDo ??? Is it neccessary for save and delete
			await Task.Run(() =>
					   {
						   _dbSet.Attach(apartment);
						   context.Entry(apartment).State = EntityState.Modified;
					   });

			await context.SaveChangesAsync();

			if (imageForDelete != null)
			{
				foreach (var image in imageForDelete)
				{
					var errorOrDeletedImage = await imageRepository.DeleteImageByName(image);

					if (errorOrDeletedImage.IsError)
						return Error.Failure(errorOrDeletedImage.FirstError.Description);

					var errorOrDeleteFile = await imageStorageService.DeleteImageAsync(image);

					if (errorOrDeleteFile.IsError)
						return Error.Failure(errorOrDeleteFile.FirstError.Description);
				}
			}

			if (newImageList != null)
			{
				foreach (var image in newImageList)
				{
					if (image != null)
					{
						var errorOrImageName = await imageStorageService.SaveImageAsync(image);

						if (errorOrImageName.IsError)
							return Error.Failure(errorOrImageName.FirstError.Description);

						Image newImage = new()
						{
							Name = errorOrImageName.Value,
							ApartmentId = newApartmentModel.Id
						};

						var errorOrInsert = await imageRepository.InsertImageAsync(newImage);

						if (errorOrInsert.IsError)
							return Error.Failure(errorOrInsert.FirstError.Description);
					}
				}
			}

			return apartment;
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<List<Apartment>>> GetAllApattmentsAsync()
	{
		try
		{
			var listOfApartments = await _dbSet.Include(a => a.Street)
												.Include(a => a.Images)
												.ToListAsync();

			if (listOfApartments == null)
				return Error.NotFound();

			return listOfApartments;
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}

	public async Task<ErrorOr<Apartment>> GetApartmentByIdAsync(int apartmentId)
	{
		try
		{
			var apartment = await _dbSet.Where(a => a.Id == apartmentId)
										  .Include(a => a.Street)
										  .Include(a => a.Images).FirstOrDefaultAsync();

			if (apartment == null)
				return Error.NotFound();
			
			return apartment;	
		}
		catch (Exception ex)
		{
			return Error.Unexpected(ex.Message.ToString());
		}
	}
}
