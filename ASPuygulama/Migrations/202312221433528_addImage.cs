namespace ASPuygulama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Image");
        }
    }
}
