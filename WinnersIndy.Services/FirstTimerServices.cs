using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;
using WinnersIndy.Model.FirstTimer;

namespace WinnersIndy.Services
{
   public  class FirstTimerServices
    {
        private readonly Guid _userId;
        public FirstTimerServices(Guid userid)
        {
            _userId = userid;
        }


        public bool CreateFirstTimer(FirstTimerCreate model)
        {
            var firstTimer= new FirstTimer()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                EmailAddress= model.EmailAddress,
                Address= model.Address,
                DateOfVisit=model.DateOfVisit,
                OwnerId = _userId,
                PurposeOfVisit=model.PurposeOfVist
            };
            using (var ctx = new ApplicationDbContext())
            {
                
                var contact = ctx.Contacts.FirstOrDefault(e => e.PhoneNumber == model.PhoneNumber);
                if(contact != null)
                {
                    contact.Converted = true;
                    ctx.FirstTimers.Add(firstTimer);

                }
                else
                {
                    ctx.FirstTimers.Add(firstTimer);
                }
                return ctx.SaveChanges() >= 1;
            }

        }
        public IEnumerable<FirstTimerListItem> GetAllFirstTimer()
        {
            using(var ctx= new ApplicationDbContext())
            {
                var firstTimers = ctx.
                                        FirstTimers.Where(e=>e.IsConverted==false)
                                        .Select(e => new FirstTimerListItem
                                        {
                                            Id = e.Id,
                                            FirstName = e.FirstName,
                                            LastName = e.LastName,
                                            PhoneNumber = e.PhoneNumber,
                                            Address = e.Address,
                                            DateOfVisit = e.DateOfVisit,
                                            EmailAddress = e.EmailAddress,
                                            PurposeOfVisit = e.PurposeOfVisit,

                                        }).ToList();
                return firstTimers;
            }
        }
        // First Timer  who  are now members
        public IEnumerable<ConvertedFirstTimerLkistItem> GetFirstTimerWhoAreMembers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var firstTimers = ctx.
                                        FirstTimers.Where(e => e.IsConverted == true)
                                        .Select(e => new ConvertedFirstTimerLkistItem
                                        {
                                            Id = e.Id,
                                            FirstName = e.FirstName,
                                            LastName = e.LastName,
                                            PhoneNumber = e.PhoneNumber,
                                            Address = e.Address,
                                            DateOfVisit = e.DateOfVisit,
                                            EmailAddress = e.EmailAddress,
                                            PurposeOfVisit = e.PurposeOfVisit,
                                            IsConverted=e.IsConverted

                                        }).ToList();
                return firstTimers;
            }
        }
        public FirstTimerDetail GetFirstTimerById(int id)
        {
            using(var ctx =new ApplicationDbContext())
            {
                var firstTimer = ctx
                                    .FirstTimers.SingleOrDefault(e => e.IsConverted == false && e.Id == id);
                if (firstTimer == null) return null;
                else
                {
                    return new FirstTimerDetail
                    {
                        Id = firstTimer.Id,
                        FirstName = firstTimer.FirstName,
                        LastName = firstTimer.LastName,
                        Address = firstTimer.Address,
                        DateOfVisit = firstTimer.DateOfVisit,
                        EmailAddress = firstTimer.EmailAddress,
                        PhoneNumber = firstTimer.PhoneNumber,
                        PurposeOfVisit = firstTimer.PurposeOfVisit

                    }; 
                }
            }
        }
        
    }
}
