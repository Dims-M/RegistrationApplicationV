namespace RegistrationApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbClient", "imagePathInDoc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbClient", "imagePathInDoc");
        }
    }
}
