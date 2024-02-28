using ErrorOr;
using Microsoft.AspNetCore.Http;
using Rivne.Booking.Domain.Apartments;

namespace Rivne.Booking.Application.Interfaces;

public interface IApartmentsRepository
{
	Task<ErrorOr<Apartment>> AddApartmentAsync(Apartment apartament, 
		string streetName, List<IFormFile> fileList);
	Task<ErrorOr<List<Apartment>>> GetAllApattmentsAsync();
	Task<ErrorOr<Apartment>> DeleteApartmentAsync(int id);
	Task<ErrorOr<Apartment>> GetApartmentByIdAsync(int apartmentId);
	Task<ErrorOr<Apartment>> EditApartmentAsync(Apartment apartment, string streetName, 
		List<IFormFile> newImageList, List<string> imageForDelete);
}
