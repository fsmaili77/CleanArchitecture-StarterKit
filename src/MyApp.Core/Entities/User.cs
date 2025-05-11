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
        public string Password { get; set; } = default!; // This should be used only for registration or login purposes.
        // Password should be hashed and not stored in plain text.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Role { get; set; } = "User"; // e.g., "Admin", "User", etc.
        public string? RefreshToken { get; set; } // For JWT refresh token functionality
        public DateTime RefreshTokenExpiryTime { get; set; } // Expiry time for the refresh token
        public bool IsEmailVerified { get; set; } = false;
        public string? EmailVerificationToken { get; set; }
        // Token used for email verification
        public DateTime? LastVerificationSentAt { get; set; }
    }
    
}