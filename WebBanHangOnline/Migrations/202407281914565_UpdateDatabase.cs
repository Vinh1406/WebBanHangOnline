namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Posts", "SeoTitle", c => c.String());
            AddColumn("dbo.tb_Posts", "SeoDescription", c => c.String());
            AddColumn("dbo.tb_Posts", "SeoKeywords", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Posts", "SeoKeywords");
            DropColumn("dbo.tb_Posts", "SeoDescription");
            DropColumn("dbo.tb_Posts", "SeoTitle");
        }
    }
}
