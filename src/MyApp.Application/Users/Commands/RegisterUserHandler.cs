using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;


namespace MyApp.Application.Users.Commands
{
    public class RegisterUserHandler: IRequestHandler<RegisterUserCommand, Guid>
    {
        // This class handles the registration of a new user in the application.
        // It contains methods to validate user data, save the user to the database, and send notifications.
        private readonly IUserRepository _userRepository;
        public RegisterUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                throw new ArgumentException("Name, Email, and Password are required.");

            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
                throw new InvalidOperationException("A user with this email already exists.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            // Hash the password using BCrypt or any other hashing algorithm

            // Generate secure email verification token
            var emailVerificationToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Password = hashedPassword,
                Role = request.Role ?? "User",
                CreatedAt = DateTime.UtcNow,
                EmailVerificationToken = emailVerificationToken,
                IsEmailVerified = false
            };
            // Log the token for debugging purposes
            Console.WriteLine($"Email Verification Token: {emailVerificationToken}");

            await _userRepository.CreateUserAsync(user);
            return user.Id;
        }
    }
}