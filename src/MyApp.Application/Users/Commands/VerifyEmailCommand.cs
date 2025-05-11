using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace MyApp.Application.Users.Commands
{
    public class VerifyEmailCommand : IRequest<bool>
    {
        public string Token { get; set; } = default!;
    }
}