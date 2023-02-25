namespace HTTP5212_Passion_Project3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Artwork : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artworks",
                c => new
                    {
                        ArtworkId = c.Int(nullable: false, identity: true),
                        ArtworkName = c.String(),
                        ArtworkDescription = c.String(),
                    })
                .PrimaryKey(t => t.ArtworkId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Artworks");
        }
    }
}
