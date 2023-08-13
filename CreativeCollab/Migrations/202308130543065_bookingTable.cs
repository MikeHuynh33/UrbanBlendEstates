namespace CreativeCollab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookingTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        PropertyId = c.Int(nullable: false),
                        restaurant_id = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        TimeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Bookings");
        }
    }
}
