using EasyChef.Shared.Models;

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
