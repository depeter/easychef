using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyChef.Backend.Rest.Repositories;
using EasyChef.Contracts.Shared.Models;
using EasyChef.Contracts.Shared.RequestResponse;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using User = EasyChef.Backend.Rest.Models.User;

namespace EasyChef.Backend.Rest.Controllers
{

    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IRequestClient<VerifyLogin, VerifyLoginResponse> _requestVerifyLogin;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserController(IRequestClient<VerifyLogin, VerifyLoginResponse> requestVerifyLogin, IUserRepo userRepo, IMapper mapper)
        {
            _requestVerifyLogin = requestVerifyLogin;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        [Route("VerifyLogin")]
        [HttpPost]
        public async Task<ActionResult> VerifyLogin([FromBody] VerifyLogin verifyLogin)
        {
            var result = await _requestVerifyLogin.Request(verifyLogin);
            if (!result.Success)
                return Ok(false);

            var existingUser = _userRepo.FindBy(x => x.Email == verifyLogin.Login);
            if (existingUser == null)
            {
                _userRepo.Add(new User()
                {
                    Email = verifyLogin.Login,
                    Password = verifyLogin.Password
                });
                _userRepo.Save();
            }

            return Ok(true);
        }

        [Route("api/User/{id:long}")]
        [HttpGet]
        public ActionResult Get(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid User id.");

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok();
        }

        [Route("Save")]
        [HttpPost]
        public ActionResult Post([FromBody] UserDTO user)
        {
            if (user == null)
                ModelState.AddModelError("", "Please specify an object of type User.");

            if (!ModelState.IsValid)
                return BadRequest();

            var original = _userRepo.FindBy(x => x.Email.ToLower() == user.Email.ToLower()).FirstOrDefault();
            if (original != null)
                return Ok(original.Id);

            var entity = _mapper.Map<User>(user);
            _userRepo.Add(entity);
            _userRepo.Save();
            return Ok(entity.Id);
        }

        [Route("api/User")]
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            if (id <= 0)
                ModelState.AddModelError("", "Please specify a valid User id.");

            return Ok();
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

            return Ok();
        }
    }
}