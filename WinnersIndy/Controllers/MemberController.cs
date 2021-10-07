using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinnersIndy.Model.MemberFolder;
using WinnersIndy.Services;
using System.Net;
using System.Net.Mail;
using WinnersIndy.Common;
using WinnersIndy.Model.FamilyModel;
using PagedList;
using WinnersIndy.Model;
using WinnersIndy.Model.ServiceUnit;

namespace WinnersIndy.Controllers
{
    public class MemberController : Controller
    {
        [Authorize]
        private MemberServices CrteateMemberService()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var service = new MemberServices(userid);
            return service;
        }
        //This is to enable access to family list  and to be able to select family when adding  member
        private FamilyServices CreateFamilyService()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var service = new FamilyServices(userid);
            return service;
        }

        // GET: Member
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
            var service = CrteateMemberService();
            var memlist = service.GetMembers();
            if (!String.IsNullOrEmpty(searchString))
            {
                memlist = memlist.Where(s => s.LastName.Contains(searchString.Substring(0, 1).ToUpper()+searchString.Substring(1))
                                       || s.FirstName.Contains(searchString.Substring(0, 1).ToUpper() + searchString.Substring(1)));
            }
            switch (sortOrder)
            {
                case "name_desc":
                   memlist = memlist.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    memlist = memlist.OrderBy(s => s.DateOfBirth);
                    break;
                case "date_desc":
                    memlist = memlist.OrderByDescending(s => s.DateOfBirth);
                    break;
                default:
                    memlist = memlist.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(memlist.ToPagedList(pageNumber,pageSize));
        }
        //GET:Member/create
        public ActionResult Create()
        {
            var service = CreateFamilyService();

            MemberCreate model = new MemberCreate();
           //List<SelectListItem>  Items = new SelectList(service.GetFamilies(), "FamilyId", "FamilyName",model.FamilyId).ToList();
           // Items.Insert(0, (new SelectListItem { Text = "SelectFamily", Value = "0" }));
           // ViewBag.MyList = Items;
            List<FamilyListItem> FamilyItem = service.GetFamilies().ToList();
            FamilyListItem family = new FamilyListItem() { FamilyId = 0, FamilyName = "Select Family" };
            FamilyItem.Add(family);
            ViewBag.MyList = new SelectList( FamilyItem.OrderBy(x=>x.FamilyId).ToList(),"FamilyId","FamilyName");
            return View();
            
        }
        //POST:Member/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CrteateMemberService();
            if (model.FamilyId == 0)
            {
                model.FamilyId = null;
            }
            if (service.CreateMember(model))
            {
                TempData["SaveResult"] = "Member added to the church Directory";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Member could not be added");
            return View(model);
        }
        //=======================Get Member Details=============
        public ActionResult Details(int Id)
        {

            var service = CrteateMemberService();
            MemberDetails model = service.GetMemberById(Id);
            return View(model);
        }
        //GET:Restaurant/Edit
        public ActionResult Edit(int id)
        {
            
            var service = CrteateMemberService();
            var model = service.GetMemberById(id);
            var modelupdate = new MemberEdit()
            {
                MemberId = model.MemberId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth,
                EmailAddress = model.EmailAddress,
                FileContent = model.FileContent,



            };
            return View(modelupdate);
        }
        //===================Edit Members================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberEdit model, int id)
        {

            if (!ModelState.IsValid) return View(model);
            if (model.MemberId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var services = CrteateMemberService();
            if (services.UpdateMember(model))
            {
                TempData["SaveResult"] = "Member's Information updated succesfully.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Update Failed.");
            return View(model);
        }
        // GET :Member/Delete{Id}
        public ActionResult Delete(int id)
        {
            var sevice = CrteateMemberService();
            var model = sevice.GetMemberById(id);
            return View(model);

        }
        //GET :Member/Delete/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            var sevice = CrteateMemberService();
            sevice.DeleteMember(id);
            TempData["SaveResult"] = "Member  was deleted ";
            return RedirectToAction("Index");
        }

        //GET:GetChildren
        public ActionResult GetChildren(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AgeSortParm = sortOrder == "Age" ? "Age_desc" : "Age";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var service = CrteateMemberService();
            var children = service.GetChildren();

            if (!String.IsNullOrEmpty(searchString))
            {
                children = children.Where(s => s.LastName.Contains(searchString.Substring(0, 1).ToUpper() + searchString.Substring(1))
                                       || s.FirstName.Contains(searchString.Substring(0, 1).ToUpper() + searchString.Substring(1)));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    children = children.OrderByDescending(s => s.LastName);
                    break;
                case "Age":
                    children = children.OrderBy(s => s.Age);
                    break;
                case "Age_desc":
                    children = children.OrderByDescending(s => s.Age);
                    break;
                default:
                    children = children.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(children.ToPagedList(pageNumber,pageSize));

        }

        public ActionResult SendBulkEmail(Email model)
        {

            return RedirectToAction("Email");
        }

        //GET:AddChildToClass

        public ActionResult AddChildToClass(int id)
        {
            var service = CrteateMemberService();
            var model = new AddChild
            {
                MemberId = id,
                ChildrenClasses = service.GetDropdown(),
            };
            return View(model);
        }
        //POST:AddChildToClass

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddChildToClass(AddChild model)
        {
            if (!ModelState.IsValid) return View();
            var services = CrteateMemberService();
            if (services.AddChildtoclass(model))
            {
                TempData["SaveResult"] = "Child Succsfully Added to Class ";
                return RedirectToAction("Index", "ChildrenClass");
            }

            ModelState.AddModelError("", "Child Could not be Added");

            return View(model);
        }

        //Remove A child from Class
        public ActionResult RemoveChild(int id)
        {
            var sevice = CrteateMemberService();
            var model = sevice.GetMemberById(id);
            return View(model);

        }
        //GET :CHildren/Delete/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("RemoveChild")]
        public ActionResult RemoveChldFromClass(int id)
        {
            var sevice = CrteateMemberService();
            sevice.DeleteChildFromClass(id);
            TempData["SaveResult"] = "Student removed from Class";
            return RedirectToAction("Index");
        }





        //GET:GetTeachers
        //public ActionResult GetTeachers()
        //{
        //    var services = CrteateMemberService();
        //    return View(services.GetTeachers());
        //}

        ////GET:AddTeacherToClass
        //public ActionResult AddTeacherToClass(int id)
        //{
        //    var service = CrteateMemberService();
        //    var model = new AddChild
        //    {
        //        MemberId = id,
        //        ChildrenClasses = service.GetDropdown(),
        //    };
        //    return View(model);
        //}
        //POST:AddTeacherToClass

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddTeacherToClass(AddChild model)
        {
            if (!ModelState.IsValid) return View();
            var services = CrteateMemberService();
            if (services.AddChildtoclass(model))
            {
                TempData["SaveResult"] = "Teacher Succsfully Added to Class ";
                return RedirectToAction("Index", "ChildrenClass");
            }

            ModelState.AddModelError("", "Teacher Could not be Added");

            return View(model);
        }
        public ActionResult GetFamilyMember(int id)
        {
            var services = CrteateMemberService();
            var familymember = services.GetFamilyMember(id);
            if (familymember is null)
            {
                return HttpNotFound();
            }
            return View(familymember);
        }
       
       
        //============= Send Email ====================//
        public ActionResult SendEmail(int id)
        {

            var services = CrteateMemberService();
            var member = services.GetMemberById(id);
            string from = "foluso.o.adegboye@gmail.com";
            string messg = String.Format($"Dear  {member.FirstName}<br/>\n" +
                $"Turnaround greetings to you in the name of the Lord Jesus christ<br/>\n" +
                $"We just like to reach out to yoiuy and find out that you are not in church yesterday");
            string subject = "Absent From Church"; 
            SendEmail sendEmail = new SendEmail();
            sendEmail.SendNotification( member.EmailAddress, messg,subject);
            

                return RedirectToAction("Index");
        }
        //GET:Email
        public ActionResult Email(Email model)
        {
            if (model.Id != null)
            {
                ViewBag.CheckId = "true";
            }
            else
            {
                ViewBag.CheckId = null;
            }
            return View();
        }
        //POST:Email

        [HttpPost]
        [ActionName("Email")]
        public ActionResult IndividualEmail2(Email model)
        {
            var services = CrteateMemberService();
            string sendTo = string.Empty;
            if (model.Id!=null)
            {
                var member = services.GetMemberById(Convert.ToInt32(model.Id));
                sendTo = member.EmailAddress;
            }
            else
            {
                sendTo = model.To;
            }
            
            SendEmail SendEmail = new SendEmail();
           
            
            try
            {
                if (ModelState.IsValid)
                {

                    SendEmail.SendNotification(sendTo, model.Body, model.Subject);
                    TempData["SaveResult"] = "Email was successfuly sent.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

            }
                return View();
            
        }

        //public void SendBirthdayMessage()
        //{
        //    var services = CrteateMemberService();
        //    var members = services.GetMembers();
        //    foreach(var member in members)
        //    {
        //        if (member.DateOfBirth == DateTime.Today)
        //        {
        //            SendEmail(member.MemberId);
        //        }
        //    }
        //}


        public ActionResult AddMemberToServiceUnit(int id)
        {
            var service = CrteateMemberService();

            var member = new MemberServiceUnitCreate()
            {
                MemberId = id,
                ServiceUnits =service.GetAllServiceUnit()
            };
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddMemberToServiceUnit(MemberServiceUnitCreate model)
        {
            var service = CrteateMemberService();
            model.ServiceUnits = service.GetAllServiceUnit();

            if (!ModelState.IsValid) return View();
            var services = CrteateMemberService();
            if (services.AddMemberToServiceUnit(model))
            {
                TempData["SaveResult"] = "Member Succsfully Added to Class ";
                return RedirectToAction("Index", "Member");
            }

            ModelState.AddModelError("", "Member already added to this Service Unit");

            return View(model);
        }

        //Delete or remove member from a list

        public ActionResult Remove(int id)
        {
            var sevice = CrteateMemberService();
            var model = sevice.GetMemberServiceById(id);
            return View(model);

        }
        //GET :MemberService/Delete/Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Remove")]
        public ActionResult RemoveMemberService(int id)
        {
            var sevice = CrteateMemberService();
            sevice.DeleteMemberFromServiceUnit(id);
            TempData["SaveResult"] = "Member removed from Unit";
            return RedirectToAction("Index");
        }


    }


}


