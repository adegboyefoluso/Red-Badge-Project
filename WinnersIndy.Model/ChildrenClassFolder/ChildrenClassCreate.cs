using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Model.ChildrenClassFolder
{
    public class ChildrenClassCreate
    {
        [Display(Name = "Class Name")]
        public string Name { get; set; }
        [Display(Name = "Class Description")]
        public string Description { get; set; }
    }
}
