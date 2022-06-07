namespace N01520224_PassionProject_Reeservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        GuestId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GuestId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Guests");
        }
    }
}
