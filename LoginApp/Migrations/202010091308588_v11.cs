namespace LoginApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestLogins", "LastNames", c => c.String());
            DropColumn("dbo.TestLogins", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestLogins", "LastName", c => c.String());
            DropColumn("dbo.TestLogins", "LastNames");
        }
    }
}
