using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Model.ServiceUnit
{
    public class ServiceUnitListItem
    {
        public int ServiceUnitId { get; set; }
        public string Name { get; set; }
        [Display(Name ="Number Of Member")]
        public int NumberOfMember { get; set; }
       




    }
}
