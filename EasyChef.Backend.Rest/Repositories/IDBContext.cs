using EasyChef.Shared.Models;
using Microsoft.EntityFrameworkCore;

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
        DbSet<ShoppingList> ShoppingLists { get; set; }
    }
}