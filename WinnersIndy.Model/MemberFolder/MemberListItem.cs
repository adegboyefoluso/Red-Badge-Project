using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Model.MemberFolder
{
    public class MemberListItem
    {
        
        public int MemberId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM, dd}")]
        [Display(Name ="Date Of Birth")]
        public DateTimeOffset DateOfBirth { get; set; }
        public int Age
        {
            get
            {
                int age = DateTime.Now.Year - DateOfBirth.Year;
                return age;
            }
        }
        public string FullName { get; set; }
       
    }
}
