using AutoMapper;
 
using rivne.booking.Core.DTOs.Apartments;
using rivne.booking.Core.Entities.Apartments;
 
using rivne.booking.Core.Helpers;
using rivne.booking.Core.Interfaces;
using static rivne.booking.Core.Entities.Specification.Apartaments;

namespace rivne.booking.Core.Services;
public class ApartmentService
{
	private readonly IMapper _mapper;
	private readonly IRepository<Apartment> _apartmentRepository;
	private readonly IRepository<Image> _imageRepository;
	private readonly IRepository<Street> _streetRepository;
	public ApartmentService(IMapper mapper, IRepository<Apartment> repository, IRepository<Image> imageRepository, 
		IRepository<Street> streetRepository)
	{
		_mapper = mapper;
		_apartmentRepository = repository;
		_imageRepository = imageRepository;
		_streetRepository = streetRepository;
	}

	public async Task<ServiceResponse> AddApartmentAsync(AddApartmentDto model, string userId)
	{
		try
		{
			var apartment = _mapper.Map<Apartment>(model);

			apartment.UserId = userId;
			apartment.DateOfPost = DateTime.Now.ToUniversalTime();
			apartment.IsBooked = false;
			apartment.IsArchived = false;
			apartment.IsPosted = false;
			

			if (model.StreetId != 0)
			{
				apartment.StreetId = model.StreetId;
			}
			else
			{
				var newStreet = new Street();
				newStreet.Name = model.StreetName;

				await _streetRepository.InsertAsync(newStreet);
				await _apartmentRepository.SaveAsync();

				apartment.StreetId = newStreet.Id;
			}

			await _apartmentRepository.InsertAsync(apartment);
			await _apartmentRepository.SaveAsync();

			if(model.Images != null) 
			{
				foreach (var image in model.Images)
				{
					if (image != null)
					{
						Image ai = new Image();

						var imgResult = await ImageWorker.SaveImageAsync(image);

						if (!imgResult.Success)
						{
							return new ServiceResponse
							{
								Success = false,
								Message = imgResult.Message,
							};
						}

						ai.Name = (string)imgResult.PayLoad;
						ai.ApartmentId = apartment.Id;
						await _imageRepository.InsertAsync(ai);
						await _imageRepository.SaveAsync();
					}
				}
			}

			return new ServiceResponse
			{
				Success = true,
				Message = "Appartment is save",
			};
		}
		catch (Exception ex)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = ex.Message,
			};
		}
		
	}
	public async Task<ServiceResponse> DeleteApartmentAsync(int id)
	{
		try
		{
			//var specification = new GetApartmentWithDetails(id);

			//var apartmentWithDetails = await _apartmentRepository.GetItemBySpec(specification);

			//foreach (var image in apartmentWithDetails.Images)
			//{
			//	var imgDelResult = await ImageWorker.DeleteImageAsync(image.Name);

			//	if (!imgDelResult.Success)
			//	{
			//		return new ServiceResponse
			//		{
			//			Success = false,
			//			Message = imgDelResult.Message,
			//		};
			//	}
			//}

			await _apartmentRepository.DeleteAsync(id);
			await _apartmentRepository.SaveAsync();

			return new ServiceResponse
			{
				Success = true,
				Message = "Appartment deleted successfully",
			};
		}
		catch (Exception ex)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = ex.Message,
			};
		}
	}
	public async Task<ServiceResponse> GetAllApattmentsAsync()
    {
		try
		{
			//var specification = new GetAllApartmentsWithDetails();

			//var apartmentsWithDetails = await _apartmentRepository.GetListBySpecAsync(specification);

			//var mappedList = new List<ListApartmentDto>();

			//foreach (var a in apartmentsWithDetails)
			//{
			//	mappedList.Add(_mapper.Map<ListApartmentDto>(a));
			//}

			return new ServiceResponse
			{
				Success = true,
				//PayLoad = mappedList,
				Message = "Appartments are loaded",
			};
		}
		catch (Exception ex)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = ex.Message,
			};
		}
	}

	public async Task<ServiceResponse> GetStreetsAsync()
	{
		try
		{
			var streetsList = await _streetRepository.GetAllAsync();
			return new ServiceResponse
			{
				Success = true,
				PayLoad = streetsList,
				Message = "Appartments are loaded",
			};
		}
		catch (Exception ex)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = ex.Message,
			};
		}
	}

	public async Task<ServiceResponse> GetApartmentAsync(int apartmentId)
	{
		try
		{
		//	var specification = new GetApartmentWithDetails(apartmentId);

		//	var apartmentWithDetails = await _apartmentRepository.GetItemBySpec(specification);

		//	var mappedApartment = _mapper.Map<GetForEditApartment>(apartmentWithDetails);
 
			return new ServiceResponse
			{
				Success = true,
				//PayLoad = mappedApartment,
				Message = "Appartment are loaded",
			};
		}
		catch (Exception ex)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = ex.Message,
			};
		}
	}

	public async Task<ServiceResponse> EditApartmentAsync(EditApartment model)
	{
		try
		{
			var apartment = await _apartmentRepository.GetByIdAsync(model.Id);

			if (apartment == null) 
			{
				return new ServiceResponse
				{
					Success = false,
					Message = "There is no such apartment",
				};
			}

			apartment.NumberOfBuilding = model.NumberOfBuilding;
			apartment.IsPrivateHouse = model.IsPrivateHouse;
			apartment.NumberOfRooms = model.NumberOfRooms;
			apartment.Floor = model.Floor;
			apartment.Area = model.Area;
			apartment.Price = model.Price;
			apartment.Description = model.Description;
			apartment.TypeOfBooking = model.TypeOfBooking;
			apartment.IsBooked = model.IsBooked;
			apartment.IsArchived = model.IsArchived;
			apartment.IsPosted = false;
			apartment.DateOfUpdate = DateTime.Now.ToUniversalTime();


			if (model.StreetId == 0)
			{
				if (model.StreetName.Length > 0)
				{
					var newStreet = new Street();
					newStreet.Name = model.StreetName;

					await _streetRepository.InsertAsync(newStreet);
					await _apartmentRepository.SaveAsync();

					apartment.StreetId = newStreet.Id;
				}
			}
			else
			{
				apartment.StreetId = model.StreetId;
			}

			await _apartmentRepository.UpdateAsync(apartment);

			await _apartmentRepository.SaveAsync();

			if (model.ImagesForDelete != null)
			{
				foreach (var image in model.ImagesForDelete)
				{
					var specification = new GetImageByName(image);

					var imageForDel = await _imageRepository.GetItemBySpec(specification);

					await _imageRepository.DeleteAsync(imageForDel);

					await _imageRepository.SaveAsync();

					await ImageWorker.DeleteImageAsync(image);
				}
			}

			if (model.Images != null)
			{
				foreach (var image in model.Images)
				{
					if (image != null)
					{
						Image ai = new Image();

						var imgResult = await ImageWorker.SaveImageAsync(image);

						if (!imgResult.Success)
						{
							return new ServiceResponse
							{
								Success = false,
								Message = imgResult.Message,
							};
						}

						ai.Name = (string)imgResult.PayLoad;
						ai.ApartmentId = model.Id;
						await _imageRepository.InsertAsync(ai);
						await _imageRepository.SaveAsync();
					}
				}
			}

			return new ServiceResponse
			{
				Success = true,
			
				Message = "Appartment edit successfull",
			};
		}
		catch (Exception ex)
		{
			return new ServiceResponse
			{
				Success = false,
				Message = ex.Message,
			};
		}
	}

}
