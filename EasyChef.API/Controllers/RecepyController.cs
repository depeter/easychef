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
    [Route("api/Recepy")]
    public class RecepyController : Controller
    {
        private readonly IRedisClientsManager redisClientsManager;

        public RecepyController(IRedisClientsManager redisClientsManager)
        {
            this.redisClientsManager = redisClientsManager;
        }

        [Route("api/Recepy/{id:long}")]
        [HttpGet]
        public ActionResult Get(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid Recepy id.");

            if (!ModelState.IsValid)
                return BadRequest();

            using (IRedisClient redis = redisClientsManager.GetClient())
            {
                var repo = redis.As<Recepy>();
                return Ok(repo.GetById(id));
            }
        }

        [Route("api/Recepy")]
        [HttpPost]
        public ActionResult Post(Recepy Recepy)
        {
            if (Recepy == null)
                ModelState.AddModelError("", "Please specify an object of type Recepy.");

            if (!ModelState.IsValid)
                return BadRequest();

            using (IRedisClient redis = redisClientsManager.GetClient())
            {
                var repo = redis.As<Recepy>();
                Recepy.Id = repo.GetNextSequence();
                repo.Store(Recepy);
            }
            return Ok();
        }

        [Route("api/Recepy")]
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid Recepy id.");

            using (IRedisClient redis = redisClientsManager.GetClient())
            {
                var repo = redis.As<Recepy>();

                if (repo.GetById(id) == null)
                    ModelState.AddModelError("Id", "Unable to find a Recepy with the specified id.");

                if (!ModelState.IsValid)
                    return BadRequest();

                repo.Delete(repo.GetById(id));
                return Ok();
            }
        }

        [Route("api/Recepy")]
        [HttpPut]
        public ActionResult Put(Recepy Recepy)
        {
            if (Recepy == null)
                ModelState.AddModelError("", "Please specify an object of type Recepy.");

            if (Recepy.Id == 0)
                ModelState.AddModelError("Id", "Unable to update a Recepy that doesn't exist yet.");

            if (!ModelState.IsValid)
                return BadRequest();

            using (IRedisClient redis = redisClientsManager.GetClient())
            {
                var repo = redis.As<Recepy>();
                Recepy.Id = repo.GetNextSequence();
                repo.Store(Recepy);
            }
            return Ok();
        }
    }
}