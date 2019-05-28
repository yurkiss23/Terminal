namespace Terminal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fname = c.String(),
                        Lname = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "Money", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Money");
            DropTable("dbo.Admins");
        }
    }
}
