using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;
using WinnersIndy.Model;
using WinnersIndy.Model.MemberFolder;
using WinnersIndy.Model.ServiceUnit;

namespace WinnersIndy.Services
{
    public class MemberServices
    {


        private readonly Guid _userId;
        public MemberServices(Guid userid)
        {
            _userId = userid;
        }

        public bool CreateMember(MemberCreate model)
        {
            byte[] bytes = null;
            if (model.File != null)
            {
                Stream Fs = model.File.InputStream;
                BinaryReader Br = new BinaryReader(Fs);
                bytes = Br.ReadBytes((Int32)Fs.Length);
            }
            var entity =
                new Member()
                {
                    FirstName = model.FirstName.Substring(0, 1).ToUpper() + model.FirstName.Substring(1),
                    LastName = model.LastName.Substring(0, 1).ToUpper() + model.LastName.Substring(1),
                    DateOfBirth = model.DateOfBirth,
                    EmailAddress = model.EmailAddress,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    FileContent = bytes,
                    OwnerId = _userId,
                    Gender = model.Gender,
                    MaritalStatus = model.MaritalStatus,
                    FamilyId = model.FamilyId
                };
            using (var ctx = new ApplicationDbContext())
            {
                var FirstTimer = ctx.FirstTimers.FirstOrDefault(e => e.PhoneNumber == model.PhoneNumber);
                if (FirstTimer != null)
                {
                    FirstTimer.IsConverted = true;
                    ctx.Members.Add(entity);
                }
                else
                {
                    ctx.Members.Add(entity);
                }
                //ctx.Members.Add(entity);
                return ctx.SaveChanges() >= 1;
            }

        }

        public IEnumerable<MemberListItem> GetMembers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var members =
                            ctx
                            .Members.Where(e => e.IsActive == true && DateTime.Now.Year - e.DateOfBirth.Year > 18)
                            .Select(e => new MemberListItem
                            {

                                MemberId = e.MemberId,
                                FirstName = e.FirstName.Substring(0, 1).ToUpper() + e.FirstName.Substring(1),
                                LastName = e.LastName.Substring(0, 1).ToUpper() + e.LastName.Substring(1),
                                EmailAddress = e.EmailAddress,
                                PhoneNumber = e.PhoneNumber,
                                DateOfBirth = e.DateOfBirth,
                                FullName= e.FirstName+ " "+e.LastName
                                
                                


                            }).ToArray();

