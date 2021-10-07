//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WinnersIndy.Data;
//using WinnersIndy.Model;

//namespace WinnersIndy.Services
//{
//    public class ReportService
//    {
//        private readonly Guid _userId;
//        public ReportService(Guid userid)
//        {
//            _userId = userid;
//        }

//        public bool CreateReport(ReportCreate model)
//        {
//            byte[] bytes = null;
//            String FileExt = Path.GetExtension(model.File.FileName).ToUpper();
//            if (FileExt == ".PDF")
//            {

//                if (model.File != null)
//                {
//                    Stream Fs = model.File.InputStream;
//                    BinaryReader Br = new BinaryReader(Fs);
//                    bytes = Br.ReadBytes((Int32)Fs.Length);
//                }
//                var entity = new Report()
//                {
//                    ReportPeriod = model.ReportPeriod,
//                    TypeOfReport = model.TypeOfReport,
//                    ServiceUnitId = model.ServiceUnitId,
//                    FileContent = bytes,
//                    OwnerId = _userId
//                };
//                using (var ctx = new ApplicationDbContext())
//                {
//                    ctx.Reports.Add(entity);
//                    return ctx.SaveChanges() == 1;
//                }
//            }
//            return false;

//        }


//    }
//}
