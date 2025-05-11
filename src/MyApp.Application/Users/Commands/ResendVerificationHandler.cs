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

        public ResendVerificationHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ResendVerificationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null || user.IsEmailVerified) return false;

            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            user.EmailVerificationToken = token;
            user.LastVerificationSentAt = DateTime.UtcNow;
            
            if (user.LastVerificationSentAt.HasValue && 
                user.LastVerificationSentAt > DateTime.UtcNow.AddMinutes(-5))
            {
                throw new InvalidOperationException("You can request a new verification email only every 5 minutes.");
            }

            await _userRepository.UpdateUserAsync(user);

            Console.WriteLine($"[Resent Verification Link] https://yourdomain.com/api/users/verify?token={token}");

            return true;
        }
    }
}