namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_ProductCategory", "Image", c => c.String(maxLength: 250));
            DropColumn("dbo.tb_ProductCategory", "Icon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_ProductCategory", "Icon", c => c.String(maxLength: 250));
            DropColumn("dbo.tb_ProductCategory", "Image");
        }
    }
}
