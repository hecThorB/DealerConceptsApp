using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DealerConceptsApp.Enums
{
    public enum MessageTypeKind
    {
        Unknown = 0,

        [Display(Name = "Contact Us")]
        ContactUs = 1,
        [Display(Name = "Client Message")]
        ClientMessage = 2,
        [Display(Name = "New Sales Person Registered")]
        NewSalesPersonNotification = 3,
        [Display(Name = "Test Drive Notification")]
        TestDriveNotification = 4,
        [Display(Name = "Extended Test Drive Notification")]
        ExtendedTestDriveNotification = 5,
        [Display(Name = "Feedback Notification")]
        FeedbackNotification = 6
    }
}