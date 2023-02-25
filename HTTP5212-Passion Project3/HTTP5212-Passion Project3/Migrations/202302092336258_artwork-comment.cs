namespace HTTP5212_Passion_Project3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class artworkcomment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "ArtworkId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "ArtworkId");
            AddForeignKey("dbo.Comments", "ArtworkId", "dbo.Artworks", "ArtworkId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ArtworkId", "dbo.Artworks");
            DropIndex("dbo.Comments", new[] { "ArtworkId" });
            DropColumn("dbo.Comments", "ArtworkId");
        }
    }
}
