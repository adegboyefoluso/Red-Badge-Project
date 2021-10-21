using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnersIndy.Data
{
    public class CheckIn
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        public bool InChurch { get; set; } = true;
        //public byte[] BarcodeImage { get; set; }
        //public string Barcode { get; set; }
        //public string ImageUrl { get; set; }



    }
}
