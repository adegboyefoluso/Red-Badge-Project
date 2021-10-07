using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Model.AttendanceFolder
{
   public class AttendanceListItem
    {
        [DisplayFormat(DataFormatString = "{0:MMM, dd, yyyy}")]
        public DateTimeOffset AttendanceDate { get; set; }


        public IEnumerable<ChildrenClassAttendanceListItem> ListChildrenClassAttendanceListItem { get; set; }
    }
}
