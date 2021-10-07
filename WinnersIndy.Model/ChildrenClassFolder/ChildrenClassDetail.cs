using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Model.ChildrenClassFolder
{
    public class ChildrenClassDetail
    {
        public int ChildrenClassId { get; set; }
        public string ChildrenClass { get; set; }
        public string Description { get; set; }
        
        //public int ChildId { get; set; }
        public IEnumerable<ChildrenClassChildDetail> ChildDetails { get; set; }
    }
}
