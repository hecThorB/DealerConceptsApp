using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DealerConceptsApp.Enums
{
    public enum MessageStatusKind
    {
        Unknown = 0,

        [Display(Name = "Read")]
        Unread = 1,
        [Display(Name = "Unread")]
        Read = 2,
        [Display(Name = "Important")]
        Important = 3
    }
}