using BabyBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

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

        public DbSet<Product> products { get; set; }

        public DbSet<Cart> cart { get; set; }

        public DbSet<CartItem> cartItems { get; set; }

        public DbSet<Category> categories { get; set; }

        public DbSet<WhishList> whishLists { get; set; }

        public DbSet<OrderMain> orders { get; set; }

        public DbSet<OrderItem> orderItems {  get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
               .HasOne(u => u.cart)
               .WithOne(c => c.users)
               .HasForeignKey<Cart>(c => c.UserId);

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.cartItems)
                .WithOne(ci => ci.cart)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<Users>()
                .Property(u => u.Role)
                .HasDefaultValue("user");

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<WhishList>()
                .HasOne(w => w.users)
                .WithMany(u => u.whishLists)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<WhishList>()
                .HasOne(w => w.products)
                .WithMany()
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<OrderMain>()
                .HasOne(o => o.users)
                .WithMany(u => u.Orders)
                .HasForeignKey(u => u.userId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<Users>()
                .Property(u => u.isBlocked)
                .HasDefaultValue(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}
