using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinnersIndy.Services;
using PagedList;
using WinnersIndy.Model.MemberFolder;
using WinnersIndy.Model.Contact;

namespace WinnersIndy.Controllers
{
    public class ContactController : Controller
    {
        [Authorize]
        private ContactService CrteateContactService()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var service = new ContactService(userid);
            return service;
        }
        private MemberServices CrteateMemberService()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var services = new MemberServices(userid);
            return services;
        }
        // GET: Contact
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var service = CrteateContactService();
            var memlist = service.GetAllContact();
            if (!String.IsNullOrEmpty(searchString))
            {
                memlist = memlist.Where(s => s.LastName.Contains(searchString.Substring(0, 1).ToUpper() + searchString.Substring(1))
                                       || s.FirstName.Contains(searchString.Substring(0, 1).ToUpper() + searchString.Substring(1)));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    memlist = memlist.OrderByDescending(s => s.LastName);
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
            var ser = CrteateMemberService();

            
            ContactCreate model = new ContactCreate();
            List<MemberListItem> MemberItem = ser.GetMembers().ToList();
            model.Memberss= new SelectList(MemberItem, "MemberId", "FullName");
            //MemberListItem Members = new MemberListItem() { MemberId = 0, LastName = "Select Member" };
            //MemberItem.Add(Members);
            //ViewBag.MyList = new SelectList(MemberItem.OrderBy(x => x.MemberId).ToList(), "MemberId", "FullName");
            return View(model);

        }

        //POST:Contact/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactCreate model)
        {
            var ser = CrteateMemberService();
            List<MemberListItem> MemberItem = ser.GetMembers().ToList();
            model.Memberss = new SelectList(MemberItem, "MemberId", "FullName");
            if (!ModelState.IsValid) return View(model);
            var service = CrteateContactService();
            //if (model.MemberId == 0)
            //{
            //    model.MemberId = null;
            //}
            if (service.CreateContact(model))
            {
                TempData["SaveResult"] = "Contact added to the church Directory";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Contact could not be added");
            return View(model);
        }


        public ActionResult Delete(int id)
        {
            var services = CrteateContactService();
            var model = services.GetContactById(id);
            return View(model);

        }
        //GET :Member/Delete/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var services = CrteateContactService();
            services.DeleteContact(id);
            TempData["SaveResult"] = "Family  was deleted ";
            return RedirectToAction("Index");
        }

    }
}