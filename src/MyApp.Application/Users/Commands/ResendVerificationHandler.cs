using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MyApp.Application.Interfaces;
using System.Security.Cryptography;

namespace MyApp.Application.Users.Commands
{
    public class ResendVerificationHandler : IRequestHandler<ResendVerificationCommand, bool>
    {
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public ResendVerificationHandler(
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<bool> Handle(ResendVerificationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        if (user == null || user.IsEmailVerified) return false;

        if (user.LastVerificationSentAt.HasValue &&
            user.LastVerificationSentAt > DateTime.UtcNow.AddMinutes(-5))
        {
            throw new InvalidOperationException("You can request a new verification email only every 5 minutes.");
        }

        var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        user.EmailVerificationToken = token;
        user.LastVerificationSentAt = DateTime.UtcNow;
        var verifyLink = $"https://yourdomain.com/api/users/verify?token={token}";

        var htmlBody = $@"
            <html>
                <body style='font-family: sans-serif;'>
                    <h2>Welcome to MyApp!</h2>
                    <p>Thank you for registering. Please confirm your email by clicking the link below:</p>
                    <p><a href='{verifyLink}' style='color: white; background-color: #007bff; padding: 10px 15px; text-decoration: none; border-radius: 5px;'>Verify Email</a></p>
                    <p>If the button doesn't work, you can also copy and paste this URL into your browser:</p>
                    <p>{verifyLink}</p>
                    <br/>
                    <small>This link will expire after your account has been verified.</small>
                </body>
            </html>";
        await _emailService.SendAsync(
                user.Email,
                "Please verify your email",
                htmlBody
            );

        await _userRepository.UpdateUserAsync(user);
        return true;
    }
}
}