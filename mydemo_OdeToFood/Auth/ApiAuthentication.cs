using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace mydemo_OdeToFood.Auth
{
    public class ApiAuthentication : IAuthIdentity
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public ApiAuthentication(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public SignInResult CheckLogIn(string userEmail, string userPassword)
        {
            var result = LoginAsync(userEmail, userPassword);

            return result.Result;
        }

        public IdentityResult CheckRegister(string userEmail, string userPassword)
        {
            var result = RegistrationAsync(userEmail, userPassword);
            return result.Result;
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

        private async Task<IdentityResult> RegistrationAsync(string userEmail, string userPassword)
        {
            var user = new IdentityUser { UserName = userEmail, Email = userEmail };
            var result = await userManager.CreateAsync(user, userPassword);

            return result;
        }

        
    }
}
