using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinnersIndy.Model.ChildAttendanceFolder;
using WinnersIndy.Services;

namespace WinnersIndy.Controllers
{
    public class ChildrenClassAttendance : Controller
    {
        // GET: ChildrenClassAttendance
        public ChildrenClassAttendanceServices CreateChildAttendanceService()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var service = new ChildrenClassAttendanceServices(userid);
            return service;

        }
        // GET: ChildAttendance
        public ActionResult Index()
        {
            var sevice = CreateChildAttendanceService();


            return View(sevice.GetChildAttendance());
        }


        //GET:ChildAttendance Create
        //public ActionResult Create(int id)
        //{
        //    var service = CreateChildAttendanceService();
        //    var model = new ChildrenClassAttendanceCreate
        //    {
        //        ChildrenClassId = id,
        //        Attendances = service.GetAttendances(),
        //        AttendanceId = 0,
        //        Children = service.GetChildren(id)



        //    };
        //    return View(model);

        //}
        //POST:ChildAttendance Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChildrenClassAttendanceCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateChildAttendanceService();
            service.CreateChildAttendance(model);
            return RedirectToAction("Details", "ChildrenClass");

        }


    }
}
