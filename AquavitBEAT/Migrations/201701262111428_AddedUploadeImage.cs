namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUploadeImage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Artworks", "TypeOfArtwork_ReleaseTypeId", "dbo.ReleaseTypes");
            DropIndex("dbo.Artworks", new[] { "TypeOfArtwork_ReleaseTypeId" });
            AlterColumn("dbo.Artworks", "Title", c => c.String());
            AlterColumn("dbo.Artworks", "TypeOfArtwork_ReleaseTypeId", c => c.Int());
            CreateIndex("dbo.Artworks", "TypeOfArtwork_ReleaseTypeId");
            AddForeignKey("dbo.Artworks", "TypeOfArtwork_ReleaseTypeId", "dbo.ReleaseTypes", "ReleaseTypeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Artworks", "TypeOfArtwork_ReleaseTypeId", "dbo.ReleaseTypes");
            DropIndex("dbo.Artworks", new[] { "TypeOfArtwork_ReleaseTypeId" });
            AlterColumn("dbo.Artworks", "TypeOfArtwork_ReleaseTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Artworks", "Title", c => c.String(nullable: false));
            CreateIndex("dbo.Artworks", "TypeOfArtwork_ReleaseTypeId");
            AddForeignKey("dbo.Artworks", "TypeOfArtwork_ReleaseTypeId", "dbo.ReleaseTypes", "ReleaseTypeId", cascadeDelete: true);
        }
    }
}
