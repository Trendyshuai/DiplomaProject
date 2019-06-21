namespace MyMovie.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20181116_V1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "Text", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Article", "Text");
        }
    }
}
