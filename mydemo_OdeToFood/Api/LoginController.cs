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

        public class OutputModel
        {
            public string Message { get; set; }
        }

        //Get: api/Login
        [HttpGet]
        public IActionResult GetLogin(string userEmail, string userPassword)
        {
            var result = authIdentity.CheckLogIn(userEmail, userPassword);

            if (result.Succeeded)
            {
                return Ok(new OutputModel { Message = "Login Success" });
            }
            if (result.RequiresTwoFactor)
            {
                return Ok(new OutputModel { Message = "Login required two step auth" });
            }
            if (result.IsLockedOut)
            {
                return Ok(new OutputModel { Message = "Account Locked" });
            }
            else
            {
                return Ok(new OutputModel { Message = "Invalid Login Attampt" });
            }
        }
    }
}