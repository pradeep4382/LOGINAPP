﻿namespace LoginApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TestLogins", newName: "LoginDetails");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.LoginDetails", newName: "TestLogins");
        }
    }
}
