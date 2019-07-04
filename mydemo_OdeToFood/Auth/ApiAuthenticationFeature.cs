using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

// This class inherits from `ApiAuthentication` Base class and implement additional feature authentication using email and phone 

namespace mydemo_OdeToFood.Auth
{
    public class ApiAuthenticationFeature : ApiAuthentication, IAuthIdentity
    {
        private readonly string LOGIN_EMAIL_FAILED = "Can not find user from given Email.";
        private readonly string LOGIN_PHONE_FAILED = "Can not find user from given Phone.";

        public ApiAuthenticationFeature(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) : base(signInManager, userManager)
        {
        }

        public OutputModel CheckLogInByEmail(InputModel input)
        {
            Input = input;
            var user = userManager.FindByEmailAsync(Input.UserEmail).Result;

            if (user == null)
            {
                Output.SuccessCode = false;
                Output.Message = LOGIN_EMAIL_FAILED;

                return Output;
            }

            var result = LoginAsync(user).Result;

            MakeLoginOutput(result);

            return Output;


        }

        public OutputModel CheckLogInByPhone(InputModel input)
        {
            Input = input;
            var currentUser = userManager.Users.Where(u => u.PhoneNumber.Equals(input.UserPhone)).SingleOrDefault();

            if (currentUser == null)
            {
                Output.SuccessCode = false;
                Output.Message = LOGIN_PHONE_FAILED;

                return Output;
            }
            var result = LoginAsync(currentUser).Result;

            MakeLoginOutput(result);

            return Output;


        }

        private async Task<SignInResult> LoginAsync(IdentityUser user)
        {
            var result = await signInManager.PasswordSignInAsync(user, Input.UserPassword, true, lockoutOnFailure: true);

            return result;
        }
    }
}
