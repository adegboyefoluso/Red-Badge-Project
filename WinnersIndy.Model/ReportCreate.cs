using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WinnersIndy.Data;
using WinnersIndy.Model.ServiceUnit;

namespace WinnersIndy.Model
{
    public class ReportCreate
    {
        public DateTime ReportPeriod { get; set; }
        public int ServiceUnitId { get; set; }
        public SelectList ServiceUnits { get; set; }
        [Required, Range(1, 2, ErrorMessage = "Select form the List")]
        public ReportType TypeOfReport { get; set; }
       
        public HttpPostedFileBase File { get; set; }
        public string FileName { get; set; }
        //public List<ServiceUnitListItem> ServicUnits { get; set; }

        
    }
}
