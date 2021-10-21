using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Model.FamilyModel
{
    public class FamilyCheckIn
    {
        public int MemberId { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool InChurch { get; set; } = false;

    }
}
