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
        private IMessageService _messageService;
        private static Dictionary<string, string> _roles;

        public UserService(IConfigService config, IMessageService messageService)
        {
            _config = config;
            _messageService = messageService;
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

        public bool Logout()
        {
            bool result = false;

            IdentityUser user = GetCurrentUser();

            if (user != null)
            {
                IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                result = true;
            }

            return result;
        }

        public Dictionary<string, string> Roles
        {
            get
            {
                if (_roles == null)
                {
                    // not bothering to lock shared resource - not a big cost

                    var context = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
                    var rolesSet = context.Set<IdentityRole>();
                    _roles = rolesSet.ToDictionary(r => r.Name, r => r.Id);
                }
                return _roles;
            }
        }

        public IdentityUser CreateUser(string email, string password, string roleName = "Consumer")
        {
            ApplicationUserManager userManager = GetUserManager();

            ApplicationUser newUser = new ApplicationUser { UserName = email, Email = email, LockoutEnabled = false };

            if (roleName != null)
            {
                IdentityUserRole role = new IdentityUserRole();

                string roleId = Roles[roleName];

                role.RoleId = roleId;
                role.UserId = newUser.Id;
                newUser.Roles.Add(role);
            }



            IdentityResult result = null;

            try
            {
                result = userManager.Create(newUser, password);

            }
            catch (Exception e)
            {
                throw e;
            }

            if (result.Succeeded)
            {


                /*-----NEW TEST DRIVE EMAIL TO ADMIN------*/
                MessageCreateRequest newUserAdminMsg = new MessageCreateRequest();

                newUserAdminMsg.ToAddress = _config.SiteAdminEmail;
                newUserAdminMsg.Subject = "You have a new Found!t Customer";

                _messageService.NewCustomerToAdmin(newUserAdminMsg, email);


                /*-----CONFRIMATION EMAIL-----*/
                SendConfirmation(newUser);

                return newUser;
            }
            else
            {
                throw new IdentityResultException(result);
            }
        }
    }
}