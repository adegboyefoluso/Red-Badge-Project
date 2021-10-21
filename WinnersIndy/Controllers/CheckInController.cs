using Microsoft.AspNet.Identity;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinnersIndy.Model.FamilyModel;
using WinnersIndy.Services;

namespace WinnersIndy.Controllers
{
    public class CheckInController : Controller
    {

        [Authorize]
        private CheckInServices CreateCheckInServices()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var service = new CheckInServices(userid);
            return service;
        }

        private MemberServices CreateMemberServices()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var ser = new MemberServices(userid);
            return ser;
        }
        // GET: CheckIn
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            var ser = CreateMemberServices();
           var member= ser.GetMemberById(id);
            ViewBag.firstName = member.FirstName;
            ViewBag.LastName = member.LastName;
            CheckInCreate create = new CheckInCreate();
            create.MemberId = id;

            return View(create);
        }
        [HttpPost]
        public ActionResult Create (CheckInCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var services = CreateCheckInServices();
            if (!services.CreateCheckIN(model))
            {
                ModelState.AddModelError("", "Child already checked in for today");
                return View(model);
            }
            return RedirectToAction("Index", "Home");

        }

        public ActionResult PrintCheckIn(int id)
        {
            var services = CreateCheckInServices();
           services.PrintCheckin(id);
            
            return new ViewAsPdf(services.PrintCheckin(id));
            
        }

        public ActionResult PrintCheckinAsPDF(int id)
        {
            return new ViewAsPdf("PrintCheckIn",new { id = id }) { FileName = "Test.pdf" };
            
        }

    }
}