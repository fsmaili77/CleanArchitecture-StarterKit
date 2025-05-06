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
            // This method is called when the RegisterUserCommand is invoked.
            var user = new User 
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow
            };
            // Validate the user data
            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Email))
            {
                throw new ArgumentException("Name and Email are required fields.");
            }
            // Check if the user already exists
            var existingUser = await _userRepository.GetUserByIdAsync(request.Id);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User already exists.");
            }
            // Save the user to the database
            await _userRepository.CreateUserAsync(user);
             // Return the ID of the newly created user
             
             return user.Id; 
        }    
    }
}