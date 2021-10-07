using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;
using WinnersIndy.Model.MemberFolder;
using WinnersIndy.Model.ServiceUnit;

namespace WinnersIndy.Services
{
   public  class ServiceUnitService
    {

        private readonly Guid _userId;
        public ServiceUnitService(Guid userid)
        {
            _userId = userid;
        }

        public bool CreateServiceUnit (ServiceUnitCreate model)
        {
            var entity = new ServiceUnit()
            {
                Name = model.Name,
                Ownerid = _userId
            };
            using (var ctx= new ApplicationDbContext())
            {
                ctx.ServiceUnits.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<ServiceUnitListItem> GetAllServiceUnit()
        {
            using(var ctx= new ApplicationDbContext())
            {
                var queries = ctx.ServiceUnits.Where(e=>e.IsActive==true).Select(e => new ServiceUnitListItem
                {
                    ServiceUnitId = e.Id,
                    NumberOfMember = e.MemberServiceUnits.Count(),
                    Name = e.Name,
                    

                }).ToList();
                return queries;
            }
        }
        public ServiceUnitDetail GetServiceUnitById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.ServiceUnits.SingleOrDefault(e => e.Id == id&&e.IsActive==true);
                return new ServiceUnitDetail
                {
                    Id = query.Id,
                    Name = query.Name,
                    NumberOfMember = query.MemberServiceUnits.Count(),
                    Members = ctx.MemberServiceUnits.Where(e => e.ServiceUnitId == id).Select(e => new MemberListItem
                    {
                        FullName = e.Member.FirstName+" "+e.Member.LastName,
                        PhoneNumber = e.Member.PhoneNumber,
                        EmailAddress = e.Member.EmailAddress
                    }).ToList()

                };
            }
        }

        public bool DeleteServiceUnit(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.ServiceUnits.SingleOrDefault(e => e.Id == id&&e.IsActive==true);
                if(query is null)
                {
                    return false;
                }
                query.IsActive = false;
                return ctx.SaveChanges() == 1;
            }
        }
        
    }
}
