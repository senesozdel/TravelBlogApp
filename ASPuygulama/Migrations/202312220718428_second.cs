namespace ASPuygulama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogCityRelations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        BlogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogPosts", t => t.BlogId)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId)
                .Index(t => t.BlogId);
            
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        PostDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BlogComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlogId = c.Int(nullable: false),
                        CommentContent = c.String(),
                        UserId = c.Int(nullable: false),
                        CommentTime = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogPosts", t => t.BlogId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.BlogId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        SignDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlogLikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BlogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogPosts", t => t.BlogId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BlogId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogCityRelations", "CityId", "dbo.Cities");
            DropForeignKey("dbo.BlogCityRelations", "BlogId", "dbo.BlogPosts");
            DropForeignKey("dbo.BlogPosts", "UserId", "dbo.Users");
            DropForeignKey("dbo.BlogComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.BlogLikes", "UserId", "dbo.Users");
            DropForeignKey("dbo.BlogLikes", "BlogId", "dbo.BlogPosts");
            DropForeignKey("dbo.BlogComments", "BlogId", "dbo.BlogPosts");
            DropIndex("dbo.BlogLikes", new[] { "BlogId" });
            DropIndex("dbo.BlogLikes", new[] { "UserId" });
            DropIndex("dbo.BlogComments", new[] { "UserId" });
            DropIndex("dbo.BlogComments", new[] { "BlogId" });
            DropIndex("dbo.BlogPosts", new[] { "UserId" });
            DropIndex("dbo.BlogCityRelations", new[] { "BlogId" });
            DropIndex("dbo.BlogCityRelations", new[] { "CityId" });
            DropTable("dbo.Cities");
            DropTable("dbo.BlogLikes");
            DropTable("dbo.Users");
            DropTable("dbo.BlogComments");
            DropTable("dbo.BlogPosts");
            DropTable("dbo.BlogCityRelations");
        }
    }
}
