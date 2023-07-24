namespace CreativeCollab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class neighbourhoods : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Neighbourhoods",
                c => new
                    {
                        NeighbourhoodId = c.Int(nullable: false, identity: true),
                        NeighbourhoodName = c.String(),
                    })
                .PrimaryKey(t => t.NeighbourhoodId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Neighbourhoods");
        }
    }
}
