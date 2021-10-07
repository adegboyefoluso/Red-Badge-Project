using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Model.FamilyModel
{
    public class AddParentToFamily
    {
        public int MemberId { get; set; }
        public int FamilyId { get; set; }
        [Display(Name ="Family Name")]
        public string FamilyName { get; set; }
        public IEnumerable<FamilyListItem> Families { get; set; } = new List<FamilyListItem>();
    }
}
