using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;

namespace WinnersIndy.Model.MemberFolder
{
    public class MemberDetails
    {
        public int MemberId { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:MMM, dd}")]
        public DateTimeOffset DateOfBirth { get; set; }
        public byte[] FileContent { get; set; }
        public string Address { get; set; }
        public string FamilyName { get; set; }

        public  List<MemberServiveUnitListItem> MemberServiceUnits { get; set; }
    }
}
