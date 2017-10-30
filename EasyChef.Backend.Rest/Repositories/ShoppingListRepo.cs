using EasyChef.Shared.Models;
using ShoppingList = EasyChef.Backend.Rest.Models.ShoppingList;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IShoppingListRepo : IGenericRepository<ShoppingList> { }
    public class ShoppingListRepo : GenericRepository<DBContext, ShoppingList>, IShoppingListRepo
    {
        public ShoppingListRepo(DBContext db) : base(db)
        {

        }
    }
}
