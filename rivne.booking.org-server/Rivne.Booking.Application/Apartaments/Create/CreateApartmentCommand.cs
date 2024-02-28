using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Rivne.Booking.Domain.Apartments;

namespace Rivne.Booking.Application.Apartaments.Create;

public record CreateApartmentCommand(
    int NumberOfBuilding,
    bool IsPrivateHouse,
    int NumberOfRooms,
    int? Floor,
    double Area,
    decimal Price,
    string Description,
    string TypeOfBooking,
    string StreetName,
    List<IFormFile>? Images,
    string UserId) : IRequest<ErrorOr<Apartment>>;

