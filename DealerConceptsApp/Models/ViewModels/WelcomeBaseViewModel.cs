using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Models.ViewModels
{
    public class WelcomeBaseViewModel : BaseViewModel
    {
        public int EditId { get; set; }

        public bool IsWelcomeMode { get; set; }
    }
}