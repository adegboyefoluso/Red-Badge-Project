using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Model.FamilyModel
{
    public class AddchildtoFamily
    {
        public int ChildId { get; set; }
        public int FamilyId { get; set; }
        public IEnumerable<FamilyListItem> Families { get; set; } = new List<FamilyListItem>();
    }
}
