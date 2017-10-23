using EasyChef.Shared.Models;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IRecepyPreparationRepo : IGenericRepository<RecepyPreparation> { }
    public class RecepyPreparationRepo : GenericRepository<DBContext, RecepyPreparation>, IRecepyPreparationRepo
    {
    }
}
