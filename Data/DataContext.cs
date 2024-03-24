using dotnetRpgApi.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetRpgApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "Fireball", Damage = 10 },
                new Skill { Id = 2, Name = "Giant Fireball", Damage = 30 },
                new Skill { Id = 3, Name = "Explosive Fireball", Damage = 60 },
                new Skill { Id = 4, Name = "Lightning Strike", Damage = 20 },
                new Skill { Id = 5, Name = "Giant Lightning Strike", Damage = 40 }
            );
        }
    }
}