using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Models.Request
{
    public class MessageQueryRequest
    {
        [Required]
        public string Query { get; set; }
    }
}