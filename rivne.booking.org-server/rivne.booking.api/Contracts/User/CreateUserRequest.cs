namespace rivne.booking.api.Contracts.User;

public record CreateUserRequest(
    string? FirstName,
    string? LastName,
    string Email,
    string? PhoneNumber,
    string Role,
    string Password,
    string ConfirmPassword);
