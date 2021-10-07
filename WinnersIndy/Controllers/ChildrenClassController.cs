using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinnersIndy.Model.ChildrenClassFolder;
using WinnersIndy.Services;

namespace WinnersIndy.Controllers
{
    public class ChildrenClassController : Controller
    {
        public ChildrenClassServices CreateChidrenClassService()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var model = new ChildrenClassServices(userid);
            return model;
        }
        // GET: ChildrenClass
        public ActionResult Index()
        {
            var services = CreateChidrenClassService();
            var model = services.GetAllChildrenClass();
            return View(model);


        }
        // GET: ChildrenClass/Create
        public ActionResult Create()
        {
            return View();

        }
        // GET: ChildrenClass/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChildrenClassCreate model)
        {
            if (!ModelState.IsValid) return View();
            var services = CreateChidrenClassService();

            if (services.CreateChildrenClass(model))
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        //GET: ChildrenClass/{id}

        public ActionResult Details(int id)
        {
            var services = CreateChidrenClassService();
            return View(services.GetListofChildrenInClass(id));
        }
        public ActionResult Edit(int id)
        {
            var services = CreateChidrenClassService();
            return View(services.GetChildrenClassById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChildrenClassEdit model)
        {
            if (model.ChildrenClassId != model.ChildrenClassId)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (!ModelState.IsValid) return View(model);
            var services = CreateChidrenClassService();
            if (services.UpdateChildrenClass(model))
            {
                TempData["SaveResult"] = "Your Note was Updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var services = CreateChidrenClassService();
            return View(services.GetChildrenClassById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var services = CreateChidrenClassService();
            services.DeleteChildrenClass(id);
            return RedirectToAction("Index");
        }


        //public ActionResult AddChildToClass()
        //{
        //    var service = CreateChidrenClassService();

        //    return View(service.GetClasses());
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public ActionResult AddChildToClass(AddChildtoClass model)
        //{
        //    var services = CreateChidrenClassService();
        //    services.AddChildrentoclass(model);
        //    return RedirectToAction("Index", "ChildrenClass");
        //}


        public ActionResult ClassDetails(int id)
        {
            var services = CreateChidrenClassService();
            services.GetListofChildrenInClass(id);
            return View(services.GetListofChildrenInClass(id));
        }
    }
}
