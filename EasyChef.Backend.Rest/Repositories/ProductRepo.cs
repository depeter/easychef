using EasyChef.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IProductRepo : IGenericRepository<Product> { }
    public class ProductRepo : GenericRepository<DBContext, Product>, IProductRepo
    {
        public ProductRepo(DBContext db) : base(db)
        {

        }
    }
}
