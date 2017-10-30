using EasyChef.Shared.Models;
using Ingredient = EasyChef.Backend.Rest.Models.Ingredient;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IIngredientRepo : IGenericRepository<Ingredient> { }
    public class IngredientRepo : GenericRepository<DBContext, Ingredient>, IIngredientRepo
    {
        public IngredientRepo(DBContext db) : base(db)
        {

        }
    }
}
