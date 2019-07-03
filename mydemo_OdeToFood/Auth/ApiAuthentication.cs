using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace mydemo_OdeToFood.Auth
{
    public partial class ApiAuthentication : IAuthIdentity
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public InputModel Input { get; set; }
        public OutputModel Output { get; set; }

        public ApiAuthentication(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;

            Output = new OutputModel();
        }

        public SignInResult CheckLogIn(string userEmail, string userPassword)
        {
            var result = LoginAsync(userEmail, userPassword);

            return result.Result;
        }

        public OutputModel CheckRegister(InputModel input)
        {
            Input = input;
            var result = RegistrationAsync().Result;
            Output.SuccessCode = result.Succeeded;

            if (result.Succeeded)
            {
                Output.Message = "New User Login Sucess";
            }
            Output.identityErrors = result.Errors;

            return Output;
        }

        public Task<string> GetAuthTokenAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<SignInResult> LoginAsync(string userEmail, string userPassword)
        {
            var result = await signInManager.PasswordSignInAsync(userEmail, userPassword, true, lockoutOnFailure: true);

            return result;
        }

        private async Task<IdentityResult> RegistrationAsync()
        {
            var user = new IdentityUser { UserName = Input.UserName, Email = Input.UserEmail };
            var result = await userManager.CreateAsync(user, Input.UserPassword);

            return result;
        }


    }
}
