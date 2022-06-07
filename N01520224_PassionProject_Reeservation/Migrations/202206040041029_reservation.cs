namespace N01520224_PassionProject_Reeservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        ReservationNumber = c.String(),
                        ReservationStatus = c.String(),
                        GuestId = c.Int(nullable: false),
                        UnitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.Guests", t => t.GuestId, cascadeDelete: true)
                .ForeignKey("dbo.Units", t => t.UnitId, cascadeDelete: true)
                .Index(t => t.GuestId)
                .Index(t => t.UnitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Reservations", "GuestId", "dbo.Guests");
            DropIndex("dbo.Reservations", new[] { "UnitId" });
            DropIndex("dbo.Reservations", new[] { "GuestId" });
            DropTable("dbo.Reservations");
        }
    }
}
