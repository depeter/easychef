using EasyChef.Shared.Models;
using Recepy = EasyChef.Backend.Rest.Models.Recepy;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IRecepyRepo : IGenericRepository<Recepy> { }
    public class RecepyRepo : GenericRepository<DBContext, Recepy>, IRecepyRepo
    {
        public RecepyRepo(DBContext db) : base(db)
        {

        }
    }
}
