namespace LoginApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestLogins", "LastName", c => c.String());
            DropColumn("dbo.TestLogins", "LastNames");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestLogins", "LastNames", c => c.String());
            DropColumn("dbo.TestLogins", "LastName");
        }
    }
}
