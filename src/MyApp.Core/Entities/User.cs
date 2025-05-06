using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Core.Entities
{
    public class User
    {
        // Properties of the User class
        public Guid Id { get; set; } = Guid.NewGuid(); // Better approach for unique identifier
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    

    }
    
}