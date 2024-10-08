using Microsoft.EntityFrameworkCore;
using Spedito.Models;
using System.Reflection.Emit;

namespace Spedito.Data
{
    public class PizzaDb : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<NutritionalValue> NutritionalValues { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Thickness> Thicknesses { get; set; }
        public PizzaDb(DbContextOptions<PizzaDb> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasOne(c => c.Image)
                .WithOne(b => b.Category)
                .HasForeignKey<Category>(x => x.ImageId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Ingredient>()
                .HasOne(c => c.Image)
                .WithOne(b => b.Ingredient)
                .HasForeignKey<Ingredient>(x => x.ImageId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
