using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EasyChef.Shared.Models;
using EasyChef.Backend.Rest.Repositories;

namespace EasyChef.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private ICategoryRepo _categoryRepo;

        public CategoryController(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet("{id:long}")]
        public ActionResult Get(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid Category id.");

            if (!ModelState.IsValid)
                return BadRequest();

            var category = _categoryRepo.FindBy(x => x.Id == id).SingleOrDefault();

            return Ok(category);
        }

        [HttpPost]
        public ActionResult Post([FromBody]Category category)
        {
            if (category == null)
                ModelState.AddModelError("", "Please specify an object of type Category.");

            if (!ModelState.IsValid)
                return BadRequest();

            _categoryRepo.Add(category);
            _categoryRepo.Save();

            return Ok(category);
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid Category id.");

            var original = _categoryRepo.FindBy(x => x.Id == id).SingleOrDefault();
            if (original == null)
                ModelState.AddModelError("Id", "Unable to find a Category with the specified id.");

            if (!ModelState.IsValid)
                return BadRequest();

            _categoryRepo.Delete(original);
            _categoryRepo.Save();

            return Ok();
        }

        [HttpPut]
        public ActionResult Put(Category category)
        {
            if (category == null)
                ModelState.AddModelError("", "Please specify an object of type Category.");

            if (category.Id == 0)
                ModelState.AddModelError("Id", "Unable to update a Category that doesn't exist yet.");

            if (!ModelState.IsValid)
                return BadRequest();

            var original = _categoryRepo.FindBy(x => x.Id == category.Id).SingleOrDefault();
            if (original == null)
                return BadRequest("Item no longer exists");

            original.Link = category.Link;
            original.Name = category.Name;
            //original.Parent = category.Parent;
            original.HasProducts = category.HasProducts;

            _categoryRepo.Edit(original);
            _categoryRepo.Save();

            return Ok();
        }
    }
}