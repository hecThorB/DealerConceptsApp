using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Models.Request
{
    public class UserPasswordRecover : UserPasswordRequest
    {
        public Guid Token { get; set; }
    }
}