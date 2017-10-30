using EasyChef.Shared.Models;
using RecepyPreparation = EasyChef.Backend.Rest.Models.RecepyPreparation;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IRecepyPreparationRepo : IGenericRepository<RecepyPreparation> { }
    public class RecepyPreparationRepo : GenericRepository<DBContext, RecepyPreparation>, IRecepyPreparationRepo
    {
        public RecepyPreparationRepo(DBContext db) : base(db)
        {

        }
    }
}
