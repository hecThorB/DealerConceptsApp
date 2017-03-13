using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Services.Interfaces
{
    public interface IUserService
    {
        string GetCurrentUserId();

        IdentityUser GetCurrentUser();

        bool IsLoggedIn();

        IList<string> GetCurrentRoles();

        IList<string> GetRoles(string userId);
    }
}