using Microsoft.EntityFrameworkCore;
using src.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace src.Data {
    public class ProjectContext : IdentityDbContext<ApplicationUser> {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) {}

        public DbSet<Product>? Products {get; set;}
        public DbSet<Cart>? Cart {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Cart>().ToTable("Cart");
        }
    }
}