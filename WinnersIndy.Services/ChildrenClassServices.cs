using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;
using WinnersIndy.Model.ChildrenClassFolder;
using WinnersIndy.Model.MemberFolder;

namespace WinnersIndy.Services
{
    public class ChildrenClassServices
    {
        private readonly Guid _userId;
        public ChildrenClassServices(Guid userid)
        {
            _userId = userid;

        }
        public bool CreateChildrenClass(ChildrenClassCreate model)
        {
            var childrenclass = new ChildrenClass()
            {
                Name = model.Name,
                Description = model.Description,

            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.ChildrenClasses.Add(childrenclass);
                return ctx.SaveChanges() == 1;
            }
        }
        public ChildrenClassEdit GetChildrenClassById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var childrenclass = ctx
                    .ChildrenClasses
                    .Find(id);
                return new ChildrenClassEdit
                {
                    ChildrenClassId = childrenclass.ChildrenClassId,
                    Description = childrenclass.Description,
                    Name = childrenclass.Name
                };

            }
        }
        public IEnumerable<ChildrenClassEdit> GetAllChildrenClass()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var childrenClasses = ctx
                    .ChildrenClasses
                    .Select(e => new ChildrenClassEdit
                    {
                        ChildrenClassId = e.ChildrenClassId,
                        Description = e.Description,
                        Name = e.Name


                    }).ToList();

                return childrenClasses;
            }

        }

        public bool DeleteChildrenClass(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var childrenclass = ctx
                    .ChildrenClasses
                    .Find(id);
                ctx.ChildrenClasses.Remove(childrenclass);
                return ctx.SaveChanges() == 1;

            }
        }
        public bool UpdateChildrenClass(ChildrenClassEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var childrenclass = ctx
                    .ChildrenClasses
                    .Find(model.ChildrenClassId);
                childrenclass.Name = model.Name;
                childrenclass.Description = model.Description;

                return ctx.SaveChanges() == 1;

            }
        }
        public bool AddChildrentoclass(AddChild model)  ///new 
        {
            using (var ctx = new ApplicationDbContext())
            {
                var child = ctx
                    .Members
                    .Find(model.MemberId);
                var childrenclass = ctx
                    .ChildrenClasses
                    .Find(model.ChildrenClassId);
                childrenclass.Children.Add(child);
                return ctx.SaveChanges() == 1;
            }
        }


        public ChildrenClassDetail GetListofChildrenInClass(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var childrenclass = ctx
                        .ChildrenClasses
                        .Find(id);

                return new ChildrenClassDetail()
                {
                    ChildrenClassId = childrenclass.ChildrenClassId,
                    ChildrenClass = childrenclass.Name,
                    Description = childrenclass.Description,
                    ChildDetails = childrenclass.Children.Where(e=>e.IsActive==true)
                    .Select(e => new ChildrenClassChildDetail
                    {
                        MemberId = e.MemberId,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        
                    }).ToList()


                };
            }
        }
    }
}

