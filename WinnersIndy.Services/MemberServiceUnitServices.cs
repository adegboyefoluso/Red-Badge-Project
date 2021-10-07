using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;
using WinnersIndy.Model;

namespace WinnersIndy.Services
{
    public class MemberServiceUnitServices
    {
        private readonly Guid _userId;
        public MemberServiceUnitServices(Guid userid)
        {
            _userId = userid;
        }

        public bool  AddMemberToServiceUnit (MemberServiceUnitCreate model)
        {
            var entity = new MemberServiceUnit()
            {
                MemberId = model.MemberId,
                ServiceUnitId = model.ServiceUnitId
            };
            using(var ctx= new ApplicationDbContext())
            {
                ctx.MemberServiceUnits.Add(entity);
                return ctx.SaveChanges() == 1;
            }


        }

        public bool DeleteMemberFromServiceUnit(int id)
        {
            using( var ctx= new ApplicationDbContext())
            {
                var entity = ctx.MemberServiceUnits.SingleOrDefault(e =>e.Id==id );
                if (entity is null) return false;
                ctx.MemberServiceUnits.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        
        
    }
}
