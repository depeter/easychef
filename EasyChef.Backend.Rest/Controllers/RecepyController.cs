using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using EasyChef.Backend.Rest.Repositories;
using EasyChef.Contracts.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Recepy = EasyChef.Backend.Rest.Models.Recepy;

namespace EasyChef.Backend.Rest.Controllers
{
    [Produces("application/json")]
    [Route("api/Recepy")]
    public class RecepyController : Controller
    {
        private readonly IRecepyRepo _recepyRepo;
        private readonly IIngredientRepo _ingredientRepo;
        private readonly IRecepyPreparationRepo _recepyPreparationRepo;
        private readonly IMapper _mapper;

        public RecepyController(IRecepyRepo recepyRepo, IIngredientRepo ingredientRepo, IRecepyPreparationRepo recepyPreparationRepo, IMapper mapper)
        {
            _recepyRepo = recepyRepo;
            _ingredientRepo = ingredientRepo;
            _recepyPreparationRepo = recepyPreparationRepo;
            _mapper = mapper;
        }

        [HttpGet("{id:long}")]
        public ActionResult Get(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid Recepy id.");

            if (!ModelState.IsValid)
                return BadRequest();

            var recepy = _recepyRepo.FindBy(x => x.Id == id).SingleOrDefault();

            return Ok(recepy);
        }
        
        [HttpGet]
        public ActionResult List()
        {
            return Ok(_mapper.Map<IList<RecepyDTO>>(_recepyRepo.GetAll()));
        }


        [HttpPost]
        public ActionResult Post([FromBody]RecepyDTO recepy)
        {
            try
            {
                var entity = _mapper.Map<Recepy>(recepy);

                if (recepy == null)
                    ModelState.AddModelError("", "Please specify an object of type RecepyDTO.");

                if (!ModelState.IsValid)
                    return BadRequest();

                foreach(var repprep in entity.RecepyPreparations) {
                    repprep.Recepy = entity;
                    _recepyPreparationRepo.Add(repprep);
                }

                foreach (var ingredient in entity.Ingredients)
                {
                    ingredient.Recepy = entity;
                    _ingredientRepo.Add(ingredient);
                }

                _recepyRepo.Add(entity);
                _recepyRepo.Save();

                return Ok(recepy);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        [HttpDelete("{id:long}")]
        public ActionResult Delete(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid Recepy id.");

            var original = _recepyRepo.FindBy(x => x.Id == id).SingleOrDefault();
            if (original == null)
                ModelState.AddModelError("Id", "Unable to find a Recepy with the specified id.");

            if (!ModelState.IsValid)
                return BadRequest();

            _recepyRepo.Delete(original);
            _recepyRepo.Save();

            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody]RecepyDTO recepy)
        {
            if (recepy == null)
                ModelState.AddModelError("", "Please specify an object of type Recepy.");

            if (recepy?.Id == 0)
                ModelState.AddModelError("Id", "Unable to update a Recepy that doesn't exist yet.");

            if (!ModelState.IsValid)
                return BadRequest();

            var original = _recepyRepo.FindBy(x => x.Id == recepy.Id).SingleOrDefault();
            if (original == null)
                return BadRequest("Item no longer exists");

            original.Description = recepy.Description;
            original.Base64Image = recepy.Base64Image;
            original.Title = recepy.Title;
            
            _recepyRepo.Edit(original);
            _recepyRepo.Save();

            return Ok();
        }
    }
}