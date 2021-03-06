﻿using System;
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
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IRedisClientsManager redisClientsManager;

        public UserController(IRedisClientsManager redisClientsManager)
        {
            this.redisClientsManager = redisClientsManager;
        }

        [Route("api/User/{id:long}")]
        [HttpGet]
        public ActionResult Get(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid User id.");

            if (!ModelState.IsValid)
                return BadRequest();

            using (IRedisClient redis = redisClientsManager.GetClient())
            {
                var repo = redis.As<User>();
                return Ok(repo.GetById(id));
            }
        }

        [Route("api/User")]
        [HttpPost]
        public ActionResult Post(User User)
        {
            if (User == null)
                ModelState.AddModelError("", "Please specify an object of type User.");

            if (!ModelState.IsValid)
                return BadRequest();

            using (IRedisClient redis = redisClientsManager.GetClient())
            {
                var repo = redis.As<User>();
                User.Id = repo.GetNextSequence();
                repo.Store(User);
            }
            return Ok();
        }

        [Route("api/User")]
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid User id.");

            using (IRedisClient redis = redisClientsManager.GetClient())
            {
                var repo = redis.As<User>();

                if (repo.GetById(id) == null)
                    ModelState.AddModelError("Id", "Unable to find a User with the specified id.");

                if (!ModelState.IsValid)
                    return BadRequest();

                repo.Delete(repo.GetById(id));
                return Ok();
            }
        }

        [Route("api/User")]
        [HttpPut]
        public ActionResult Put(User User)
        {
            if (User == null)
                ModelState.AddModelError("", "Please specify an object of type User.");

            if (User.Id == 0)
                ModelState.AddModelError("Id", "Unable to update a User that doesn't exist yet.");

            if (!ModelState.IsValid)
                return BadRequest();

            using (IRedisClient redis = redisClientsManager.GetClient())
            {
                var repo = redis.As<User>();
                User.Id = repo.GetNextSequence();
                repo.Store(User);
            }
            return Ok();
        }
    }
}