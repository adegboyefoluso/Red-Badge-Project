using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Model.AttendanceFolder
{
    public class AttendanceList
    {
        public int ChildrenClassId { get; set; }

        //public DateTimeOffset AttendanceDate { get; set; }
        public List<AttendanceSheet> AttendanceSheetList { get; set; }
    }
}
