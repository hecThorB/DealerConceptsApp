using DealerConceptsApp.Enums;
using System;
using System.Collections.Generic;
using DealerConceptsApp.Models.ViewModels;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealerConceptsApp.Controllers
{
    [RoutePrefix("dynamic/js")][AllowAnonymous]
    public class JsBuilderController : BaseController
    {
        private static string KIND_FORMAT = "{0}Kind";

        [Route("dealers/create")]
        public ActionResult DealerRegistration()
        {
            DealerConceptsApp.Models.ViewModels.JscriptViewModel model = new DealerConceptsApp.Models.ViewModels.JscriptViewModel();
            var enumList = new
            {
                //Look at how the different Methods output the JS objects
                AdminByName = typeof(AdminTitleKind).EnumByDisplay(1)
                ,
               DealerByName = typeof(DealershipKind).EnumByDisplay(1)

            };
            Response.ContentType = "application/javascript";
            model.Name = String.Format(KIND_FORMAT, "dealerRegistration"); //"ExampleNameKinds";
            model.Kinds = enumList;
            return View("Dynamic", model);
        }

        protected override PartialViewResult PartialView(string viewName, object model)
        {
            Response.ContentType = "application/javascript";

            return base.PartialView(viewName, model);
        }
    }
}