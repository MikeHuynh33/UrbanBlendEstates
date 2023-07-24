namespace CreativeCollab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class neighbourhoodrestaurant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "NeighbourhoodId", c => c.Int(nullable: false));
            CreateIndex("dbo.Restaurants", "NeighbourhoodId");
            AddForeignKey("dbo.Restaurants", "NeighbourhoodId", "dbo.Neighbourhoods", "NeighbourhoodId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Restaurants", "NeighbourhoodId", "dbo.Neighbourhoods");
            DropIndex("dbo.Restaurants", new[] { "NeighbourhoodId" });
            DropColumn("dbo.Restaurants", "NeighbourhoodId");
        }
    }
}
