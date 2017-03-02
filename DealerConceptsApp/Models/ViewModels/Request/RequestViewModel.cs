using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Models.ViewModels.Request
{
    public class RequestViewModel : BaseViewModel
    {
        public Dictionary<string, Dictionary<string, object>> RequestStatusType { get; set; }
    }
}