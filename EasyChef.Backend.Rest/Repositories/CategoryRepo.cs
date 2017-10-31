using EasyChef.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Category = EasyChef.Backend.Rest.Models.Category;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface ICategoryRepo : IGenericRepository<Category> {
        IList<Category> GetAllWithProducts();
    }

    public class CategoryRepo : GenericRepository<DBContext, Category>, ICategoryRepo
    {
        public CategoryRepo(DBContext db) : base(db)
        {

        }

        public IList<Category> GetAllWithProducts()
        {
            return _db.Categories.Where(x => x.HasProducts)
                                 .Where(x => !x.LastProductScan.HasValue || x.LastProductScan.Value < DateTime.Today.AddDays(-10))
                                 .ToList();
        }
    }

}
