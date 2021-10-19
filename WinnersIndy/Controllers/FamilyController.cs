using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinnersIndy.Model.FamilyModel;
using WinnersIndy.Services;

namespace WinnersIndy.Controllers
{
    public class FamilyController : Controller
    {
        public FamilyServices CreateFamilyService()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var model = new FamilyServices(userid);
            return model;
        }
        // GET: Family
        public ActionResult Index()
        {
            var services = CreateFamilyService();

            return View(services.GetFamilies());
        }
        //GET:Family/Create
        public ActionResult Create()
        {
            return View();
        }
        //POST:Family/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FamilyCreate model)
        {
            if (!ModelState.IsValid) return View();
            var services = CreateFamilyService();

            if (services.CreateFamily(model))
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        ////GET:AddChild
        //public ActionResult AddChildToFamily(int id)
        //{
        //    var service = CreateFamilyService();
        //    var model = new AddchildtoFamily
        //    {
        //        ChildId = id,
        //        Families = service.GetFamilies(),
        //        FamilyId=0


        //    };
        //    return View(model);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public ActionResult AddChildToFamily(AddchildtoFamily model)
        //{
        //    var services = CreateFamilyService();
        //    if (services.AddchildrenFamily(model))
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return View(model);
        //}

        public ActionResult AddParentToFamily(int id)
        {
            var service = CreateFamilyService();
            var model = new AddParentToFamily
            {
                MemberId = id,
                Families = service.GetFamilies(),
                FamilyId = 0


            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddParentToFamily(AddParentToFamily model)
        {
            var services = CreateFamilyService();
            if (services.AddParentstoFamily(model))
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public ActionResult GetFamilyMember(int id)
        {
            var services = CreateFamilyService();
            return View(services.GetFamilyById(id));
        }

        // GET :Family/Delete{Id}
        public ActionResult Delete(int id)
        {
            var services = CreateFamilyService();
            var model = services.GetFamilyById(id);
            return View(model);

        }
        //GET :Family/Delete/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var services = CreateFamilyService();
            services.DeleteFamily(id);
            TempData["SaveResult"] = "Family  was deleted ";
            return RedirectToAction("Index");
        }
        
        [HttpGet]

        public ActionResult GetFamilyCheckIn(string PhoneNumber)
        {
            var service = CreateFamilyService();
            if (service.CheckInSheet(PhoneNumber) is null) return HttpNotFound();
            return View(service.CheckInSheet(PhoneNumber));
        }
    }
}
