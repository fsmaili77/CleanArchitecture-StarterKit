using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MediatR;
using MyApp.Application.Interfaces;

namespace MyApp.Application.Users.Commands
{
    public class RequestPasswordResetHandler : IRequestHandler<RequestPasswordResetCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public RequestPasswordResetHandler(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<bool> Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null || !user.IsEmailVerified)
                return false;

            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            user.PasswordResetToken = token;
            user.PasswordResetTokenExpiry = DateTime.UtcNow.AddMinutes(30);

            var resetLink = $"https://yourdomain.com/api/users/reset-password?token={token}";
            var htmlBody = $@"
                <html>
                <body style='font-family: sans-serif;'>
                    <h2>Password Reset Request</h2>
                    <p>We received a request to reset your password. Click below to continue:</p>
                    <p><a href='{resetLink}' style='color: white; background-color: #dc3545; padding: 10px 15px; text-decoration: none; border-radius: 5px;'>Reset Password</a></p>
                    <p>If you did not request this, you can safely ignore this message.</p>
                    <p>{resetLink}</p>
                    <br/>
                    <small>This link will expire in 30 minutes.</small>
                </body>
                </html>";

            await _emailService.SendAsync(user.Email, "Reset your password", htmlBody);
            await _userRepository.UpdateUserAsync(user);

            return true;
        }
    }
}