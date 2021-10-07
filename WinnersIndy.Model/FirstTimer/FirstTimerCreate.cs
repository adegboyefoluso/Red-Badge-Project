using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;

namespace WinnersIndy.Model.FirstTimer
{
    public class FirstTimerCreate
    {
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Purpose Of Visit")]
        public ReasonForVisit PurposeOfVist { get; set; }
        [Display(Name = "Email Address")]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){1,})+)$", ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        [Display(Name = "Date Of Visit")]
        public DateTimeOffset DateOfVisit { get; set; }
    }
}