                return members;
            }
        }
        //================ Get Individual Member details ===============//
        public MemberDetails GetMemberById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var member =
                        ctx
                        .Members
                        .Single(e => e.MemberId == id&&e.IsActive==true);
                return new MemberDetails()
                {
                    MemberId = member.MemberId,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    PhoneNumber = member.PhoneNumber,
                    DateOfBirth = member.DateOfBirth,
                    Address = member.Address,
                    EmailAddress = member.EmailAddress,
                    FileContent = member.FileContent,
                    FamilyName = member.FamilyId == null ? string.Empty : member.Family.FamilyName, //Ternary operation
                    MemberServiceUnits = ctx.MemberServiceUnits.Where(e=>e.MemberId==id).Select(e=>new MemberServiveUnitListItem
                    {
                         Id=e.Id,
                         ServiceUnitName=e.ServiceUnit.Name
                    }).ToList()


                };
            }
        }
        public bool UpdateMember(MemberEdit model)
        {
            byte[] bytes = null;
            if (model.File != null)
            {
                Stream Fs = model.File.InputStream;
                BinaryReader Br = new BinaryReader(Fs);
                bytes = Br.ReadBytes((Int32)Fs.Length);
            }

            using (var ctx = new ApplicationDbContext())
            {
                var member =
                    ctx
                    .Members
                    .Find(model.MemberId);

                member.FirstName = model.FirstName.Substring(0, 1).ToUpper() + model.FirstName.Substring(1);
                member.LastName = model.LastName.Substring(0, 1).ToUpper() + model.LastName.Substring(1); 
                member.PhoneNumber = model.PhoneNumber;
                member.EmailAddress = model.EmailAddress;
                member.DateOfBirth = model.DateOfBirth;
                member.FileContent = bytes;
                member.ModifiedUtc = DateTimeOffset.Now;
                member.Address = model.Address;
               

                return ctx.SaveChanges() == 1;
            }
        }
        //=============Delete A Member=======================================================//
        public bool DeleteMember(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var member = ctx
                    .Members
                    .SingleOrDefault(e =>/* e.OwnerId == _userId &&*/ e.MemberId == id);
                //foreach (var item in member.Contacts)
                //{
                //    item.MemberId = null;
                //}
                member.IsActive = false;
                //ctx.Members.Remove(member);
                return ctx.SaveChanges() >= 1;
            }
        }
        //==============List Of Members where their Age is less than 18 as Children===========//
        public IEnumerable<Children> GetChildren()
        {
            List<Children> ListOfChildren = new List<Children>();
            using (var ctx = new ApplicationDbContext())
            {
                var members = ctx.Members.Where(e=>e.IsActive==true)
                                  .Select(e => new Children()
                                  {
                                      MemberId = e.MemberId,
                                      FirstName = e.FirstName,
                                      LastName = e.LastName,
                                      DateOfBirth = e.DateOfBirth,
                                      Age = DateTime.Now.Year - e.DateOfBirth.Year

                                  }).ToList();


                foreach (var member in members)
                {
                    if (member.Age <= 18)
                    {
                        ListOfChildren.Add(member);
                    }
                }


            }
            return ListOfChildren;

        }

        //public IEnumerable<Children> GetChildren()
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var members =
        //                    ctx
        //                    .Members.Where(e => e.DateOfBirth.Year - DateTime.Now.Year >= 18)
        //                    .Select(e => new Children
        //                    {

        //                        MemberId = e.MemberId,
        //                        FirstName = e.FirstName.Substring(0, 1).ToUpper() + e.FirstName.Substring(1),
        //                        LastName = e.LastName.Substring(0, 1).ToUpper() + e.LastName.Substring(1),
        //                        DateOfBirth = e.DateOfBirth,
        //                        Age = DateTime.Now.Year - e.DateOfBirth.Year


        //                    }).ToArray();

        //        return members;
        //    }
        //}
        //================Add A child to Children Class==============//

        public bool AddChildtoclass(AddChild model)
        {
            //===========Check to prevent adding  a student that is already  added to a class.
            using (var ctx = new ApplicationDbContext())
            {
                var checkchild = ctx
                            .Members
                            .SingleOrDefault(e=>e.IsActive==true&&model.MemberId==e.MemberId);
                if (checkchild.ChildrenClassId >= 1)
                {
                    return false;
                }

                var child = ctx
                  .Members
                  .Find(model.MemberId);
                var childrenclass = ctx
                    .ChildrenClasses
                    .Find(model.ChildrenClassId);
                childrenclass.Children.Add(child);
                return ctx.SaveChanges() == 1;
            }

            //using (var ctx = new ApplicationDbContext())
            //{
            //    var child = ctx
            //        .Members
            //        .Find(model.MemberId);
            //    var childrenclass = ctx
            //        .ChildrenClasses
            //        .Find(model.ChildrenClassId);
            //    childrenclass.Children.Add(child);
            //    return ctx.SaveChanges() == 1;
            //}
        }
        // Remove a child from a class
        public bool DeleteChildFromClass(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var member = ctx.Members.Find(id);
                var entity = ctx.ChildrenClasses.ToList();
                foreach(var c in entity)
                {
                    foreach(var e in c.Children.ToList())
                    {
                        if (e.MemberId == id)
                        {
                            c.Children.Remove(member);
                        }
                    }
                }
                
                return ctx.SaveChanges() >= 1;
            }
        }
        //==================Get List of Children Classes============//

        public List<ChildrenClass> GetDropdown()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var nvm =
                    ctx
                        .ChildrenClasses
                        .ToList();
                return nvm;
            }
        }


        //=============List of Members that are Teachers===========//
        //public List<MemberListItem> GetTeachers()
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var teachers = ctx
        //                            .Members
        //                            .Where(e => e.ServiceUnit == UnitService.Teacher)
        //                            .Select(e => new MemberListItem
        //                            {
        //                                MemberId = e.MemberId,
        //                                FirstName = e.FirstName,
        //                                LastName = e.LastName,
        //                                EmailAddress = e.EmailAddress,
        //                                PhoneNumber = e.PhoneNumber
        //                            }).ToList();

        //        return teachers;

        //    }
        //}

        public bool AddTeachertoclass(AddChild model)
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

        public ICollection<Member> GetFamilyMember(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var member = ctx
                                .Members
                                .SingleOrDefault(e => e.MemberId == id);

                var family = ctx
                                        .Families
                                        .SingleOrDefault(e => e.FamilyId == member.FamilyId);
                if (family is null)
                {
                    return null;
                }


                return family.FamilyMember;
            }
        }

        //Add a memeber to a servvice unit using the joining table 
        public bool AddMemberToServiceUnit(MemberServiceUnitCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                // Seachingto prevent duplicating a member 
                var member = ctx.MemberServiceUnits.SingleOrDefault(e => e.MemberId == model.MemberId && e.ServiceUnitId == model.ServiceUnitId);
                if (member != null) return false;

                var entity = new MemberServiceUnit()
                {
                    MemberId = model.MemberId,
                    ServiceUnitId = model.ServiceUnitId,
                     
                     
                };

                ctx.MemberServiceUnits.Add(entity);
                return ctx.SaveChanges() == 1;

            }
            //var entity = new MemberServiceUnit()
            //{
            //    MemberId = model.MemberId,
            //    ServiceUnitId = model.ServiceUnitId
            //};
            //using (var ctx = new ApplicationDbContext())
            //{
            //    ctx.MemberServiceUnits.Add(entity);
            //    return ctx.SaveChanges() == 1;
            //}


        }

        public bool DeleteMemberFromServiceUnit(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.MemberServiceUnits.SingleOrDefault(e => e.Id == id);
                if (entity is null) return false;
                ctx.MemberServiceUnits.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public MemberServiveUnitListItem GetMemberServiceById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.MemberServiceUnits.Find(id);
                return new MemberServiveUnitListItem
                {
                    Id = query.Id,
                    ServiceUnitName=query.ServiceUnit.Name
                   
                };
            }

        }

        public List<ServiceUnitListItem> GetAllServiceUnit()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var queries = ctx.ServiceUnits.Where(e=>e.IsActive==true).Select(e => new ServiceUnitListItem
                {
                    ServiceUnitId = e.Id,
                    Name = e.Name,

                }).ToList();
                return queries;
            }
        }

    }


}
