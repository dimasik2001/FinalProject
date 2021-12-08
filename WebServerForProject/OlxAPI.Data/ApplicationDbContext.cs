using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OlxAPI.Data.Entities;

namespace OlxAPI.Data
{
    public class ApplicationDbContext: IdentityDbContext<User>
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
                Name = "Other",
            }
            );
            var admin = new IdentityRole("Admin");
            admin.NormalizedName = admin.Name.ToUpper();
            var user = new IdentityRole("User");
            user.NormalizedName = user.Name.ToUpper();
            builder.Entity<IdentityRole>().HasData(
                admin, user);
        }
    }
}
