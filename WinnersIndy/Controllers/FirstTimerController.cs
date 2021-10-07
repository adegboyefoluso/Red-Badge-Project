using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinnersIndy.Services;
using PagedList;
using WinnersIndy.Model.FirstTimer;

namespace WinnersIndy.Controllers
{
    public class FirstTimerController : Controller
    {
        [Authorize]
        private FirstTimerServices CrteateFirstTimerService()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var service = new FirstTimerServices(userid);
            return service;
        }
        // GET: FirstTimer
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var service = CrteateFirstTimerService();
            var memlist = service.GetAllFirstTimer();
            if (!String.IsNullOrEmpty(searchString))
            {
                memlist = memlist.Where(s => s.LastName.Contains(searchString.Substring(0, 1).ToUpper() + searchString.Substring(1))
                                       || s.FirstName.Contains(searchString.Substring(0, 1).ToUpper() + searchString.Substring(1)) || s.DateOfVisit.ToString("d").Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    memlist = memlist.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    memlist = memlist.OrderBy(s => s.DateOfVisit);
                    break;
                case "date_desc":
                    memlist = memlist.OrderByDescending(s => s.DateOfVisit);
                    break;

                default:
                    memlist = memlist.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(memlist.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FirstTimerCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CrteateFirstTimerService();

            if (service.CreateFirstTimer(model))
            {
                TempData["SaveResult"] = "Fist Timer added to the church Directory";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "First Timer could not be added");
            return View(model);
        }

        // List of FirstTimer That have  are now Members
        [HttpGet]       
        public ActionResult ConvertedFirstTimer(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var service = CrteateFirstTimerService();
            var memlist = service.GetFirstTimerWhoAreMembers();
            if (!String.IsNullOrEmpty(searchString))
            {
                memlist = memlist.Where(s => s.LastName.Contains(searchString.Substring(0, 1).ToUpper() + searchString.Substring(1))
                                       || s.FirstName.Contains(searchString.Substring(0, 1).ToUpper() + searchString.Substring(1)) || s.DateOfVisit.ToString("Y").Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    memlist = memlist.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    memlist = memlist.OrderBy(s => s.DateOfVisit);
                    break;
                case "date_desc":
                    memlist = memlist.OrderByDescending(s => s.DateOfVisit);
                    break;

                default:
                    memlist = memlist.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(memlist.ToPagedList(pageNumber, pageSize));
        }
    }
}