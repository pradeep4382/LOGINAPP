using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LoginApp.Models;

namespace LoginApp.DataAccess
{
    public class LoginContext : DbContext
    {
        public LoginContext() : base("name=Mystring")
        {
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<LoginContext, Migrations.Configuration>());
        }

        
        public DbSet<LoginDetails> TestLogins { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}