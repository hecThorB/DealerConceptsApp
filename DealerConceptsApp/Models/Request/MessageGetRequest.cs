using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Models.Request
{
    public class MessageGetRequest
    {
        public int? TypeId { get; set; }

        public int? StatusId { get; set; }
    }
}