using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealerConceptsApp.Controllers
{
    [RoutePrefix("dealers")]
    public class DealerAccountInfoController : Controller
    {
        // GET: DealerAccountInfo
        [Route("create")]
        public ActionResult CreateAccount()
        {
            return View();
        }
    }
}