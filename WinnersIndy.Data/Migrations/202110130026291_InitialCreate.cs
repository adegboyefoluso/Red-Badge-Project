namespace WinnersIndy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendance",
                c => new
                    {
                        AttendanceId = c.Int(nullable: false, identity: true),
                        ChildrenClassId = c.Int(),
                        AttendanceDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ChildrenClass_ChildrenClassId = c.Int(),
                    })
                .PrimaryKey(t => t.AttendanceId)
                .ForeignKey("dbo.ChildrenClass", t => t.ChildrenClass_ChildrenClassId)
                .ForeignKey("dbo.ChildrenClass", t => t.ChildrenClassId)
                .Index(t => t.ChildrenClassId)
                .Index(t => t.ChildrenClass_ChildrenClassId);
            
            CreateTable(
                "dbo.ChildrenClass",
                c => new
                    {
                        ChildrenClassId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        OwenerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ChildrenClassId);
            
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DateOfBirth = c.DateTimeOffset(nullable: false, precision: 7),
                        Address = c.String(nullable: false),
                        Gender = c.Int(nullable: false),
                        FamilyId = c.Int(),
                        PhoneNumber = c.String(),
                        EmailAddress = c.String(),
                        FileName = c.String(),
                        FileContent = c.Binary(),
                        MaritalStatus = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ChildrenClassId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.ChildrenClass", t => t.ChildrenClassId)
                .ForeignKey("dbo.Family", t => t.FamilyId)
                .Index(t => t.FamilyId)
                .Index(t => t.ChildrenClassId);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        MemberId = c.Int(nullable: false),
                        Converted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Member", t => t.MemberId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Family",
                c => new
                    {
                        FamilyId = c.Int(nullable: false, identity: true),
                        AniversaryDate = c.DateTime(),
                        FamilyName = c.String(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FamilyId);
            
            CreateTable(
                "dbo.MemberServiceUnit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        ServiceUnitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Member", t => t.MemberId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceUnit", t => t.ServiceUnitId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.ServiceUnitId);
            
            CreateTable(
                "dbo.ServiceUnit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Ownerid = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChildrenClassAttendance",
                c => new
                    {
                        MemberId = c.Int(nullable: false),
                        AttendanceId = c.Int(nullable: false),
                        InChurch = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.MemberId, t.AttendanceId })
                .ForeignKey("dbo.Attendance", t => t.AttendanceId, cascadeDelete: true)
                .ForeignKey("dbo.Member", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.AttendanceId);
            
            CreateTable(
                "dbo.FirstTimer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        EmailAddress = c.String(),
                        Address = c.String(),
                        DateOfVisit = c.DateTimeOffset(nullable: false, precision: 7),
                        OwnerId = c.Guid(nullable: false),
                        IsConverted = c.Boolean(nullable: false),
                        PurposeOfVisit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Report",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReportPeriod = c.DateTime(nullable: false),
                        ServiceUnitId = c.Int(nullable: false),
                        TypeOfReport = c.Int(nullable: false),
                        File = c.String(),
                        FileContent = c.Binary(),
                        OwnerId = c.Guid(nullable: false),
                        Size = c.Long(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceUnit", t => t.ServiceUnitId, cascadeDelete: true)
                .Index(t => t.ServiceUnitId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Report", "ServiceUnitId", "dbo.ServiceUnit");
            DropForeignKey("dbo.ChildrenClassAttendance", "MemberId", "dbo.Member");
            DropForeignKey("dbo.ChildrenClassAttendance", "AttendanceId", "dbo.Attendance");
            DropForeignKey("dbo.Attendance", "ChildrenClassId", "dbo.ChildrenClass");
            DropForeignKey("dbo.MemberServiceUnit", "ServiceUnitId", "dbo.ServiceUnit");
            DropForeignKey("dbo.MemberServiceUnit", "MemberId", "dbo.Member");
            DropForeignKey("dbo.Member", "FamilyId", "dbo.Family");
            DropForeignKey("dbo.Contact", "MemberId", "dbo.Member");
            DropForeignKey("dbo.Member", "ChildrenClassId", "dbo.ChildrenClass");
            DropForeignKey("dbo.Attendance", "ChildrenClass_ChildrenClassId", "dbo.ChildrenClass");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Report", new[] { "ServiceUnitId" });
            DropIndex("dbo.ChildrenClassAttendance", new[] { "AttendanceId" });
            DropIndex("dbo.ChildrenClassAttendance", new[] { "MemberId" });
            DropIndex("dbo.MemberServiceUnit", new[] { "ServiceUnitId" });
            DropIndex("dbo.MemberServiceUnit", new[] { "MemberId" });
            DropIndex("dbo.Contact", new[] { "MemberId" });
            DropIndex("dbo.Member", new[] { "ChildrenClassId" });
            DropIndex("dbo.Member", new[] { "FamilyId" });
            DropIndex("dbo.Attendance", new[] { "ChildrenClass_ChildrenClassId" });
            DropIndex("dbo.Attendance", new[] { "ChildrenClassId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Report");
            DropTable("dbo.FirstTimer");
            DropTable("dbo.ChildrenClassAttendance");
            DropTable("dbo.ServiceUnit");
            DropTable("dbo.MemberServiceUnit");
            DropTable("dbo.Family");
            DropTable("dbo.Contact");
            DropTable("dbo.Member");
            DropTable("dbo.ChildrenClass");
            DropTable("dbo.Attendance");
        }
    }
}
