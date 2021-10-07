using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Data
{


    public enum Sex
    {
        Male = 1,
        Female
    }

    public enum Status
    {
        Single = 1,
        Married,
        Others
    }
    

    public class Member
    {
        public int MemberId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTimeOffset DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
        [Required, Range(1, 2, ErrorMessage = "Select form the List")]
        public Sex Gender { get; set; }

        [ForeignKey(nameof(Family))]
        public int? FamilyId { get; set; }
        public virtual Family Family { get; set; }

        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        [Required, Range(1, 3, ErrorMessage = "Select form the List")]
        public Status MaritalStatus { get; set; }
        
        public Guid OwnerId { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset ModifiedUtc { get; set; }
        public int Age
        {
            get
            {
                int age = DateTime.Now.Year - DateOfBirth.Year;
                return age;
            }
        }
        [ForeignKey(nameof(ChildrenClass))]
        public int? ChildrenClassId { get; set; }
        public virtual ChildrenClass ChildrenClass { get; set; }

        public bool IsActive { get; set; } = true;

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public virtual List<MemberServiceUnit> MemberServiceUnits { get; set; }

        public virtual List<Contact> Contacts { get; set; } = new List<Contact>();

    }

}
