namespace rivne.booking.api.Contracts.User;

public record EditUserRequest(
    string Id,
    string? FirstName,
    string? LastName,
    string Email,
    bool EmailConfirmed,
    string? PhoneNumber,
    bool PhoneNumberConfirmed,
    bool LockoutEnabled,
    string Role);

