using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DealerConceptsApp.Enums
{
    public enum EmailTemplateKind
    {
        Unknown = 0,
        [Display(Name = "Customer went on test drive")]
        TestDrive = 1,
        [Display(Name = "Customer plans to test drive more than one vehicle")]
        MultipleTestDrivesIntended = 2,
        [Display(Name = "Customer would like to go on a 30 min test drive")]
        LongTestDriveRequest = 3,
        [Display(Name = "Customer did not complete test drive request")]
        TestDriveOrderNotComplete = 4,
        [Display(Name = "Customer feedback about test drive")]
        TestDriveFeedback = 5,
    }
}