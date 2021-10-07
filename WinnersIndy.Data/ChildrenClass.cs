using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Data
{
    public class ChildrenClass
    {
        [Key]
        public int ChildrenClassId { get; set; }
        [Required]
        [Display(Name = "Class Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Age Category")]
        public string Description { get; set; }

        public Guid OwenerId { get; set; }
        public virtual ICollection<Member> Children { get; set; }
        public ICollection<Attendance> Attendances { get; set; }

    }
}
