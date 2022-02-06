namespace Restaurant_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class myFirstMigrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.String(),
                        Day = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        isAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MealName = c.String(),
                        Day = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        meal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Meal", t => t.meal_Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.meal_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "UserId", "dbo.User");
            DropForeignKey("dbo.Order", "meal_Id", "dbo.Meal");
            DropForeignKey("dbo.Meal", "UserId", "dbo.User");
            DropIndex("dbo.Order", new[] { "meal_Id" });
            DropIndex("dbo.Order", new[] { "UserId" });
            DropIndex("dbo.Meal", new[] { "UserId" });
            DropTable("dbo.Order");
            DropTable("dbo.User");
            DropTable("dbo.Meal");
        }
    }
}
