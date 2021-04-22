namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'24c04501-7d58-4ec0-a4fa-c71871afe9ac', N'guest@vidly.com', 0, N'APC0GShPK6448jxeoCY7zvGvRDgjwmpXCpvce+xefRgNi9Q/umceBEb6hfYrdbrLAg==', N'c124fa62-a115-408b-a85a-6b5bc176d632', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9d5d10b1-f927-41e7-9da7-115b6a4cba16', N'admin@vidly.com', 0, N'APJbkbwc2tkHik5jbM/PEGGDAowKmtk4v2uPiFIvpMN/1+B5Segapy7kG0HvMXFfTg==', N'8549c5b9-5117-4e12-8544-dfb760f8a19c', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'17532711-7f7a-4abe-95f1-d70451771fa9', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9d5d10b1-f927-41e7-9da7-115b6a4cba16', N'17532711-7f7a-4abe-95f1-d70451771fa9')
");
        }
        
        public override void Down()
        {
        }
    }
}
