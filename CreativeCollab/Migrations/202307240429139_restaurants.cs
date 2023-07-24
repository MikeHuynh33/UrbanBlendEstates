namespace CreativeCollab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restaurants : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false, identity: true),
                        RestaurantName = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        RestaurantLink = c.String(),
                    })
                .PrimaryKey(t => t.RestaurantId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Restaurants");
        }
    }
}
