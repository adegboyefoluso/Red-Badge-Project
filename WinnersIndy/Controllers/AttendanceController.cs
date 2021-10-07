using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinnersIndy.Model.AttendanceFolder;
using WinnersIndy.Services;

namespace WinnersIndy.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance
        public AttendanceServices CreateAttendanceservices()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var services = new AttendanceServices(userid);
            return services;
        }
        // GET: Attendance
        //public ActionResult Index()
        //{
        //    var services = CreateAttendanceservices();
        //    return View(services.GetAllAttendance());
        //}


        public ActionResult Create(int id)
        {
            ViewBag.Date = DateTime.Today.ToString("D");
            var service = CreateAttendanceservices();
            var model = new AttendanceList()
            {
                ChildrenClassId = id,
                AttendanceSheetList = service.GetAttendanceList(id)
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AttendanceList model)

        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //if (model. != DateTime.Today || model.AttendanceDate==null)
            //{
                
            //    return View(model);
            //}
            if (model.AttendanceSheetList == null)
            {
                return View(model);
            }
            var service = CreateAttendanceservices();
            // model.AttendanceSheetList = service.GetAttendanceList(model.ChildrenClassId);
            if (service.CreateAttendance(model))
            {
                TempData["SaveResult"] = "Attendance Information succesfully Added";
                return RedirectToAction("Details", "ChildrenClass", new { id = model.ChildrenClassId });
            }
            ModelState.AddModelError("", "Record already exist");
            return View(model);
        }

        //GET :Attendance
        public ActionResult GetClassAttendance(int id)
        {
           
            var service = CreateAttendanceservices();
            ViewBag.Date = service.GetDate(id).AttendanceDate.ToString("D");
            
            return View(service.GetClassAttendance(id));
        }

        public ActionResult GetAttendanceDate(int id)
        {
            var service = CreateAttendanceservices();
            return View(service.GetAttendanceDate(id));
        }

        
    }
}
