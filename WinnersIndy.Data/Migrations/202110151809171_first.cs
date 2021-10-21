namespace WinnersIndy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckIn",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CheckInDate = c.DateTime(nullable: false),
                        MemberId = c.Int(nullable: false),
                        InChurch = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Member", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CheckIn", "MemberId", "dbo.Member");
            DropIndex("dbo.CheckIn", new[] { "MemberId" });
            DropTable("dbo.CheckIn");
        }
    }
}
