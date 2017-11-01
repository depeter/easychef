using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EasyChef.Backend.Rest.Models;
using EasyChef.Backend.Rest.Repositories;
using EasyChef.Contracts.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using EasyChef.Shared.Models;
using Microsoft.EntityFrameworkCore;
using ShoppingCart = EasyChef.Backend.Rest.Models.ShoppingCart;

namespace EasyChef.API.Controllers
{
    [Produces("application/json")]
    [Route("api/ShoppingCart")]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepo _shoppingCartRepo;
        private readonly IShoppingCartProductRepo _shoppingCartProductRepo;
        private readonly IMapper _mapper;

        public ShoppingCartController(IShoppingCartRepo shoppingCartRepo, IShoppingCartProductRepo shoppingCartProductRepo, IMapper mapper)
        {
            _shoppingCartRepo = shoppingCartRepo;
            _shoppingCartProductRepo = shoppingCartProductRepo;
            _mapper = mapper;
        }

        [HttpGet("{userId:long}")]
        [Route("GetByUser/{userId}")]
        public ActionResult GetByUser(long userId)
        {
            if (userId <= 0)
                ModelState.AddModelError("", "Please specify a valid user id.");

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_mapper.Map<ShoppingCartDTO>(_shoppingCartRepo.FindBy(x => x.UserId == userId).Include("ShoppingCartProducts").SingleOrDefault()));
        }

        [HttpGet("{id:long}")]
        public ActionResult Get(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid ShoppingCart id.");

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_mapper.Map<ShoppingCartDTO>(_shoppingCartRepo.FindBy(x => x.Id == id).Include("ShoppingCartProducts").SingleOrDefault()));
        }

        [HttpPost]
        public ActionResult Post([FromBody]ShoppingCartDTO shoppingCart)
        {
            if (shoppingCart == null)
                ModelState.AddModelError("", "Please specify an object of type ShoppingCart.");

            if (!ModelState.IsValid)
                return BadRequest();

            var entity = _mapper.Map<ShoppingCart>(shoppingCart);

            var products = entity.ShoppingCartProducts;

            entity.ShoppingCartProducts = new List<ShoppingCartProduct>();
            _shoppingCartRepo.Add(entity);
            _shoppingCartRepo.Save();

            foreach (var shoppingCartProduct in products)
            {
                shoppingCartProduct.ShoppingCartId = entity.Id;
                _shoppingCartProductRepo.Add(shoppingCartProduct);
            }
            _shoppingCartProductRepo.Save();

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
        public ActionResult Put([FromBody]ShoppingCartDTO shoppingCart)
        {
            if (shoppingCart == null)
                ModelState.AddModelError("", "Please specify an object of type ShoppingCart.");
            
            if (shoppingCart?.Id == null || shoppingCart.Id == 0)
                ModelState.AddModelError("Id", "Unable to update a ShoppingCart that doesn't exist yet.");
            
            if(!ModelState.IsValid)
                return BadRequest();

            var original = _shoppingCartRepo.FindBy(x => x.Id == shoppingCart.Id).Include("ShoppingCartProducts").FirstOrDefault();
            if(original == null) { 
                ModelState.AddModelError("Id", "Unable to update a ShoppingCart that doesn't exist yet.");
                return BadRequest();
            }

            // remove original products
            foreach(var scp in original.ShoppingCartProducts)
                _shoppingCartProductRepo.Delete(scp);

            _shoppingCartProductRepo.Save();

            original.ShoppingCartProducts = new List<ShoppingCartProduct>();

            // add new products
            foreach(var scp in shoppingCart.ShoppingCartProducts)
            {
                var entscp = _mapper.Map<ShoppingCartProduct>(scp);
                entscp.ShoppingCartId = original.Id;
                original.ShoppingCartProducts.Add(entscp);
            }

            _shoppingCartRepo.Save();

            return Ok(original);
        }
    }
}