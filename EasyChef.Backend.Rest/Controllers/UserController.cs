using System.Threading.Tasks;
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

        public UserController(IRequestClient<VerifyLogin, VerifyLoginResponse> requestVerifyLogin)
        {
            _requestVerifyLogin = requestVerifyLogin;
        }

        [Route("VerifyLogin")]
        [HttpPost]
        public async Task<ActionResult> VerifyLogin([FromBody] VerifyLogin verifyLogin)
        {
            var result = await _requestVerifyLogin.Request(verifyLogin);
            return Ok(result.Success);
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

        [Route("api/User")]
        [HttpPost]
        public ActionResult Post(User User)
        {
            if (User == null)
                ModelState.AddModelError("", "Please specify an object of type User.");

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok();
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