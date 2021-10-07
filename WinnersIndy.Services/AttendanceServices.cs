using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;
using WinnersIndy.Model.AttendanceFolder;

namespace WinnersIndy.Services
{
    public class AttendanceServices
    {
        private readonly Guid _userId;
        public AttendanceServices(Guid userid)
        {
            _userId = userid;
        }

        public bool CreateAttendance(AttendanceList model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var attendance = ctx.Attendances
                                   .SingleOrDefault(e => e.ChildrenClassId == model.ChildrenClassId && e.AttendanceDate == DateTime.Today);
                if (attendance != null) return false;
               // if (attendance.AttendanceDate != DateTime.Today) return false;
            }
            var entity = new Attendance()
            {
                AttendanceDate = DateTime.Today,
                ChildrenClassId = model.ChildrenClassId
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Attendances.Add(entity);
                ctx.SaveChanges();
                var attendance = ctx.Attendances
                    .Where(e => e.AttendanceDate ==DateTime.Today && e.ChildrenClassId == model.ChildrenClassId)
                    .FirstOrDefault();

                foreach (var attendanceEntry in model.AttendanceSheetList)
                {
                    if (attendanceEntry.Inchurch)
                    {
                        ctx.ChildrenClassAttendances.Add(new ChildrenClassAttendance
                        {
                            AttendanceId = attendance.AttendanceId,
                            MemberId = attendanceEntry.MemberId,
                            InChurch = true,


                        });
                    }


                    else
                    {
                        ctx.ChildrenClassAttendances.Add(new ChildrenClassAttendance
                        {
                            AttendanceId = attendance.AttendanceId,
                            MemberId = attendanceEntry.MemberId,
                            InChurch = false,

                        });
                    }
                }
                return ctx.SaveChanges() >= 1;

            }

        }
        public IEnumerable<Attendance> GetAttendanceDate(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                                .Attendances
                                .Where(e => e.ChildrenClassId == id).ToList();
                return query;


            }
        }
        public Attendance GetDate(int id)
        {
            using(var ctx= new ApplicationDbContext())
            {
                var date = ctx
                                .Attendances
                                .Find(id);
                return date;
            }
        }

        public IEnumerable<AttendanceListItem> GetAllAttendance(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                                .Attendances
                                .Select(e => new AttendanceListItem()
                                {
                                    AttendanceDate = e.AttendanceDate,
                                    ListChildrenClassAttendanceListItem = ctx
                                                                                .ChildrenClassAttendances
                                                                                .Where(c => c.AttendanceId == e.AttendanceId && e.ChildrenClassId == id)
                                                                                .Select(m => new ChildrenClassAttendanceListItem
                                                                                {
                                                                                    MemberId = m.MemberId,
                                                                                    FirstName = m.Member.FirstName,
                                                                                    LastName = m.Member.LastName,
                                                                                    InChurch = m.InChurch
                                                                                    
                                                                                })

                                })
                                .ToList();
                return query;

            }
        }

        public IEnumerable<ChildrenClassAttendanceListItem> GetClassAttendance(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var listofClassAttendance = ctx
                                                .ChildrenClassAttendances
                                                .Where(c => c.AttendanceId == id)
                                               .Select(m => new ChildrenClassAttendanceListItem
                                               {
                                                   MemberId = m.MemberId,
                                                   FirstName = m.Member.FirstName,
                                                   LastName = m.Member.LastName,
                                                   InChurch = m.InChurch
                                               }).ToList();
                return listofClassAttendance;
            }
        }





        public List<Member> GetChildren(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var children = ctx
                                   .Members
                                   .Where(e => e.ChildrenClassId == id).ToList();
                return children;
            }
        }

        public List<AttendanceSheet> GetAttendanceList(int id)
        {
            var interim = GetChildren(id);
            List<AttendanceSheet> somename = new List<AttendanceSheet>();
            foreach (var child in interim)
            {
                somename.Add(new AttendanceSheet { MemberId = child.MemberId, Inchurch = false, FirstName = child.FirstName, LastName = child.LastName });
            }
            return somename;
        }
    }
}

