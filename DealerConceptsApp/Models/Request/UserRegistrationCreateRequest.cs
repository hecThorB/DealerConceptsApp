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

        [Required, StringLength(128)]
        public string DealerName { get; set; }

        [Required]
        public int DealerType { get; set; }

        [Required, StringLength(128)]
        public string FirstName { get; set; }

        [Required, StringLength(128)]
        public string LastName { get; set; }

        [Required]
        public int Title { get; set; }

        [Required, StringLength(128)]
        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; }
    }
}