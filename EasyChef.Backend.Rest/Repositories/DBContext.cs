using EasyChef.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyChef.Backend.Rest.Repositories
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        //public DbSet<HouseHold> HouseHolds { get; set; }
        //public DbSet<Ingredient> Ingredients { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<Recepy> Recepies { get; set; }
        //public DbSet<RecepyPreparation> RecepyPreparations { get; set; }
        //public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        //public DbSet<ShoppingList> ShoppingLists { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForSqlServerUseIdentityColumns();
            modelBuilder.Entity<Category>().HasKey(x => x.Id);
            //modelBuilder.Entity<Category>().HasMany(x => x.Children).WithOne(x => x.Parent);
        }
    }
}
