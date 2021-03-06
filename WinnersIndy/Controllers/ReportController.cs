using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinnersIndy.Model;
using WinnersIndy.Model.ServiceUnit;
using WinnersIndy.Services;

namespace WinnersIndy.Controllers
{
    public class ReportController : Controller
    {
        [Authorize]
        private ReportService CreateReportService()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var service = new ReportService(userid);
            return service;
        }
        private ServiceUnitService CreateServiceUnit()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var service = new ServiceUnitService(userid);
            return service;
        }
        // GET: Report
        public ActionResult Index()
        {
            var service = CreateReportService();
            return View(service.GetAllReport());
        }

        //Post:Report/Get
        public ActionResult Create()
        {
            ReportCreate model = new ReportCreate();
            var service = CreateServiceUnit();

            List<ServiceUnitListItem> serviceUnits = service.GetAllServiceUnit().ToList();
            model.ServiceUnits = new SelectList(serviceUnits, "ServiceUnitId", "Name");
            //ViewBag.MyList = new SelectList(ServiceUnits.OrderBy(x => x.ServiceUnitId).ToList(), "ServiceUnitId", "Name");

            return View(model);

        }
        //POST:Report/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReportCreate model)
        {
            
            var service = CreateServiceUnit();
            List<ServiceUnitListItem> serviceUnits = service.GetAllServiceUnit().ToList();
            model.ServiceUnits = new SelectList(serviceUnits, "ServiceUnitId", "Name");
            if (!ModelState.IsValid) return View(model);
            var services = CreateReportService();

            
            if (services.CreateReport(model))
            {
                TempData["SaveResult"] = "For God is not unjust so as to overlook your work and the love that you have shown for his name in serving the saints,\n" +
                    " as you still do. And we desire each one of you to show the same earnestness to have the full assurance of hope until the end,\n" +
                    " so that you may not be sluggish, but imitators of those who through faith and patience inherit the promises. ..... Heb 6:10-12                .\n" +
                     "           May the God Reward your Service  in His Vineyard and bless your according to His riches in glory\n" +
                    "May He makes his light to shine on you\n" +
                    "An remember all you service ";
                return RedirectToAction("SubmitMessage" ,"Report");
            }
            ModelState.AddModelError("", "Pdf Format only");
            return View(model);
        }

        //Method that is called  in the filedetails/Index view to download the pdf
        [HttpGet]
        public FileResult DownLoadFile(int id)

        {
            var service = CreateReportService();
            IEnumerable<ReportListItem> ObjFiles = service.GetAllReport();

            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(id)
                            select new { FC.FileName, FC.FileContent }).ToList().FirstOrDefault();

            return File(FileById.FileContent, "application/pdf", FileById.FileName);
            

        }

        //An index  list of  all report
        [HttpGet]
        public ActionResult FileDetails()
        {
            var service = CreateReportService();
            IEnumerable<ReportListItem> DetList = service.GetAllReport();

            return View("FileDetails", DetList);
        }


        [HttpGet]
        public ActionResult SubmitMessage() // An Empty action to be displayed  after a report is submitted
        {
            return View();
        }


        [HttpGet]
        public ActionResult Message() // An empty Action that  display Delete Message
        {
            return View();
        }

        //A method to delete a report without displaying a view for the delete.
        [HttpGet]
        public ActionResult DeleteReport(int id)
        {
            var service = CreateReportService();
            if (service.DeleteReport(id))
            {
                TempData["SaveResult"] = "Report Deleted Suuccesfully";
                return RedirectToAction("Message", "Report");
            }
            return RedirectToAction("Message", "Report");

        }

        
    }
}