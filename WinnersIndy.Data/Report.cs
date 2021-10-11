using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Data
{
    public enum ReportType
    {
        MinuteOfMeeting=1,
        UnitReport,
    }
    public class Report
    {
        public int Id { get; set; }
        public DateTime ReportPeriod { get; set; }
        public int ServiceUnitId { get; set; }
        public  virtual ServiceUnit ServiceUnit { get; set; }
        public ReportType TypeOfReport { get; set; }
        public string File { get; set; }
        public byte[] FileContent { get; set; }
        public Guid OwnerId { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }

    }
}
