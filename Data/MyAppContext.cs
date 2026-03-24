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
            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CartItem>()
               .HasOne(c => c.ProductColor)
               .WithMany()
               .HasForeignKey(c => c.ProductColorId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrderItem>()
                .HasOne(c => c.ProductVariant)
                .WithMany()
                .HasForeignKey(c => c.ProductVariantId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrderItem>()
                .HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<OrderItem>()
               .HasOne(c => c.ProductColor)
               .WithMany()
               .HasForeignKey(c => c.ProductColorId)
               .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId , ur.RoleId });
        }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Brand> Brands { set; get; }
        public DbSet<CategoryBrand> CategoryBrands { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductColor> ProductColors { set; get; }
        public DbSet<ProductVariant> ProductVariants { set; get; }
        public DbSet<ProductSpecification> ProductSpecifications { set; get; }
        public DbSet<ProductSpecificationMapping> ProductSpecificationMappings { set; get; }

        public DbSet<Cart> Carts { set; get; }
        public DbSet<CartItem> CartItems { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderItem> OrderItems { set; get;}
        public DbSet<User> Users { set; get; }
        public DbSet<UserRole> UserRoles { set; get; }
        public DbSet<Role> Roles { set; get; }
    }
}
