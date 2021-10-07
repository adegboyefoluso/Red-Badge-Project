namespace WinnersIndy.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class another : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Contact", new[] { "MemberId" });
            AlterColumn("dbo.Contact", "MemberId", c => c.Int(nullable: false));
            CreateIndex("dbo.Contact", "MemberId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Contact", new[] { "MemberId" });
            AlterColumn("dbo.Contact", "MemberId", c => c.Int());
            CreateIndex("dbo.Contact", "MemberId");
        }
    }
}
