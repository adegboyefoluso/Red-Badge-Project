using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;

namespace WinnersIndy.Model.FirstTimer
{
    public class FirstTimerDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public DateTimeOffset DateOfVisit { get; set; }
        public bool IsConverted { get; set; } = false;
        public ReasonForVisit PurposeOfVisit { get; set; }
    }
}
