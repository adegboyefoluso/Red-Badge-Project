using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinnersIndy.Data;
using WinnersIndy.Model.Contact;

namespace WinnersIndy.Services
{
     public class ContactService
    {
        private readonly Guid _userId;
        public ContactService(Guid userid)
        {
            _userId = userid;
        }

        public bool CreateContact(ContactCreate model)
        {
            var contact = new Contact()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                MemberId = model.MemberId,
                OwnerId=_userId
            };
            using(var ctx= new ApplicationDbContext())
            {
                ctx.Contacts.Add(contact);
                return ctx.SaveChanges() == 1;
            }

        }
        public IEnumerable<ContactListItem> GetAllContact()
        {
            using (var ctx= new ApplicationDbContext())
            {
               
                var contacts = ctx.Contacts.Where(e=>e.IsActive)
                                            .Select(e => new ContactListItem
                                            {
                                                Id=e.Id,
                                                FirstName = e.FirstName.Substring(0, 1).ToUpper() + e.FirstName.Substring(1),
                                                LastName = e.LastName.Substring(0, 1).ToUpper() + e.LastName.Substring(1),
                                                PhoneNumber = e.PhoneNumber,
                                                MemberLastName = e.Member.LastName,
                                                MemberFirstName = e.Member.FirstName
                                            }).ToList();
                return contacts;
            }

        }

        public IEnumerable<ConvertedContact> GetAllcovertedContact()
        {
            using(var ctx=new ApplicationDbContext())
            {
                var contacts = ctx.Contacts.Where(e => e.Converted == true)
                                 .Select(e => new ConvertedContact
                                 {
                                     FirstName = e.FirstName,
                                     LastName = e.LastName,
                                     PhoneNumber = e.PhoneNumber,
                                     MemberLastName = e.Member.LastName,
                                     MemberFirstName = e.Member.FileName
                                 }).ToList();
                return contacts;
            }
        }

        //Get Contact who has not been coverted  to Member //
        public ContactListItem GetContactById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                
                var contacts = ctx.Contacts.SingleOrDefault(e => e.Id == id && e.Converted == false && e.IsActive == true);
                if (contacts is null) return null;


                return new ContactListItem
                {
                    Id = contacts.Id,
                    FirstName = contacts.FirstName.Substring(0, 1).ToUpper() + contacts.FirstName.Substring(1),
                    LastName = contacts.LastName.Substring(0, 1).ToUpper() + contacts.LastName.Substring(1),
                    PhoneNumber = contacts.PhoneNumber,
                    MemberLastName = contacts.Member.LastName,
                    MemberFirstName = contacts.Member.FirstName
                };

            }
        }
        //Edit a contact//

        public bool EditContact(ContactEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var contacts = ctx.Contacts.SingleOrDefault(e => e.Id == model.ContactId && e.Converted == false&&e.IsActive==true);
                if (contacts is null) return false;
                contacts.FirstName = model.FirstName;
                contacts.LastName = model.LastName;
                contacts.PhoneNumber = model.PhoneNumber;

                return ctx.SaveChanges() == 1;

            }
        }
        //Soft Delete on Contact
        public bool DeleteContact(int id)
        {
            using(var ctx= new ApplicationDbContext())
            {
                var contact = ctx.Contacts.SingleOrDefault(e => e.Id == id && e.Converted == false && e.IsActive == true);
                if (contact is null) return false;
                contact.IsActive = false;
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
