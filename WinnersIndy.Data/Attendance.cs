using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Data
{
    public class Attendance
    {
        public int AttendanceId { get; set; }

        [ForeignKey(nameof(ChildrenClass))]
        public int? ChildrenClassId { get; set; }


        public virtual ChildrenClass ChildrenClass { get; set; }

        public ICollection<ChildrenClassAttendance> ChildrenAttendance { get; set; }
        public virtual ChildrenClassAttendance ChildrenClassAttendance { get; set; }

        [Display(Name = "Attendance Date")]
        [DisplayFormat(DataFormatString = "{0:MMM, dd, yyyy}")]
        //[DataType(DataType.DateTime.ToString("d"))]
        public DateTimeOffset AttendanceDate { get; set; }

    }
}
