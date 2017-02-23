using Project.DataAccess.Database1.Files.Migrations.Utils;

namespace Project.DataAccess.Database1.Files.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            Sql(FileStreamTableCreator.PrepareDbSql, true);
            Sql(FileStreamTableCreator.TableSql, true);

            CreateTable(
                "dbo.BinaryMeta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        Name = c.String(),
                        Size = c.Long(nullable: false),
                        Storage = c.Byte(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.File1MData",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Data = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.File1MData");
            DropTable("dbo.BinaryMeta");
        }
    }
}
