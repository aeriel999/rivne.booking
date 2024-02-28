namespace rivne.booking.api.Contracts.User;

public record UpdateUserProfileRequest(
    string? FirstName,
    string? LastName,
    string Email,
    string? PhoneNumber);
