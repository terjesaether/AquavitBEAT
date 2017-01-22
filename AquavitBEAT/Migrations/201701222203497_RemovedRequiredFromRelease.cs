namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredFromRelease : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Releases", "ReleaseType_ReleaseTypeId", "dbo.ReleaseTypes");
            DropIndex("dbo.Releases", new[] { "ReleaseType_ReleaseTypeId" });
            AlterColumn("dbo.Releases", "ReleaseType_ReleaseTypeId", c => c.Int());
            CreateIndex("dbo.Releases", "ReleaseType_ReleaseTypeId");
            AddForeignKey("dbo.Releases", "ReleaseType_ReleaseTypeId", "dbo.ReleaseTypes", "ReleaseTypeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Releases", "ReleaseType_ReleaseTypeId", "dbo.ReleaseTypes");
            DropIndex("dbo.Releases", new[] { "ReleaseType_ReleaseTypeId" });
            AlterColumn("dbo.Releases", "ReleaseType_ReleaseTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Releases", "ReleaseType_ReleaseTypeId");
            AddForeignKey("dbo.Releases", "ReleaseType_ReleaseTypeId", "dbo.ReleaseTypes", "ReleaseTypeId", cascadeDelete: true);
        }
    }
}
