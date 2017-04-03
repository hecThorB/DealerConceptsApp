using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealerConceptsApp.Models.ViewModels;

namespace DealerConceptsApp.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("public")]
    public class PublicController : BaseController
    {
        ////I still need to create views for these commented views below
        //#region - Main/Info/Contact - 
        //[Route("~/")]
        //public ActionResult HomePage()
        //{
        //    return View();
        //}

        //[Route("~/contactus")]
        //public ActionResult ContactUs()
        //{
        //    return View();
        //}

        //[Route("~/aboutus")]
        //public ActionResult AboutUs()
        //{
        //    return View();
        //}

        //[Route("~/faqs")]
        //public ActionResult FAQs()
        //{
        //    return View();
        //}

        //[Route("~/howitworks")]
        //public ActionResult HowitWorks()
        //{
        //    return View();
        //}

        //#endregion

        #region - Users -
        [Route("~/register")]
        public ActionResult CreateUser()
        {
            return View();
        }

        //[Route("~/login")]
        //public ActionResult ConsumerLogin(string returnUrl = null)
        //{
        //    LoginViewModel model = GetViewModel<LoginViewModel>();
        //    model.ReturnUrl = returnUrl;

        //    return View(model);
        //}

        //[Route("~/recover/{token:Guid}")]
        //public ActionResult ConsumerRecoverPassword(Guid token)
        //{
        //    ItemViewModel<Guid> message = new ItemViewModel<Guid>();
        //    message.Item = token;
        //    return View(message);
        //}

        //[Route("~/confirm/{token:Guid}")]
        //public ActionResult Confirm(Guid token)
        //{
        //    ItemViewModel<Guid> message = new ItemViewModel<Guid>();
        //    message.Item = token;
        //    return View(message);
        //}

        //[Route("~/recover")]
        //public ActionResult ConsumerRecoverEmail()
        //{
        //    return View();
        //}
        #endregion
    }
}