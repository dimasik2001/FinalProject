using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OlxAPI.Data.Entities;

namespace OlxAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AdsCategories> AdsCategories { get; set; }
        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasMany(i => i.Ads)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Category>().HasData(new Category
            {
                Id = 1,
                Name = "Clothes",
            },
            new Category
            {
                Id = 2,
                Name = "Tech",
            },
            new Category
            {
                Id = 3,
                Name = "Mobile phones",
            },
            new Category
            {
                Id = 4,
                Name = "Toys",
            },
            new Category
            {
                Id = 5,
                Name = "Sport products",
            },
            new Category
            {
                Id = 6,
                Name = "Automobiles",
            },
             new Category
             {
                 Id = 7,
                 Name = "Furniture",
             },
            new Category
            {
                Id = 8,
                Name = "Animals",
            },
            new Category
            {
                Id = 9,
                Name = "Home",
            },
            new Category
            {
                Id = 10,
                Name = "Children",
            },
            new Category
            {
                Id = 11,
                Name = "Beauty",
            },
            new Category
            {
                Id = 12,
                Name = "Business and services",
            },
            new Category
            {
                Id = 13,
                Name = "Education",
            },
            new Category
            {
                Id = 14,
                Name = "Books",
            },
            new Category
            {
                Id = 15,
                Name = "Realty",
            },
            new Category
            {
                Id = 16,
                Name = "Other",
            }
            );
        }
    }
}
