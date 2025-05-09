using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace MyApp.Application.Users.Commands
{
    public class LoginUserCommand : IRequest<string> // returns the JWT token
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}