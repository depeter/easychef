using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EasyChef.Backend.Rest.Repositories;
using Microsoft.AspNetCore.Mvc;
using Product = EasyChef.Backend.Rest.Models.Product;
using EasyChef.Contracts.Shared.Models;

namespace EasyChef.Backend.Rest.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;

        public ProductController(IProductRepo productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [HttpGet("{id:long}")]
        public ActionResult Get(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid Product id.");

            if (!ModelState.IsValid)
                return BadRequest();

            var product = _productRepo.FindBy(x => x.Id == id).SingleOrDefault();

            return Ok(product);
        }

        [HttpGet]
        public ActionResult List()
        {
            return Ok(_mapper.Map<IList<ProductDTO>>(_productRepo.GetAll()));
        }


        [HttpPost]
        public ActionResult Post([FromBody]ProductDTO product)
        {
            var entity = _mapper.Map<Product>(product);

            if (product == null)
                ModelState.AddModelError("", "Please specify an object of type ProductDTO.");

            if (!ModelState.IsValid)
                return BadRequest();

            _productRepo.Add(entity);
            _productRepo.Save();

            return Ok(_mapper.Map<ProductDTO>(entity));
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid Product id.");

            var original = _productRepo.FindBy(x => x.Id == id).SingleOrDefault();
            if (original == null)
                ModelState.AddModelError("Id", "Unable to find a Product with the specified id.");

            if (!ModelState.IsValid)
                return BadRequest();

            _productRepo.Delete(original);
            _productRepo.Save();

            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody]ProductDTO product)
        {
            if (product == null)
                ModelState.AddModelError("", "Please specify an object of type Product.");

            if (product?.Id == 0)
                ModelState.AddModelError("Id", "Unable to update a Product that doesn't exist yet.");

            if (!ModelState.IsValid)
                return BadRequest();

            var original = _productRepo.FindBy(x => x.Id == product.Id).SingleOrDefault();
            if (original == null)
                return BadRequest("Item no longer exists");

            original.Description = product.Description;
            original.Name = product.Name;
            original.Price = product.Price;
            original.SKU = product.SKU;
            original.Weight = product.Weight;

            _productRepo.Edit(original);
            _productRepo.Save();

            return Ok();
        }
    }
}