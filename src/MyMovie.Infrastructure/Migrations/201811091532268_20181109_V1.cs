namespace MyMovie.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20181109_V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        MovieId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsAnonymous = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Conetnt = c.String(),
                        MovieId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ParId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dictionary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Abbreviation = c.String(),
                        DelFlag = c.Int(nullable: false),
                        ParId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movie_Info",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Director = c.String(),
                        Language = c.String(),
                        ReleaseDate = c.String(),
                        Duration = c.String(),
                        Alias = c.String(),
                        Description = c.String(),
                        Rate = c.Single(nullable: false),
                        PosterURL = c.String(),
                        DelFlag = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movie_Type",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                        DelFlag = c.String(),
                        Sort = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Gender = c.String(),
                        BirthDay = c.DateTime(nullable: false),
                        TelNumber = c.String(),
                        NickName = c.String(),
                        Bio = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInfo");
            DropTable("dbo.Types");
            DropTable("dbo.Movie_Type");
            DropTable("dbo.Movie_Info");
            DropTable("dbo.Dictionary");
            DropTable("dbo.Comment");
            DropTable("dbo.Article");
        }
    }
}
