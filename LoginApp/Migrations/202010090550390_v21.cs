namespace LoginApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestLogins", "FirstName", c => c.String());
            DropColumn("dbo.TestLogins", "FirstNames");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestLogins", "FirstNames", c => c.String());
            DropColumn("dbo.TestLogins", "FirstName");
        }
    }
}
