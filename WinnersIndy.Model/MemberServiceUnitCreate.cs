using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WinnersIndy.Model.ServiceUnit;

namespace WinnersIndy.Model
{
    public class MemberServiceUnitCreate
    {
        public int MemberId { get; set; }
        public int ServiceUnitId { get; set; }
        public string Name { get; set; }

        public List<ServiceUnitListItem> ServiceUnits { get; set; }
    }
}
