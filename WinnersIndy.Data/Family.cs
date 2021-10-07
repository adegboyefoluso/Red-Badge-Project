using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Data
{
    public class Family
    {
        public int FamilyId { get; set; }

        [Display(Name = "Anniversary Date")]

        public DateTime? AniversaryDate { get; set; }

        [Required]
        [Display(Name = "Family Name ")]
        public string FamilyName { get; set; }
        public Guid OwnerId { get; set; }
        public virtual ICollection<Member> FamilyMember { get; set; }
        public bool IsActive { get; set; } = true;


    }
}
