using DataAccess.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options )
        {

            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure UserHasRole
            modelBuilder.Entity<UserHasRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserHasRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserHasRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // Configure OrderHasProduct
            modelBuilder.Entity<OrderHasProduct>()
           .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderHasProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderHasProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId);
        

        // Configure ProductHasCategory
        modelBuilder.Entity<ProductHasCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductHasCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductHasCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<ShoppingCartItem>()
           .HasKey(s => s.Id); // Define primary key

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(s => s.User) // Relationship with User
                .WithMany(u => u.ShoppingCartItems)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(s => s.Product) // Relationship with Product
                .WithMany(p => p.ShoppingCartItems)
                .HasForeignKey(s => s.ProductId);
        }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        public DbSet<UserHasRole> UserRoles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderHasProduct> OrderProducts { get; set; }
        public DbSet<ProductHasCategory> ProductCategories { get; set; }    
        // Add here DbSet for the table
    }
}
