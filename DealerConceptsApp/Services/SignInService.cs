using DealerConceptsApp.Domain;
using DealerConceptsApp.Models;
using DealerConceptsApp.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace DealerConceptsApp.Services
{
    public class SignInService : BaseService, ISignInService
    {
        private IDealerAccountInfoService _dealerAccountInfoService;

        public SignInService(IDealerAccountInfoService dealerAccountInfoService)
        {
            _dealerAccountInfoService = dealerAccountInfoService;
        }

        private static ApplicationUserManager GetUserManager()
        {
            return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }


        public bool Signin(string emailaddress, string password, bool allowUnconfirmed = false)
        {
            bool result = false;

            DealerAccountInfo accountInfo = new DealerAccountInfo();

            ApplicationUserManager userManager = GetUserManager();
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            ApplicationUser user = userManager.Find(emailaddress, password);

            if (user != null && (allowUnconfirmed || user.EmailConfirmed))
            {

                accountInfo = _dealerAccountInfoService.SelectByEmail(emailaddress);

                if (accountInfo == null || !accountInfo.IsBlocked)
                {
                    ClaimsIdentity signin = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, signin);
                    result = true;
                }

                else

                {
                    throw new Exception("You are blocked");

                }


            }

            else if (user == null)
            {
                throw new Exception("Invalid credentials");
            }

            else if (!user.EmailConfirmed)
            {
                throw new Exception("Please confirm your email address");
            }

            return result;
        }
    }
}