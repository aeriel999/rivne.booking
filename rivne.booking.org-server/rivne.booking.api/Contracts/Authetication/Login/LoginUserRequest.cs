namespace rivne.booking.api.Contracts.Authetication.Login;

public record LoginUserRequest(
    string Email,
    string Password);
