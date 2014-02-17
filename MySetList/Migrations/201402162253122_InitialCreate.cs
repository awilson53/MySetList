namespace MySetList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChordCharts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StoragePath = c.String(),
                        ChartID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ChordCharts");
        }
    }
}
