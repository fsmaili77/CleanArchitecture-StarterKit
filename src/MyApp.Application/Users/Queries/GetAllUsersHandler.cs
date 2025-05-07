using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;

namespace MyApp.Application.Users.Queries
{
    public class GetAllUsersHandler :IRequestHandler<GetAllUsersQuery, List<User>>    
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            // This method handles the GetAllUsersQuery and retrieves all users from the repository.
            // It returns a list of User entities.
            var users = await _userRepository.GetAllUsersAsync();
            return users.ToList();
        }
        
    }
}