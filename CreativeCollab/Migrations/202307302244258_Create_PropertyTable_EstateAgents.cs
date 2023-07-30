namespace CreativeCollab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_PropertyTable_EstateAgents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EstateAgents",
                c => new
                    {
                        EstateAgentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.EstateAgentId);
            
            CreateTable(
                "dbo.PropertyDetails",
                c => new
                    {
                        PropertyID = c.Int(nullable: false, identity: true),
                        PropertyType = c.String(),
                        PropertyAddress = c.String(),
                        PropertySize = c.String(),
                        NumberOfBedrooms = c.Int(nullable: false),
                        NumberOfBathrooms = c.Int(nullable: false),
                        Amenities = c.String(),
                        PropertyPrice = c.Double(nullable: false),
                        PropertyDescription = c.String(),
                        PropertyStatus = c.String(),
                        ImageFileNames = c.String(),
                        ListingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyID);
            
            CreateTable(
                "dbo.PropertyDetailEstateAgents",
                c => new
                    {
                        PropertyDetail_PropertyID = c.Int(nullable: false),
                        EstateAgent_EstateAgentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PropertyDetail_PropertyID, t.EstateAgent_EstateAgentId })
                .ForeignKey("dbo.PropertyDetails", t => t.PropertyDetail_PropertyID, cascadeDelete: true)
                .ForeignKey("dbo.EstateAgents", t => t.EstateAgent_EstateAgentId, cascadeDelete: true)
                .Index(t => t.PropertyDetail_PropertyID)
                .Index(t => t.EstateAgent_EstateAgentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropertyDetailEstateAgents", "EstateAgent_EstateAgentId", "dbo.EstateAgents");
            DropForeignKey("dbo.PropertyDetailEstateAgents", "PropertyDetail_PropertyID", "dbo.PropertyDetails");
            DropIndex("dbo.PropertyDetailEstateAgents", new[] { "EstateAgent_EstateAgentId" });
            DropIndex("dbo.PropertyDetailEstateAgents", new[] { "PropertyDetail_PropertyID" });
            DropTable("dbo.PropertyDetailEstateAgents");
            DropTable("dbo.PropertyDetails");
            DropTable("dbo.EstateAgents");
        }
    }
}
