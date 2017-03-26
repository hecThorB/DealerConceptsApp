namespace DealerConceptsApp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DealerConceptsApp.Models.ApplicationDbContext>
    {
        protected override void Seed(DealerConceptsApp.Models.ApplicationDbContext context)
        {
            CreateRole(context, RoleNames.Administrator);
            CreateRole(context, RoleNames.SalesPerson);

            string[] adminRoles = new string[] { RoleNames.Administrator };
            string[] userRoles = new string[] { RoleNames.SalesPerson };

            CreateUser(context, "DealerConceptsInbox@mailinator.com", adminRoles);
            CreateUser(context, "DealerConceptsUser@mailinator.com", userRoles);

        }


        private static void CreateRole(ApplicationDbContext context, string roleName)
        {
            if (!context.Roles.Any(r => r.Name == roleName))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = roleName };

                manager.Create(role);
            }
        }

        private static void CreateUser(ApplicationDbContext context, string userNameEmail, string[] roles)
        {
            if (!context.Users.Any(u => u.UserName == userNameEmail))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = userNameEmail,
                    Email = userNameEmail,
                    LockoutEnabled = false
                };

                manager.Create(user, "Dealerpass1!");
                manager.AddToRoles(user.Id, roles);
            }
        }
    }
}