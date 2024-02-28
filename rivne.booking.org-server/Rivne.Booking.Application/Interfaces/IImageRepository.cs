using ErrorOr;
using Rivne.Booking.Domain.Apartments;

namespace Rivne.Booking.Application.Interfaces;

public interface IImageRepository
{
	Task<ErrorOr<Created>> InsertImageAsync(Image image);
	Task<ErrorOr<List<string>>> GetListOfImageNemesForApartmentIdAsync(int id);
	Task<ErrorOr<Deleted>> DeleteImageByName(string imageName);
}
