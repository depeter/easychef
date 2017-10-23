using EasyChef.Shared.Models;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IShoppingCartRepo : IGenericRepository<ShoppingCart> { }
    public class ShoppingCartRepo : GenericRepository<DBContext, ShoppingCart>, IShoppingCartRepo
    {
    }
}
