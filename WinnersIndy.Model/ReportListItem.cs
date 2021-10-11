using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;

namespace WinnersIndy.Model
{
    public class ReportListItem
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM, dd,yyyy}")]
        [Display(Name = "Report Date")]
        public DateTime ReportPeriod { get; set; }
        [Display(Name = "Service Unit")]
        public string ServiceUnit { get; set; }
        [Display(Name = "Type of Report")]
        public string TypeOfReport { get; set; }
        public byte[] FileContent { get; set; }
        [Display(Name = "Document Name")]
        public string FileName { get; set; }
    }
}
