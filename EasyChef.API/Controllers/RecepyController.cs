using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyChef.API.Models;

namespace EasyChef.API.Controllers
{
    [Route("api/[controller]")]
    public class RecepyController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Recepy>> Get()
        {
            return await DocumentDBRepository<Recepy>.GetItemsAsync(x => true);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<Recepy>> Get(long id)
        {
            return await DocumentDBRepository<Recepy>.GetItemsAsync(x => x.Id == id);
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]Recepy recepy)
        {
            await DocumentDBRepository<Recepy>.CreateItemAsync(recepy);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]Recepy recepy)
        {
            await DocumentDBRepository<Recepy>.UpdateItemAsync(id, recepy);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await DocumentDBRepository<Recepy>.DeleteItemAsync(id);
        }
    }
}
