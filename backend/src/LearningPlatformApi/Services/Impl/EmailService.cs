using LearningPlatformApi.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace LearningPlatformApi.Services.Impl;

public class EmailService(IOptions<EmailSettings> emailSettings) : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(emailSettings.Value.SmtpFrom));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(emailSettings.Value.SmtpHost, emailSettings.Value.SmtpPort, MailKit.Security.SecureSocketOptions.None,
            cancellationToken);
        await smtp.AuthenticateAsync(emailSettings.Value.SmtpUsername, emailSettings.Value.SmtpPassword, cancellationToken);

        await smtp.SendAsync(message, cancellationToken);
        await smtp.DisconnectAsync(true, cancellationToken);
    }
}