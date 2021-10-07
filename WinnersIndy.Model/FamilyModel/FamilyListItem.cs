using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;

namespace WinnersIndy.Model.FamilyModel
{
    public class FamilyListItem
    {
        public int FamilyId { get; set; }
        [Display(Name ="Anivessary Date")]
        [DisplayFormat(DataFormatString = "{0:MMM, dd}")]
        public DateTime? AniversaryDate { get; set; }
        [Display(Name = "Family Name")]
        public string FamilyName { get; set; }

        public virtual ICollection<Member> Parent { get; set; }
    }
}
