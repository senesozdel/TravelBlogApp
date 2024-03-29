namespace ASPuygulama.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isLogin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsLogin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsLogin");
        }
    }
}
