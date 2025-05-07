using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MyApp.Application.DTOs;

namespace MyApp.Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        // This class represents a query to retrieve a user by their ID.
        // It implements the IRequest interface from MediatR, which is used for sending requests and receiving responses.
        // The response type is a UserDto, which is a Data Transfer Object representing the user data.
        public Guid UserId { get; set; } // The ID of the user to be retrieved.
    }
  } 