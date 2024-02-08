namespace rivne.booking.api.Contracts;

public record RegisterUserRequest(
	string Email, 
	string Password, 
	string ConfirmPassword);
 