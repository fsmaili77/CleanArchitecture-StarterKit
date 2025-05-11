using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace MyApp.Application.Users.Commands
{
    public class ResendVerificationCommand : IRequest<bool>
    {
        public string Email { get; set; } = default!;
    }
}