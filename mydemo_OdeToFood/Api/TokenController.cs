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
    public class TokenController : ControllerBase
    {
        private readonly OdeToFoodDbContext _Context;
        private readonly IAuthIdentity authIdentity;

        public TokenController(OdeToFoodDbContext _context, IAuthIdentity authIdentity)
        {
            _Context = _context;
            this.authIdentity = authIdentity;
        }

        public class OutputModel
        {
            public string Message { get; set; }
        }

        // Get: api/Token 
        [HttpGet]
        public IActionResult GetToken(string userEmail, string userPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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