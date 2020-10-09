namespace LoginApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestLogins", "FirstNames", c => c.String());
            DropColumn("dbo.TestLogins", "FirstName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestLogins", "FirstName", c => c.String());
            DropColumn("dbo.TestLogins", "FirstNames");
        }
    }
}
