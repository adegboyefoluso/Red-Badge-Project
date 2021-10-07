using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WinnersIndy.Data;
using WinnersIndy.Model.FamilyModel;

namespace WinnersIndy.Model.MemberFolder
{
    public class MemberCreate
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email Address")]
        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){1,})+)$", ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
        [Display(Name = "Date of Bitrth")]
        public DateTimeOffset DateOfBirth { get; set; }
        public HttpPostedFileBase File { get; set; }
        [Required]
        public string Address { get; set; }
        [Required, Range(1, 3, ErrorMessage = "Select form the List")]
        public Status MaritalStatus { get; set; }
       
        
        [Required, Range(1, 2, ErrorMessage = "Select form the List")]
        public Sex Gender { get; set; }
        public IEnumerable<FamilyListItem> Families { get; set; }
        public int? FamilyId { get; set; }
    }
}
