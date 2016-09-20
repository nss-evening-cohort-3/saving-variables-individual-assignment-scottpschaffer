namespace SavingVariables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SaveVars",
                c => new
                    {
                        VarId = c.Int(nullable: false, identity: true),
                        VarName = c.String(nullable: false),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VarId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SaveVars");
        }
    }
}
