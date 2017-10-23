using EasyChef.Shared.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface ICategoryRepo : IGenericRepository<Category> { }

    public class CategoryRepo : GenericRepository<DBContext, Category>, ICategoryRepo
    {

    }

}
