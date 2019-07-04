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

            ApiAuthentication.OutputModel output = new ApiAuthentication.OutputModel();
            if (input.UserName != null)
            {
                output = authIdentity.CheckLogIn(input);
                return Ok(output);
            }
            if (input.UserEmail != null)
            {
                output = authIdentity.CheckLogInByEmail(input);
                return Ok(output);
            }
            if (input.UserPhone != null)
            {
                output = authIdentity.CheckLogInByPhone(input);
                return Ok(output);
            }


            return Ok(output);

        }
    }
}