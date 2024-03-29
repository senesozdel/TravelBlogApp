namespace ASPuygulama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPosts", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogPosts", "Image");
        }
    }
}
