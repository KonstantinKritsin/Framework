namespace Project.DataAccess.Database1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 255),
                        Name = c.String(maxLength: 500),
                        Email = c.String(maxLength: 255),
                        Lang = c.String(maxLength: 10),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        LastActivityDate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
        }
    }
}
