using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

using DealerConceptsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DealerConceptsApp.Models;
using Microsoft.Owin.Security;

namespace DealerConceptsApp.Services
{
    public class UserService : BaseService , IUserService
    {
        private IConfigService _config;

        public UserService(IConfigService config)
        {
            _config = config;
        }

        private static ApplicationUserManager GetUserManager()
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        public ApplicationUser GetUserById(string userId)
        {

            ApplicationUserManager userManager = GetUserManager();
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            ApplicationUser user = userManager.FindById(userId);

            return user;
        }

        public IdentityUser GetCurrentUser()
        {
            if (!IsLoggedIn())
                return null;

            string userId = GetCurrentUserId();


            IdentityUser currentUserId = GetUserById(userId);
            return currentUserId;
        }

        public string GetCurrentUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }


        public bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(GetCurrentUserId());

        }

        public IList<string> GetCurrentRoles()
        {
            return GetRoles(GetCurrentUserId());
        }

        public IList<string> GetRoles(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return null;

            }

            ApplicationUserManager userManager = GetUserManager();

            return userManager.GetRoles(userId);
        }
    }
}