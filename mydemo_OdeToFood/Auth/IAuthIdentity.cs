﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mydemo_OdeToFood.Auth
{
    public interface IAuthIdentity
    {

        SignInResult CheckLogIn(string userEmail, string userPassword);

        ApiAuthentication.OutputModel CheckRegister(ApiAuthentication.InputModel input);

        Task<string> GetAuthTokenAsync();
    }
}
