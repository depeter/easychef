using EasyChef.Shared.Models;
using ShoppingCart = EasyChef.Backend.Rest.Models.ShoppingCart;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IShoppingCartRepo : IGenericRepository<ShoppingCart> { }
    public class ShoppingCartRepo : GenericRepository<DBContext, ShoppingCart>, IShoppingCartRepo
    {
        public ShoppingCartRepo(DBContext db) : base(db)
        {

        }
    }
}
