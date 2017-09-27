namespace ErrorLoggerModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        appId = c.Int(nullable: false, identity: true),
                        appName = c.String(nullable: false, maxLength: 50),
                        appType = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.appId);
            
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        logID = c.Int(nullable: false, identity: true),
                        Application_appId = c.Int(nullable: false),
                        LogStatus_logStatusID = c.Int(),
                        Users_userId = c.Int(),
                    })
                .PrimaryKey(t => t.logID)
                .ForeignKey("dbo.Applications", t => t.Application_appId, cascadeDelete: true)
                .ForeignKey("dbo.LogStatus", t => t.LogStatus_logStatusID)
                .ForeignKey("dbo.Users", t => t.Users_userId)
                .Index(t => t.Application_appId)
                .Index(t => t.LogStatus_logStatusID)
                .Index(t => t.Users_userId);
            
            CreateTable(
                "dbo.LogStatus",
                c => new
                    {
                        logStatusID = c.Int(nullable: false, identity: true),
                        logStatusDesc = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.logStatusID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userId = c.Int(nullable: false, identity: true),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        lastLogin = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.userId);
            
            CreateTable(
                "dbo.UsersApplications",
                c => new
                    {
                        Users_userId = c.Int(nullable: false),
                        Application_appId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Users_userId, t.Application_appId })
                .ForeignKey("dbo.Users", t => t.Users_userId, cascadeDelete: true)
                .ForeignKey("dbo.Applications", t => t.Application_appId, cascadeDelete: true)
                .Index(t => t.Users_userId)
                .Index(t => t.Application_appId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Errors", "Users_userId", "dbo.Users");
            DropForeignKey("dbo.UsersApplications", "Application_appId", "dbo.Applications");
            DropForeignKey("dbo.UsersApplications", "Users_userId", "dbo.Users");
            DropForeignKey("dbo.Errors", "LogStatus_logStatusID", "dbo.LogStatus");
            DropForeignKey("dbo.Errors", "Application_appId", "dbo.Applications");
            DropIndex("dbo.UsersApplications", new[] { "Application_appId" });
            DropIndex("dbo.UsersApplications", new[] { "Users_userId" });
            DropIndex("dbo.Errors", new[] { "Users_userId" });
            DropIndex("dbo.Errors", new[] { "LogStatus_logStatusID" });
            DropIndex("dbo.Errors", new[] { "Application_appId" });
            DropTable("dbo.UsersApplications");
            DropTable("dbo.Users");
            DropTable("dbo.LogStatus");
            DropTable("dbo.Errors");
            DropTable("dbo.Applications");
        }
    }
}
