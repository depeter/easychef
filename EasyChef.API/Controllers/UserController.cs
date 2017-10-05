using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyChef.API.Models;

namespace EasyChef.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await DocumentDBRepository<User>.GetItemsAsync(x => true);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<User>> Get(long id)
        {
            return await DocumentDBRepository<User>.GetItemsAsync(x => x.Id == id);
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]User User)
        {
            await DocumentDBRepository<User>.CreateItemAsync(User);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]User User)
        {
            await DocumentDBRepository<User>.UpdateItemAsync(id, User);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await DocumentDBRepository<User>.DeleteItemAsync(id);
        }
    }
}
