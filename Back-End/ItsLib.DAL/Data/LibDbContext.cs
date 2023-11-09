using ItsLib.DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ItsLib.DAL.Data
{
    public class LibDbContext : IdentityDbContext<IdentityUser>
    {
        public LibDbContext() { }
        public LibDbContext(DbContextOptions<LibDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<DiscountCode> DiscountCode { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductUser> ProductUser { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserDiscountCode> UserDiscountCode { get; set; }
    }
}
