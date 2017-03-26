using DealerConceptsApp.Models;
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

        bool Logout();

        IList<string> GetCurrentRoles();

        IList<string> GetRoles(string userId);

        IdentityUser CreateUser(string email, string password, string roleName = "Sales Person");

        bool ChangePassword(string userId, string newPassword);

        bool IsValidToken(Guid token);

        string UpdateConfirmation(Guid token);

        ApplicationUser GetUser(string emailaddress);

        ApplicationUser UpdateUserById(string userId);
    }
}