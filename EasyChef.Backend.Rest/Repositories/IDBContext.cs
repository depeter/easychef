using EasyChef.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Category = EasyChef.Backend.Rest.Models.Category;
using HouseHold = EasyChef.Backend.Rest.Models.HouseHold;
using Ingredient = EasyChef.Backend.Rest.Models.Ingredient;
using Product = EasyChef.Backend.Rest.Models.Product;
using Recepy = EasyChef.Backend.Rest.Models.Recepy;
using RecepyPreparation = EasyChef.Backend.Rest.Models.RecepyPreparation;
using ShoppingCart = EasyChef.Backend.Rest.Models.ShoppingCart;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IDBContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<HouseHold> HouseHolds { get; set; }
        DbSet<Ingredient> Ingredients { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Recepy> Recepies { get; set; }
        DbSet<RecepyPreparation> RecepyPreparations { get; set; }
        DbSet<ShoppingCart> ShoppingCarts { get; set; }
    }
}