using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mydemo_OdeToFood.Auth;
using OdeToFood.Data;

namespace mydemo_OdeToFood.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        private readonly IAuthIdentity authIdentity;

        public RegisterController(IAuthIdentity authIdentity)
        {
            this.authIdentity = authIdentity;
        }

        // Post: api/Register
        [HttpPost]
        [Route("")]
        public IActionResult PostRegister([FromBody] ApiAuthentication.InputModel input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var output = authIdentity.CheckRegister(input);

            foreach (var error in output.identityErrors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return Ok(output);

        }
    }
}