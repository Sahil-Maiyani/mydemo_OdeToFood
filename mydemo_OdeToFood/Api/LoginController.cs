using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mydemo_OdeToFood.Auth;

namespace mydemo_OdeToFood.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthIdentity authIdentity;

        public LoginController(IAuthIdentity authIdentity)
        {
            this.authIdentity = authIdentity;
        }

        //Post: api/Login
        [HttpPost]
        public IActionResult PostLogin([FromBody]  ApiAuthentication.InputModel input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var output = authIdentity.CheckLogInByEmail(input);

            return Ok(output);

        }
    }
}