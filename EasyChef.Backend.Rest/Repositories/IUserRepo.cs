using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyChef.Backend.Rest.Models;

namespace EasyChef.Backend.Rest.Repositories
{
    public interface IUserRepo : IGenericRepository<User>
    {
    }

    public class UserRepo : GenericRepository<DBContext, User>, IUserRepo
    {
        public UserRepo(DBContext db) : base(db)
        {

        }
    }
}
