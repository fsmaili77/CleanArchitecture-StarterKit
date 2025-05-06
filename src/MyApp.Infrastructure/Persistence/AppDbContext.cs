using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.Core.Entities;

namespace MyApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        // This class represents the database context for the application.
        // It inherits from DbContext and provides access to the database using Entity Framework Core.
        // The context is responsible for managing the connection to the database, tracking changes,
        // and providing methods for querying and saving data.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // The DbContextOptions<TContext> parameter is used to configure the context with options such as the database provider and connection string.
            // The base constructor initializes the context with the provided options.        
        }
        public DbSet<User> Users  => Set<User>();        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed a user
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // Fixed GUID for repeatability
                Name = "Admin",
                Email = "admin@example.com",
                Password = "admin123", // ⚠️ Only for seeding — hash real passwords in prod!
                CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc) // ✅ fixed DateTime
            });
        }        
    }
    
    /* public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Configure the User entity properties and relationships here
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
            builder.Property(u => u.CreatedAt).IsRequired();
        }
    } */
}