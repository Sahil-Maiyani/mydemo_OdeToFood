using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mydemo_OdeToFood.Auth
{
    public partial class ApiAuthentication
    {
        public class OutputModel
        {
        
            public bool SuccessCode { get; set; }

            public string Message { get; set; }

            public IEnumerable<IdentityError> identityErrors { get; set; }
        }


    }
}
