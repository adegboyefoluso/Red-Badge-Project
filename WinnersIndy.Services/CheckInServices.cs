using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WinnersIndy.Data;
using WinnersIndy.Model.FamilyModel;

namespace WinnersIndy.Services
{
    public class CheckInServices
    {
        private readonly Guid _userId;
        public CheckInServices(Guid userid)
        {
            _userId = userid;
        }

        /// <summary>
        /// Check to see if the is an  exixsting checking for this memeber,
        /// if there isn't  any , then we go ahead and check in the member.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CreateCheckIN (CheckInCreate model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var checking = ctx.CheckIns.SingleOrDefault(e => e.CheckInDate == DateTime.Today && e.MemberId == model.MemberId);
                if(checking is null)
                {
                    var entity = new CheckIn()
                    {
                        CheckInDate = DateTime.Today,
                        MemberId = model.MemberId

                    };
                    ctx.CheckIns.Add(entity);
                    return ctx.SaveChanges() == 1;
                }
                return false;
            }
            
        }

        public CheckInDetails PrintCheckin(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var checking = ctx.CheckIns.FirstOrDefault(e => e.MemberId == Id&&e.CheckInDate==DateTime.Today);
                if (checking is null) return null;
                return new CheckInDetails
                {
                    FirstName = checking.Member.FirstName,
                    LastName=checking.Member.LastName,
                    CheckinDate = checking.CheckInDate.ToString("D"),
                     Id="WIN "+checking.Id
                    

                };
            }
        }

    //    public string generateBarcode()
    //    {
    //        try
    //        {
    //            string[] charPool = "1-2-3-4-5-6-7-8-9-0".Split('-');
    //            StringBuilder rs = new StringBuilder();
    //            int length = 6;
    //            Random rnd = new Random();
    //            while (length-- > 0)
    //            {
    //                int index = (int)(rnd.NextDouble() * charPool.Length);
    //                if (charPool[index] != "-")
    //                {
    //                    rs.Append(charPool[index]);
    //                    charPool[index] = "-";
    //                }
    //                else
    //                    length++;
    //            }
    //            return rs.ToString();
    //        }
    //        catch (Exception ex)
    //        {
    //            //ErrorLog.WriteErrorLog("Barcode", ex.ToString(), ex.Message);
    //        }
    //        return "";
    //    }

    //    public Byte[] getBarcodeImage(string barcode, string file)
    //    {
    //        try
    //        {
    //            Barcode39 _barcode = new Barcode39();
    //            int barSize = 16;
    //            string fontFile = HttpContext.Current.Server.MapPath("~/fonts/FREE3OF9.TTF");
    //            return _barcode.(barcode, barSize, true, file, fontFile);
    //        }
    //        catch (Exception ex)
    //        {
    //            //ErrorLog.WriteErrorLog("Barcode", ex.ToString(), ex.Message);
    //        }
    //        return null;
    //    }

    }
}
