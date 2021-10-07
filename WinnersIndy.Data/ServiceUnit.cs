using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Data
{
    public class ServiceUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid Ownerid { get; set; }
        public virtual List<MemberServiceUnit> MemberServiceUnits { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
