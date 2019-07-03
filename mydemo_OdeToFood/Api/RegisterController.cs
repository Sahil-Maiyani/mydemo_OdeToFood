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

        public class OutputModel
        {
            public string Message { get; set; }
        }

        // Get: api/Register
        [HttpGet]
        public IActionResult GetRegister(string userEmail, string userPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = authIdentity.CheckRegister(userEmail, userPassword);

            if (result.Succeeded)
            {
                return Ok(new OutputModel { Message = "Registration Success" });
            }
            else
            {
                return Ok(new OutputModel { Message = "Failed" });
            }


        }
    }
}