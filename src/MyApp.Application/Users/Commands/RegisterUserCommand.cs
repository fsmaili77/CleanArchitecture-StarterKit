using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace MyApp.Application.Users.Commands
{
    // This class represents a command to register a new user in the application.
    // It contains properties for the user's name and email address.
    public class RegisterUserCommand : IRequest<Guid>
    
    {
        public Guid Id { get; set; }  = Guid.NewGuid(); // Default to a new GUID
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;        
        
    }
}