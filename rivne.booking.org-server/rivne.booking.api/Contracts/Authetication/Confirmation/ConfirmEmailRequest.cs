namespace rivne.booking.api.Contracts.Authetication.Confirmation;

public record ConfirmEmailRequest(string UserId, string Token);

