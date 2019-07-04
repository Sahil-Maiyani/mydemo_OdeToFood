using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace mydemo_OdeToFood.Auth
{
    public partial class ApiAuthentication : IAuthIdentity
    {
        protected readonly SignInManager<IdentityUser> signInManager;
        protected readonly UserManager<IdentityUser> userManager;

        private readonly string LOGIN_SUCCESS = "User login success. :)";
        private readonly string LOGIN_2FA = "User login required two step authentication.";
        private readonly string LOGIN_LOCKED = "User login account is locked.";
        private readonly string LOGIN_FAILED = "User login Failed.";

        protected InputModel Input { get; set; }
        protected OutputModel Output { get; set; }

        public ApiAuthentication(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;

            Output = new OutputModel();
        }

        public OutputModel CheckLogIn(InputModel input)
        {
            Input = input;
            var result = LoginAsync().Result;
            MakeLoginOutput(result);
            return Output;
        }

        protected void MakeLoginOutput(SignInResult result)
        {
            if (result.Succeeded)
            {
                Output.SuccessCode = result.Succeeded;
                Output.Message = LOGIN_SUCCESS;

            }
            else if (result.RequiresTwoFactor)
            {
                Output.SuccessCode = result.RequiresTwoFactor;
                Output.Message = LOGIN_2FA;
            }
            else if (result.IsLockedOut)
            {
                Output.SuccessCode = result.IsLockedOut;
                Output.Message = LOGIN_LOCKED;
            }
            else
            {
                Output.SuccessCode = false;
                Output.Message = LOGIN_FAILED;
            }
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

        private async Task<SignInResult> LoginAsync()
        {
            var result = await signInManager.PasswordSignInAsync(Input.UserName, Input.UserPassword, true, lockoutOnFailure: true);

            return result;
        }

        private async Task<IdentityResult> RegistrationAsync()
        {
            var user = new IdentityUser { UserName = Input.UserName, Email = Input.UserEmail };
            var result = await userManager.CreateAsync(user, Input.UserPassword);

            return result;
        }

        public OutputModel CheckLogInByEmail(InputModel input)
        {
            throw new NotImplementedException();
        }
    }
}
