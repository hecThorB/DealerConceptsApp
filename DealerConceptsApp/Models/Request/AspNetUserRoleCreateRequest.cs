using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Models.Request
{
    public class AspNetUserRoleCreateRequest
    {
        public bool IsSalesPerson { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}