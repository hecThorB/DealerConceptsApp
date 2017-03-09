using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Enums
{
    public enum AdminTitleKind
    {
        Unknown = 0,
        [Display(Name = "Owner")]
        Owner = 1,
        [Display(Name = "General Manager")]
        GeneralManager = 2,
        [Display(Name = "Manager")]
        Manager = 3,
        [Display(Name = "Other Authorized Personnel")]
        OtherAuthorizedPersonnel = 4
    }
}