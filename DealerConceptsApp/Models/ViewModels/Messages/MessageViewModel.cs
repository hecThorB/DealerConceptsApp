using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Models.ViewModels.Messages
{
    public class MessageViewModel : BaseViewModel
    {
        public Dictionary<string, Dictionary<string, object>> StatusTypes { get; set; }

        public Dictionary<string, Dictionary<string, object>> MessageTypes { get; set; }
    }
}