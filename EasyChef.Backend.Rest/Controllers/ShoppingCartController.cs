using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EasyChef.Shared.Models;

namespace EasyChef.API.Controllers
{
    [Produces("application/json")]
    [Route("api/ShoppingCart")]
    public class ShoppingCartController : Controller
    {
        public ShoppingCartController()
        {
        }

        [HttpGet("GetByUser/{userId:long}")]
        public ActionResult GetByUser(long userId)
        {
            if (userId <= 0)
                ModelState.AddModelError("", "Please specify a valid user id.");

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok();
        }

        [HttpGet("{id:long}")]
        public ActionResult Get(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid ShoppingCart id.");

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        public ActionResult Post([FromBody]ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
                ModelState.AddModelError("", "Please specify an object of type ShoppingCart.");

            if (!ModelState.IsValid)
                return BadRequest();


            return Ok();
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid ShoppingCart id.");


            //if (repo.GetById(id) == null)
            //    ModelState.AddModelError("Id", "Unable to find a ShoppingCart with the specified id.");
            //
            //if (!ModelState.IsValid)
            //    return BadRequest();
            return Ok();

        }

        [HttpPut]
        public ActionResult Put(ShoppingCart shoppingCart)
        {
            //if (shoppingCart == null)
            //    ModelState.AddModelError("", "Please specify an object of type ShoppingCart.");
            //
            //if (shoppingCart.Id == 0)
            //    ModelState.AddModelError("Id", "Unable to update a ShoppingCart that doesn't exist yet.");
            //
            //if(!ModelState.IsValid)
            //    return BadRequest();
            //
            //using (IRedisClient redis = redisClientsManager.GetClient())
            //{
            //    var repo = redis.As<ShoppingCart>();
            //    shoppingCart.Id = repo.GetNextSequence();
            //    repo.Store(shoppingCart);
            //}
            return Ok();
        }
    }
}