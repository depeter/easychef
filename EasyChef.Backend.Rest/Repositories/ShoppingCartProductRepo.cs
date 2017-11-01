using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyChef.Backend.Rest.Models;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IShoppingCartProductRepo : IGenericRepository<ShoppingCartProduct> { }
    public class ShoppingCartProductRepo : GenericRepository<DBContext, ShoppingCartProduct>, IShoppingCartProductRepo
    {
        public ShoppingCartProductRepo(DBContext db) : base(db)
        {

        }
    }
}
