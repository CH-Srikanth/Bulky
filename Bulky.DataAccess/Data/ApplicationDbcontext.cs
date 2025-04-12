using Bulky.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
    public class ApplicationDbcontext : DbContext //default to implement dbcontext class ,this is the root class for entity framework core ,using which we are accessing entity framework
    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //this method is only for hard enter data into db 
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "scifi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
            );

            modelBuilder.Ignore<SelectListItem>(); // ✅ Tells EF to ignore it

            modelBuilder.Entity<Products>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Products>().HasData(
                new Products
                {
                    Id = 1,
                    Title = "Clean Code",
                    Description = "A Handbook of Agile Software ",
                    ISBN = "9780132350884",
                    Author = "Robert C. Martin",
                    ListPrice = 40.00,
                    Price50 = 35.00,
                    Price100 = 30.00,
                    CategoryId = 3,
                    ImageUrl = ""
                },
                new Products
                {
                    Id = 2,
                    Title = "The Programmer",
                    Description = "Your Journey to Mastery",
                    ISBN = "9780135957059",
                    Author = "David Thomas",
                    ListPrice = 45.00,
                    Price50 = 40.00,
                    Price100 = 35.00,
                    CategoryId = 2,
                    ImageUrl = ""
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
