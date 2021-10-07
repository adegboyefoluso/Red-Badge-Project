using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Model.AttendanceFolder
{
    public class ChildrenClassAttendanceListItem
    {
        public int MemberId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "In Church")]
        public bool InChurch { get; set; }
        
        public DateTimeOffset AttendanceDate { get; set; }
    }

}
