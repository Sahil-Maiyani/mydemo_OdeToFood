using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mydemo_OdeToFood.Auth
{
    public interface IAuthIdentity
    {

        ApiAuthentication.OutputModel CheckLogIn(ApiAuthentication.InputModel input);

        ApiAuthentication.OutputModel NewRegister(ApiAuthentication.InputModel input);

        ApiAuthentication.OutputModel CheckLogInByEmail(ApiAuthentication.InputModel input);

        ApiAuthentication.OutputModel CheckLogInByPhone(ApiAuthentication.InputModel input);

        //Task<string> GetAuthTokenAsync();
    }
}
