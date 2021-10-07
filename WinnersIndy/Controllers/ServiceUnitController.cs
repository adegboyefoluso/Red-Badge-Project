using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinnersIndy.Model.ServiceUnit;
using WinnersIndy.Services;

namespace WinnersIndy.Controllers
{
    public class ServiceUnitController : Controller
    {
        [Authorize]
        private ServiceUnitService CrteateServiceUnit()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var service = new ServiceUnitService(userid);
            return service;
        }
        // GET: ServiceUnit
        public ActionResult Index()
        {
            var service = CrteateServiceUnit();
            return View(service.GetAllServiceUnit());
        }

        public ActionResult Create()
        {
            return View();
        }
        //POST:Family/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceUnitCreate model)
        {
            if (!ModelState.IsValid) return View();
            var services = CrteateServiceUnit();

            if (services.CreateServiceUnit(model))
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var service = CrteateServiceUnit();
            return View(service.GetServiceUnitById(id));
        }

        // GET :Member/Delete{Id}
        public ActionResult Delete(int id)
        {
            var service = CrteateServiceUnit();
            var model = service.GetServiceUnitById(id);
            return View(model);

        }
        //GET :Member/Delete/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var service = CrteateServiceUnit();
            service.DeleteServiceUnit(id);
            TempData["SaveResult"] = "Unit  was deleted ";
            return RedirectToAction("Index");
        }


    }
}