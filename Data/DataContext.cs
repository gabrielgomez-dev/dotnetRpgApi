using dotnetRpgApi.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetRpgApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}