using EasyChef.Shared.Models;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IShoppingListRepo : IGenericRepository<ShoppingList> { }
    public class ShoppingListRepo : GenericRepository<DBContext, ShoppingList>, IShoppingListRepo
    {
    }
}
