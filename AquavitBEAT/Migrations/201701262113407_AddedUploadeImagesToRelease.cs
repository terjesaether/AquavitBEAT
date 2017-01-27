namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUploadeImagesToRelease : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UploadedImages",
                c => new
                    {
                        UploadedImageId = c.Int(nullable: false, identity: true),
                        ImgUrl = c.String(),
                        Title = c.String(),
                        Release_ReleaseId = c.Int(),
                    })
                .PrimaryKey(t => t.UploadedImageId)
                .ForeignKey("dbo.Releases", t => t.Release_ReleaseId)
                .Index(t => t.Release_ReleaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UploadedImages", "Release_ReleaseId", "dbo.Releases");
            DropIndex("dbo.UploadedImages", new[] { "Release_ReleaseId" });
            DropTable("dbo.UploadedImages");
        }
    }
}
