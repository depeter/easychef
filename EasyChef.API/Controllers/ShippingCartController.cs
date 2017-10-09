using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyChef.Shared.Models;

namespace EasyChef.API.Controllers
{
    [Produces("application/json")]
    [Route("api/ShippingCart")]
    public class ShippingCartController : Controller
    {
        public ShippingCartController()
        {

        }

        [HttpGet]
        public ShoppingCart Get(long userId)
        {

        }

        [HttpPost]
        public HttpResponse Post(ShoppingCart shoppingCart)
        {
            //Thread-safe client factory
            var redisManager = new BasicRedisClientManager("localhost:6379");

            redisManager.ExecAs<Todo>(redisTodos => {
                var todo = new Todo
                {
                    Id = redisTodos.GetNextSequence(),
                    Content = "Learn Redis",
                    Order = 1,
                };

                redisTodos.Store(todo);

                Todo savedTodo = redisTodos.GetById(todo.Id);
                savedTodo.Done = true;

                redisTodos.Store(savedTodo);

                redisTodos.DeleteById(savedTodo.Id);

                var allTodos = redisTodos.GetAll();

                Assert.That(allTodos.Count, Is.EqualTo(0));
            });
        }
    }
}