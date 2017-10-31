using EasyChef.Backend.Rest.Models;
using Microsoft.EntityFrameworkCore;
using Category = EasyChef.Backend.Rest.Models.Category;

namespace EasyChef.Backend.Rest
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        //public DbSet<HouseHold> HouseHolds { get; set; }
        //public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<Recepy> Recepies { get; set; }
        //public DbSet<RecepyPreparation> RecepyPreparations { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForSqlServerUseIdentityColumns();

            modelBuilder.Entity<Category>().HasKey(x => x.Id);
            modelBuilder.Entity<Category>().HasMany(x => x.Children).WithOne(x => x.Parent);

            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<Product>().HasOne(x => x.Category).WithMany(x => x.Products);
            modelBuilder.Entity<Product>().HasMany(x => x.ShoppingCartProduct).WithOne(x => x.Product);

            modelBuilder.Entity<ShoppingCart>().HasKey(x => x.Id);
            modelBuilder.Entity<ShoppingCart>().HasMany(x => x.ShoppingCartProducts).WithOne(x => x.ShoppingCart);

            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasOne(x => x.ShoppingCart).WithOne(x => x.User);
        }
    }
}
