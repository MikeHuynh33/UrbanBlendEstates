namespace CreativeCollab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TheLatestUpdateMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropertyDetails", "NeighbourhoodId", c => c.Int(nullable: false));
            CreateIndex("dbo.PropertyDetails", "NeighbourhoodId");
            AddForeignKey("dbo.PropertyDetails", "NeighbourhoodId", "dbo.Neighbourhoods", "NeighbourhoodId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropertyDetails", "NeighbourhoodId", "dbo.Neighbourhoods");
            DropIndex("dbo.PropertyDetails", new[] { "NeighbourhoodId" });
            DropColumn("dbo.PropertyDetails", "NeighbourhoodId");
        }
    }
}
