namespace HTTP5212_Passion_Project3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class artworkimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artworks", "ArtworkImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artworks", "ArtworkImage");
        }
    }
}
