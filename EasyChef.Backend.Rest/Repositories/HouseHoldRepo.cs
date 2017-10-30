using EasyChef.Shared.Models;
using HouseHold = EasyChef.Backend.Rest.Models.HouseHold;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IHouseHoldRepo : IGenericRepository<HouseHold> { }

    public class HouseHoldRepo : GenericRepository<DBContext, HouseHold>, IHouseHoldRepo
    {
        public HouseHoldRepo(DBContext db) : base(db)
        {

        }
    }
}
