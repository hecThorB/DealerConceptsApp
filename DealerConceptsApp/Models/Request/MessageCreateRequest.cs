using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DealerConceptsApp.Services.Interfaces;

namespace DealerConceptsApp.Models.Request
{
    public class MessageCreateRequest : IMessage
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string ToAddress { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string FromAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }
    }
}