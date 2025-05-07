using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MyApp.Core.Entities;

namespace MyApp.Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {
        // This class represents a query to retrieve all users from the system.
        // It implements the IRequest interface from MediatR, which is used for sending requests and receiving responses.
        // The response type is a list of User entities.
    }
}