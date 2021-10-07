using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Data
{
    public class MemberServiceUnit
    {
        public int Id { get; set; }
        [ ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        [ ForeignKey(nameof(ServiceUnit))]
        public int ServiceUnitId { get; set; }
        public virtual Member Member { get; set; }
        public  virtual ServiceUnit ServiceUnit { get; set; }
    }
}
