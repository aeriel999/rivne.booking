namespace rivne.booking.api.Contracts.User.GetUser;

public record GetUserResponse(
    string Id,
    string? FirstName,
    string? LastName,
    string Email,
    bool EmailConfirmed,
    string? PhoneNumber,
    bool PhoneNumberConfirmed,
    bool LockoutEnabled,
    string Role,
    string Avatar);


