using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

public class ResetPasswordCommand : IRequest<bool>
{
    public string Token { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}