namespace HTTP5212_Passion_Project3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class artists : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ArtistId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Artists");
        }
    }
}
