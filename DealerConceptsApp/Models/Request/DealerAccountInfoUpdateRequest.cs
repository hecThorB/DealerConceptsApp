using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Models.Request
{
    public class DealerAccountInfoUpdateRequest : DealerAccountInfoCreateRequest
    {
        [Required]
        public int Id { get; set; }
    }
}