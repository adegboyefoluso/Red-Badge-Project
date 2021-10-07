using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;
using WinnersIndy.Model.ChildAttendanceFolder;

namespace WinnersIndy.Services
{
    public class ChildrenClassAttendanceServices
    {

        private readonly Guid _userId;
        public ChildrenClassAttendanceServices(Guid userid)
        {
            _userId = userid;
        }

        public bool CreateChildAttendance(ChildrenClassAttendanceCreate model)
        {
            //List<Child> listofchildren;
            //using (var ctx= new ApplicationDbContext())
            //{
            //     listofchildren = ctx.Children
            //                            .Where(e => e.ChildrenClassId == model.ChildrenClassId).ToList();
            //}
            var entity = new ChildrenClassAttendance()
            {
                MemberId = model.PersonId,
                AttendanceId = model.AttendanceId,
                InChurch = model.InChurch



            };
            //foreach(var item in entity.Children)
            //{
            //    item.Inchurch = model.InChurch;
            //}
            using (var ctx = new ApplicationDbContext())
            {

                ctx.ChildrenClassAttendances.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //public IList<Attendance> GetAttendances()
        //{
        //    using (var ctx=new ApplicationDbContext())
        //    {
        //        var attendances = ctx.Attendances.ToList();
        //        return attendances;
        //    }

        //}

        //public List<Child> GetChildren(int id)
        //{
        //    var children = new ChildrenClassServices(_userId).GetListofChildrenInClass(id).ChildDetails;
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var children = ctx
        //                            .Children
        //                            .Where(e => e.ChildrenClass. == id);
        //        return children.ToList();
        //    }
        //    return children;
        //    return children;
        //}

        public IEnumerable<ChildrenClassAttendance> GetChildAttendance()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                                .ChildrenClassAttendances
                                .ToList();
                return query;
            }

        }

        public ChildrenClassAttendance GetChildAttendance(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var childattendance = ctx
                                        .ChildrenClassAttendances
                                        .Find(id);
                return childattendance;
            }
        }

        //public bool Edit(ChildAttendanceCreate model)
        //{
        //    using (var ctx =new ApplicationDbContext())
        //    {
        //        var query = ctx
        //                        .ChildrenAttendance
        //                        .SingleOrDefault(e => e.ChildId == model.ChildId && e.AttendanceId == model.AttendanceId);
        //        query.InChurch = model.InChurch;
        //      return   ctx.SaveChanges() == 1;
        //    }
        //}

    }
}

