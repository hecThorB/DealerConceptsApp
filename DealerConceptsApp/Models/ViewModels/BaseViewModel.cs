using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Models.ViewModels
{
    public class BaseViewModel
    {
        public bool IsAdmin { get; internal set; }

        public bool IsLoggedIn { get; set; }

        public bool IsKioskMode { get; internal set; }

        public IList<string> Roles { get; internal set; }

        public string SiteName { get; internal set;}

        public string PageName { get; set; }
    }
}