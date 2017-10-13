using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyChef.Shared.Models;
using StackExchange.Redis;
using ServiceStack.Redis;

namespace EasyChef.API.Controllers
{
    [Produces("application/json")]
    [Route("api/ShoppingCart")]
    public class ShoppingCartController : Controller
    {
        private readonly IRedisClientsManager redisClientsManager;

        public ShoppingCartController(IRedisClientsManager redisClientsManager)
        {
            this.redisClientsManager = redisClientsManager;
        }

        [HttpGet("GetByUser/{userId:long}")]
        public ActionResult GetByUser(long userId)
        {
            if (userId <= 0)
                ModelState.AddModelError("", "Please specify a valid user id.");

            if (!ModelState.IsValid)
                return BadRequest();

            using (IRedisClient redis = redisClientsManager.GetClient())
            {
                var repo = redis.As<User>();
                return Ok(repo.GetRelatedEntities<ShoppingCart>(userId));
            }
        }

        [HttpGet("{id:long}")]
        public ActionResult Get(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid ShoppingCart id.");

            if (!ModelState.IsValid)
                return BadRequest();

            using (IRedisClient redis = redisClientsManager.GetClient())
            {
                var repo = redis.As<ShoppingCart>();
                return Ok(repo.GetById(id));
            }
        }

        [HttpPost]
        public ActionResult Post(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
                ModelState.AddModelError("", "Please specify an object of type ShoppingCart.");

            if (!ModelState.IsValid)
                return BadRequest();

            using (IRedisClient redis = redisClientsManager.GetClient())
            {
                var repo = redis.As<ShoppingCart>();
                shoppingCart.Id = repo.GetNextSequence();
                repo.Store(shoppingCart);
            }
            return Ok();
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid ShoppingCart id.");

            using (IRedisClient redis = redisClientsManager.GetClient())
            {
                var repo = redis.As<ShoppingCart>();

                if (repo.GetById(id) == null)
                    ModelState.AddModelError("Id", "Unable to find a ShoppingCart with the specified id.");

                if (!ModelState.IsValid)
                    return BadRequest();

                repo.Delete(repo.GetById(id));
                return Ok();
            }
        }

        [HttpPut]
        public ActionResult Put(ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
                ModelState.AddModelError("", "Please specify an object of type ShoppingCart.");

            if (shoppingCart.Id == 0)
                ModelState.AddModelError("Id", "Unable to update a ShoppingCart that doesn't exist yet.");

            if(!ModelState.IsValid)
                return BadRequest();

            using (IRedisClient redis = redisClientsManager.GetClient())
            {
                var repo = redis.As<ShoppingCart>();
                shoppingCart.Id = repo.GetNextSequence();
                repo.Store(shoppingCart);
            }
            return Ok();
        }
    }
}