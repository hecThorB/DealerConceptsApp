using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Models.Request
{
    public class DealerAccountInfoCreateRequest
    {
        [Required, StringLength(128)]
        public string DealerName { get; set; }

        [Required, StringLength(128)]
        public string FirstName { get; set; }

        [Required, StringLength(128)]
        public string LastName { get; set; }

        [Required]
        public int Title { get; set; }

        [Required, StringLength(128)]
        public string Email { get; set; }

        [Required, StringLength(128)]
        public string Password { get; set; }

        public bool IsDeleted { get; set; }
    }
}