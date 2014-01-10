namespace MusicOre.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        DeviceId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.DeviceId);
            
            CreateTable(
                "dbo.RootFolders",
                c => new
                    {
                        RootFolderId = c.Int(nullable: false, identity: true),
                        IsCloud = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 4000),
                        Path = c.String(maxLength: 4000),
                        Devices_RootFolderId = c.Int(),
                        Device_DeviceId = c.Int(),
                    })
                .PrimaryKey(t => t.RootFolderId)
                .ForeignKey("dbo.RootFolders", t => t.Devices_RootFolderId)
                .ForeignKey("dbo.Devices", t => t.Device_DeviceId)
                .Index(t => t.Devices_RootFolderId)
                .Index(t => t.Device_DeviceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RootFolders", "Device_DeviceId", "dbo.Devices");
            DropForeignKey("dbo.RootFolders", "Devices_RootFolderId", "dbo.RootFolders");
            DropIndex("dbo.RootFolders", new[] { "Device_DeviceId" });
            DropIndex("dbo.RootFolders", new[] { "Devices_RootFolderId" });
            DropTable("dbo.RootFolders");
            DropTable("dbo.Devices");
        }
    }
}
