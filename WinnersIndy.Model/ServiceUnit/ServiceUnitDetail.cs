using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;
using WinnersIndy.Model.MemberFolder;

namespace WinnersIndy.Model.ServiceUnit
{
    public class ServiceUnitDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MemberListItem> Members { get; set; }
        public int NumberOfMember { get; set; }
    }
}
