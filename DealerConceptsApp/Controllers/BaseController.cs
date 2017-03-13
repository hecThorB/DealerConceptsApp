using DealerConceptsApp.Models.ViewModels;
using DealerConceptsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealerConceptsApp.Controllers
{
    public class BaseController : Controller
    {
        [Microsoft.Practices.Unity.Dependency]
        public IConfigService ConfigService { get; set; }

        [Microsoft.Practices.Unity.Dependency]
        public IUserService UserService { get; set; }


        public new ViewResult View()
        {
            BaseViewModel model = GetViewModel<BaseViewModel>();

            return base.View(model);
        }
        public new ViewResult View(string viewString)
        {
            BaseViewModel model = GetViewModel<BaseViewModel>();

            return base.View(viewString, model);
        }

        protected ViewResult View<T>() where T : BaseViewModel, new()
        {
            T model = GetViewModel<T>();

            return base.View(model);
        }


        protected T GetViewModel<T>() where T : BaseViewModel, new()
        {
            T model = new T();

            model.IsLoggedIn = UserService.IsLoggedIn();
            model.Roles = UserService.GetCurrentRoles();

            if (model.Roles != null)
            {
                model.IsAdmin = model.Roles.Contains("Administrator");
                model.IsKioskMode = model.Roles.Contains("Kiosk");

            }


            model.SiteName = ConfigService.SiteName;

            return model;
        }

    }
}