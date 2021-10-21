using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Data
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [ForeignKey(nameof(Member))]
        public int? MemberId { get; set; }

        public virtual Member Member { get; set; }
        public bool Converted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public Guid OwnerId { get; set; }
        
    }
}
