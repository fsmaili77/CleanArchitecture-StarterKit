using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApp.Core.Entities;

namespace MyApp.Application.Interfaces
{
    public interface IUserRepository
    {
        // This interface defines the contract for user-related data operations.
        // It includes methods for creating, retrieving, updating, and deleting user records.
        // Implementations of this interface will interact with the database or other data sources.
        Task<User> GetUserByIdAsync(Guid userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task CreateUserAsync(User user);    
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid userId);     
        Task<bool> UserExistsAsync(Guid userId);
        Task<bool> UserExistsByEmailAsync(string email);   
        Task<User?> GetUserByEmailAsync(string email); // This method checks if a user exists by their email.
        Task<User?> GetUserByEmailAndPasswordAsync(string email, string password); // This method retrieves a user by their email and password.
        Task<User?> GetUserByVerificationTokenAsync(string token); // This method retrieves a user by their email verification token.
    }
}