using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WinnersIndy.Model.MemberFolder
{
    public class MemberEdit
    {
        public int MemberId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,10}$", ErrorMessage = "Please enter valid phone no.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email Address")]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){1,})+)$", ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
        [Display(Name = "Date Of Birth")]
        public DateTimeOffset DateOfBirth { get; set; }
        public HttpPostedFileBase File { get; set; }
        [Display(Name = "Image")]
        public byte[] FileContent { get; set; }
        public string Address { get; set; }
    }
}
