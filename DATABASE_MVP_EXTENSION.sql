/*
  MVP extension required by "API continuation" brief.
  Adds refresh/session-adjacent support tables used by new endpoints:
  - POST /api/notifications/register -> DeviceTokens
  - POST /api/support/tickets        -> SupportTickets
*/

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeviceTokens]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[DeviceTokens] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [UserId] NVARCHAR(128) NOT NULL,
        [Platform] NVARCHAR(20) NOT NULL,
        [Token] NVARCHAR(500) NOT NULL,
        [CreatedAtUtc] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        [UpdatedAtUtc] DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
    CREATE UNIQUE INDEX UX_DeviceTokens_User_Platform ON [dbo].[DeviceTokens]([UserId], [Platform]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SupportTickets]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SupportTickets] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [TicketGuid] NVARCHAR(64) NOT NULL,
        [UserId] NVARCHAR(128) NOT NULL,
        [MachineId] INT NULL,
        [Subject] NVARCHAR(200) NOT NULL,
        [Description] NVARCHAR(2000) NOT NULL,
        [ImageUrl] NVARCHAR(1000) NULL,
        [Status] NVARCHAR(50) NOT NULL,
        [CreatedAtUtc] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        [HandledBy] NVARCHAR(128) NULL
    );
    CREATE UNIQUE INDEX UX_SupportTickets_TicketGuid ON [dbo].[SupportTickets]([TicketGuid]);
    CREATE INDEX IX_SupportTickets_UserId ON [dbo].[SupportTickets]([UserId]);
    CREATE INDEX IX_SupportTickets_CreatedAtUtc ON [dbo].[SupportTickets]([CreatedAtUtc]);
END
GO
