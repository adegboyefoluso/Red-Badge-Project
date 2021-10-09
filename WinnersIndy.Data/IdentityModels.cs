using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WinnersIndy.Data;

namespace WinnersIndy.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Family> Families { get; set; }
        
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<ChildrenClass> ChildrenClasses { get; set; }
        
        public DbSet<ChildrenClassAttendance> ChildrenClassAttendances { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<FirstTimer> FirstTimers { get; set; }
        public DbSet<ServiceUnit>ServiceUnits { get; set; }
        public DbSet<MemberServiceUnit> MemberServiceUnits { get; set; }
        public DbSet<Report> Reports { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Conventions
                 .Remove<PluralizingTableNameConvention>();

            modelBuilder
                .Configurations
                .Add(new IdentityUserLoginConfiguration())
                .Add(new IdentityUserRoleConfiguration());

            modelBuilder.Entity<Attendance>()
                .HasOptional<ChildrenClass>(c => c.ChildrenClass)
                .WithMany()
                .WillCascadeOnDelete(false);

            //A member can may or may not have a contact , but a contact  must always have  a member
            // when a member is deleted , the child(Contact ) will not be  deleted
            //==>A Member may have many contact( HAsMany ) but a contact must have a member(WithRequired).
            modelBuilder.Entity<Member>()
                .HasMany(c => c.Contacts)
                .WithOptional(a => a.Member)
                .WillCascadeOnDelete(false); // to research further on why the cascade delete  is not setting to false in botyh 

            //modelBuilder.Entity<Family>()
            //    .HasMany(c => c.FamilyMember)
            //    .WithOptional(a => a.Family)
            //    .WillCascadeOnDelete(false);



        }
    }

    public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
    {
        public IdentityUserLoginConfiguration()
        {
            HasKey(iul => iul.UserId);
        }
    }

    public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
    {
        public IdentityUserRoleConfiguration()
        {
            HasKey(iur => iur.UserId);
        }
    }

}