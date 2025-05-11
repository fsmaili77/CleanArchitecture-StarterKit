using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MyApp.Application.Interfaces;

namespace MyApp.Application.Users.Commands
{
    public class VerifyEmailHandler : IRequestHandler<VerifyEmailCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public VerifyEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByVerificationTokenAsync(request.Token);
            if (user == null) return false;

            user.IsEmailVerified = true;
            user.EmailVerificationToken = null;

            await _userRepository.UpdateUserAsync(user);
            return true;
        }
    }
}