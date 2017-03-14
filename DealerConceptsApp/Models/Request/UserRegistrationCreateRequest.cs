using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DealerConceptsApp.Models.Request
{
    public class UserRegistrationCreateRequest
    {
        [Required, EmailAddress, StringLength(256)]
        public string Email { get; set; }

        [Required, StringLength(256)]
        public string Password { get; set; }
    }
}