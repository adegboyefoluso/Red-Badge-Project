using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Model.ChildAttendanceFolder
{
    public class ChildrenClassAttendanceCreate
    {

        public int PersonId { get; set; }
        public int AttendanceId { get; set; }
        //public int ChildrenClassId { get; set; }
        public bool InChurch { get; set; }
        //public List<Child> Children { get; set; }
        //public IList<Attendance> Attendances { get; set; }
    }
}
