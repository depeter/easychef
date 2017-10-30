using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EasyChef.Backend.Rest.Repositories;
using Microsoft.AspNetCore.Mvc;
using Category = EasyChef.Backend.Rest.Models.Category;
using EasyChef.Contracts.Shared.Models;

namespace EasyChef.Backend.Rest.Controllers
{
    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepo categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
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

        [HttpGet]
        public ActionResult List(bool withProductsOnly = true)
        {
            if(withProductsOnly)
                return Ok(_mapper.Map<IList<CategoryDTO>>(_categoryRepo.GetAllWithProducts()));

            return Ok(_mapper.Map<IList<CategoryDTO>>(_categoryRepo.GetAll()));
        }


        [HttpPost]
        public ActionResult Post([FromBody]CategoryDTO category)
        {
            var entity = _mapper.Map<Category>(category);

            if (category == null)
                ModelState.AddModelError("", "Please specify an object of type CategoryDTO.");

            if (!ModelState.IsValid)
                return BadRequest();

            _categoryRepo.Add(entity);
            _categoryRepo.Save();

            return Ok(_mapper.Map<CategoryDTO>(entity));
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
        public ActionResult Put([FromBody]CategoryDTO category)
        {
            if (category == null)
                ModelState.AddModelError("", "Please specify an object of type Category.");

            if (category?.Id == 0)
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
            original.Children = _mapper.Map<IList<Category>>(category.Children);

            _categoryRepo.Edit(original);
            _categoryRepo.Save();

            return Ok();
        }
    }
}