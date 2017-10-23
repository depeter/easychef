using EasyChef.Shared.Models;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IHouseHoldRepo : IGenericRepository<HouseHold> { }

    public class HouseHoldRepo : GenericRepository<DBContext, HouseHold>, IHouseHoldRepo
    {
    }
}
