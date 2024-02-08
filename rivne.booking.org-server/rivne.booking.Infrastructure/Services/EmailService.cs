using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Rivne.Booking.Application.Interfaces;

namespace rivne.booking.Infrastructure.Services;

public class EmailService(IConfiguration configuration) : IEmailService
{
	public async Task SendEmailAsync(string toEmail, string subject, string body)
	{
		string fromEmail = configuration["EmailSettings:User"];
		string SMTP = configuration["EmailSettings:SMTP"];
		int port = int.Parse(configuration["EmailSettings:port"]);
		string password = configuration["EmailSettings:Password"];

		var email = new MimeMessage();
		email.From.Add(MailboxAddress.Parse(fromEmail));
		email.To.Add(MailboxAddress.Parse(toEmail));
		email.Subject = subject;

		var bodyBuilder = new BodyBuilder();
		bodyBuilder.HtmlBody = body;
		email.Body = bodyBuilder.ToMessageBody();

		// send email
		using (var smtp = new SmtpClient())
		{
			smtp.Connect(SMTP, port, SecureSocketOptions.SslOnConnect);
			smtp.Authenticate(fromEmail, password);
			await smtp.SendAsync(email);
			smtp.Disconnect(true);
		}
	}
}
