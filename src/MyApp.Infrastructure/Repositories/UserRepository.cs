using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using MyApp.Infrastructure.Persistence;

namespace MyApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        // This class implements the IUserRepository interface and provides concrete methods for user-related data operations.
        // It interacts with the database or other data sources to perform CRUD operations on user records.
        // The actual implementation would depend on the specific requirements of the application.
        // For example, it might include methods for querying the database, handling transactions, and managing connections.
        // The repository pattern is used to abstract the data access layer from the rest of the application,
        // allowing for better separation of concerns and easier testing.
        // The UserRepository class would typically be injected into services or controllers that require access to user data,
        // allowing them to perform operations such as creating, retrieving, updating, and deleting users.
        
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }        
    

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> UserExistsAsync(Guid userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }
        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}