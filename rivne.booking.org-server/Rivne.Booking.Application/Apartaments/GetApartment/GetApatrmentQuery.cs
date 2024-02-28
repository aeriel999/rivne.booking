using ErrorOr;
using MediatR;
using Rivne.Booking.Domain.Apartments;

namespace Rivne.Booking.Application.Apartaments.GetApartment;

public record GetApatrmentQuery(int Id) : IRequest<ErrorOr<Apartment>>;
//{
//    public int Id { get; set; }
//    public int NumberOfBuilding { get; set; }
//    public bool IsPrivateHouse { get; set; }
//    public int NumberOfRooms { get; set; }
//    public int? Floor { get; set; }
//    public double Area { get; set; }
//    public decimal Price { get; set; }
//    public required string Description { get; set; }
//    public required string TypeOfBooking { get; set; }
//    public bool IsBooked { get; set; } = false;
//    public bool IsArchived { get; set; }
//    public bool IsPosted { get; set; }
//    public required string StreetName { get; set; }
//    public List<string>? Images { get; set; }
//}
