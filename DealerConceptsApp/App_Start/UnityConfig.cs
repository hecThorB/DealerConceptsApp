using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using DealerConceptsApp.Services.Interfaces;
using DealerConceptsApp.Services;
using System.Web.Http;

namespace DealerConceptsApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IConfigService, ConfigService>();

            container.RegisterType<IMessageService, MessageService>();

            container.RegisterType<IDealerAccountInfoService, DealerAccountInfoService>();

            container.RegisterType<IUserService, UserService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}