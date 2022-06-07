namespace N01520224_PassionProject_Reeservation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unitupdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Units", "UnitNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Units", "UnitNumber", c => c.String());
        }
    }
}
