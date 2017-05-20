namespace PuzzleBGlossary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Glossaries",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Term = c.String(nullable: false),
                    Definition = c.String(nullable: false),
                })
                .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
            DropTable("dbo.Glossaries");
        }
    }
}
