namespace AquavitBEAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCommentToRelease : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Releases", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Releases", "Comment");
        }
    }
}
