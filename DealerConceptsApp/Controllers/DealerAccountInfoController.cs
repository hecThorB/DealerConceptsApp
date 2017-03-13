using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealerConceptsApp.Controllers
{
    [RoutePrefix("dealers")]
    public class DealerAccountInfoController : BaseController
    {
        // GET: DealerAccountInfo
        [Route("create")]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [Route("password")]
        public ActionResult CreatePassword()
        {
            return View();
        }
    }
}