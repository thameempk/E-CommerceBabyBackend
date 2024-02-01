using BabyBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BabyBackend.DbContexts
{
    public class BabyDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;

        public BabyDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration["ConnectionString:DefaultConnection"];
        }

        public DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            
        //}

    }
}
