using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class MyAppContext: DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options)
        : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryBrand>()
                .HasKey(cb => new { cb.CategoryId, cb.BrandId });
            modelBuilder.Entity<ProductSpecificationMapping>()
                .HasKey(cb => new { cb.ProductId, cb.ProductSpecificationId });
            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.ProductVariant)
                .WithMany()
                .HasForeignKey(c => c.ProductVariantId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrderItem>()
                .HasOne(c => c.ProductVariant)
                .WithMany()
                .HasForeignKey(c => c.ProductVariantId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId , ur.RoleId });
            modelBuilder.Entity<SpecificationDetail>()
                .HasOne(sd => sd.ProductSpecification)
                .WithMany()
                .HasForeignKey(sd => sd.ProductSpecificationId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductCode)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u=> new {u.Email, u.PhoneNumber})       
                .IsUnique();
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderCode)
                .IsUnique();
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CategoryCode)
                .IsUnique();
            modelBuilder.Entity<Brand>()
                .HasIndex(b => b.BrandCode)
                .IsUnique();
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItem)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.NoAction);

        }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Brand> Brands { set; get; }
        public DbSet<CategoryBrand> CategoryBrands { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductVariant> ProductVariants { set; get; }
        public DbSet<ProductSpecification> ProductSpecifications { set; get; }
        public DbSet<ProductSpecificationMapping> ProductSpecificationMappings { set; get; }
        public DbSet<SpecificationDetail> SpecificationDetail { set; get; }
        public DbSet<Cart> Carts { set; get; }
        public DbSet<CartItem> CartItems { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderItem> OrderItems { set; get;}
        public DbSet<User> Users { set; get; }
        public DbSet<UserRole> UserRoles { set; get; }
        public DbSet<Role> Roles { set; get; }
        public DbSet<UserToken> UserTokens { set; get; }
    }
}
