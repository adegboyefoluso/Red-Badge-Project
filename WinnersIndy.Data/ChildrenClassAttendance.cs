using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Data
{
    public class ChildrenClassAttendance
    {
        [Key, Column(Order = 0), ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }


        [Key, Column(Order = 1), ForeignKey(nameof(Attendance))]
        public int AttendanceId { get; set; }
        public virtual Attendance Attendance { get; set; }


        public bool InChurch { get; set; }

    }
}
