using GameZone.Data.Configuration;
using GameZone.Models;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Data
{
    public class ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }
        override protected void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameConfiguration).Assembly);
            modelBuilder.Entity<Category>().HasData(
            [
                new Category { Id = 1, Name = "Action" },
                new Category { Id = 2, Name = "Adventure" },
                new Category { Id = 3, Name = "Arcade" },
                new Category { Id = 4, Name = "Board" },
                new Category { Id = 5, Name = "Card" },
                new Category { Id = 6, Name = "Casino" },
                new Category { Id = 7, Name = "Casual" },
                new Category { Id = 8, Name = "Educational" },
                new Category { Id = 9, Name = "Music" },
                new Category { Id = 10, Name = "Puzzle" },
            ]);
            modelBuilder.Entity<Device>().HasData(
            [
                new Device { Id = 1, Name = "Android",Icon = "bi bi-android2" },
                new Device { Id = 2, Name = "iOS",Icon = "bi bi-apple" },
                new Device { Id = 3, Name = "Windows",Icon = "bi bi-windows" },
                new Device { Id = 4, Name = "Mac",Icon = "bi bi-command" },
                new Device { Id = 5, Name = "Linux",Icon = "bi bi-ubuntu" },
                new Device { Id = 6, Name = "Xbox" ,Icon = "bi bi-xbox"},
                new Device { Id = 7, Name = "PlayStation",Icon = "bi bi-playstation" },
                new Device { Id = 8, Name = "Nintendo",Icon = "bi bi-nintendo-switch" },
            ]);

        }
    }
}
