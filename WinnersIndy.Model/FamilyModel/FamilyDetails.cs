using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Model.FamilyModel
{
    public class FamilyDetails
    {
        public int FamilyId { get; set; }
        public DateTime? AniversaryDate { get; set; }
        public string FamilyName { get; set; }

        public List<PersonDetails> FamilyMembers { get; set; }
    }
}
