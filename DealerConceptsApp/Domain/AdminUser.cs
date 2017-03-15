using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Domain
{
    public class AdminUser
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsBlocked { get; set; }

        public string RoleId { get; set; }
    }
}