namespace RegistrationApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColonEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbClient", "email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbClient", "email");
        }
    }
}
