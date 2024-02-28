using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Rivne.Booking.Domain.Apartments;

namespace Rivne.Booking.Application.Apartaments.Edit;
public record EditApartmentCommand(
    int Id,
    int NumberOfBuilding,
    bool IsPrivateHouse,
    int NumberOfRooms,
    int? Floor,
    double Area,
    decimal Price,
    string TypeOfBooking,
    string Description,
    bool IsBooked,
    bool IsArchived,
    string StreetName,
    List<string>? ImagesForDelete,
    List<IFormFile>? Images) : IRequest<ErrorOr<Apartment>>;
