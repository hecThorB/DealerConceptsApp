using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Models.Request
{
    public class AdminUserUpdateRequest
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; }
    }
}