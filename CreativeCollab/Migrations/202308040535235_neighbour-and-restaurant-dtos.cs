namespace CreativeCollab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class neighbourandrestaurantdtos : DbMigration
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
                "dbo.Neighbourhoods",
                c => new
                    {
                        NeighbourhoodId = c.Int(nullable: false, identity: true),
                        NeighbourhoodName = c.String(),
                    })
                .PrimaryKey(t => t.NeighbourhoodId);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false, identity: true),
                        RestaurantName = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        NeighbourhoodId = c.Int(nullable: false),
                        RestaurantLink = c.String(),
                    })
                .PrimaryKey(t => t.RestaurantId)
                .ForeignKey("dbo.Neighbourhoods", t => t.NeighbourhoodId, cascadeDelete: true)
                .Index(t => t.NeighbourhoodId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Restaurants", "NeighbourhoodId", "dbo.Neighbourhoods");
            DropForeignKey("dbo.PropertyDetailEstateAgents", "EstateAgent_EstateAgentId", "dbo.EstateAgents");
            DropForeignKey("dbo.PropertyDetailEstateAgents", "PropertyDetail_PropertyID", "dbo.PropertyDetails");
            DropIndex("dbo.PropertyDetailEstateAgents", new[] { "EstateAgent_EstateAgentId" });
            DropIndex("dbo.PropertyDetailEstateAgents", new[] { "PropertyDetail_PropertyID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Restaurants", new[] { "NeighbourhoodId" });
            DropTable("dbo.PropertyDetailEstateAgents");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Neighbourhoods");
            DropTable("dbo.PropertyDetails");
            DropTable("dbo.EstateAgents");
        }
    }
}
