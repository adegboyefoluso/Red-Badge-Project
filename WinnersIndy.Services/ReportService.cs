using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;
using WinnersIndy.Model;
using WinnersIndy.Model.ServiceUnit;

namespace WinnersIndy.Services
{
    public class ReportService
    {
        private readonly Guid _userId;
        public ReportService(Guid userid)
        {
            _userId = userid;
        }

        public bool CreateReport(ReportCreate model)
        {
            if(model.File == null)
            {
                return false;
            }
            byte[] bytes = null;
            String FileExt = Path.GetExtension(model.File.FileName).ToUpper();
            if (FileExt == ".PDF")
            {

                if (model.File != null)
                {
                    Stream Fs = model.File.InputStream;
                    BinaryReader Br = new BinaryReader(Fs);
                    bytes = Br.ReadBytes((Int32)Fs.Length);
                }
                var entity = new Report()
                {
                    ReportPeriod = model.ReportPeriod,
                    TypeOfReport = model.TypeOfReport,
                    ServiceUnitId = model.ServiceUnitId,
                    FileContent = bytes,
                    OwnerId = _userId,
                    File=model.FileName
                };
                using (var ctx = new ApplicationDbContext())
                {
                    ctx.Reports.Add(entity);
                    
                    return ctx.SaveChanges() == 1;
                }
            }
            return false;

        }
        public IEnumerable<ReportListItem> GetAllReport()
        {
            using(var ctx= new ApplicationDbContext())
            {
                var reports = ctx.Reports.Select(e => new ReportListItem()
                {
                    Id = e.Id,
                    FileContent = e.FileContent,
                    ReportPeriod = e.ReportPeriod,
                    TypeOfReport = e.TypeOfReport.ToString(),
                    FileName =e.File,
                    ServiceUnit=ctx.ServiceUnits.FirstOrDefault(c=>c.Id==e.ServiceUnitId).Name
                   
                });
                return reports.ToList();
            }

        }
        public bool DeleteReport(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var reports = ctx.Reports.Find(id);
                ctx.Reports.Remove(reports);
                return ctx.SaveChanges() == 1;
            }
        }

        public ReportListItem GetReportById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var reports = ctx.Reports.Find(id);
                return new ReportListItem
                {
                    Id = reports.Id,
                    FileContent = reports.FileContent,
                    ReportPeriod = reports.ReportPeriod,
                    TypeOfReport = reports.TypeOfReport.ToString(),
                    FileName = reports.File,
                    ServiceUnit = ctx.ServiceUnits.FirstOrDefault(c => c.Id == reports.ServiceUnitId).Name
                };
            }
        }


    }
}
