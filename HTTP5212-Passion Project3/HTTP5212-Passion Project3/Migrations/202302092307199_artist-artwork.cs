namespace HTTP5212_Passion_Project3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class artistartwork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artworks", "ArtistId", c => c.Int(nullable: false));
            CreateIndex("dbo.Artworks", "ArtistId");
            AddForeignKey("dbo.Artworks", "ArtistId", "dbo.Artists", "ArtistId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Artworks", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Artworks", new[] { "ArtistId" });
            DropColumn("dbo.Artworks", "ArtistId");
        }
    }
}
