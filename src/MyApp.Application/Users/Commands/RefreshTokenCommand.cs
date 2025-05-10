using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace MyApp.Application.Users.Commands
{
    public class RefreshTokenCommand : IRequest<string>
    {
        public string Email { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}