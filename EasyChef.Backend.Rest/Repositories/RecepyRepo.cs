using EasyChef.Shared.Models;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IRecepyRepo : IGenericRepository<Recepy> { }
    public class RecepyRepo : GenericRepository<DBContext, Recepy>, IRecepyRepo
    {
    }
}
