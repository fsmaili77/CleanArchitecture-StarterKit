using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Core.Entities
{
    public class User
    {
        // Properties of the User class
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        // Password should be hashed and not stored in plain text.
        // Use a library like BCrypt.Net to hash passwords before storing them.
        public string PasswordHash { get; set; } = default!; // Store the hashed password here.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Role { get; set; } = "User"; // e.g., "Admin", "User", etc.
    }
    
}