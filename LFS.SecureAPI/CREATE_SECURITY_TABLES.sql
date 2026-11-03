-- SQL Script to Create Security Tables for LFS Secure API
-- Run this script on your database: Laundry_ShowRoom
-- Server: 35.181.48.101\SQLEXPRESS,1433

USE [Laundry_ShowRoom];
GO

-- Create Devices Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Devices]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Devices] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [DeviceId] NVARCHAR(100) NOT NULL UNIQUE,
        [SecretHash] NVARCHAR(256) NOT NULL,
        [OwnerClientId] INT NULL,
        [AllowedScopes] NVARCHAR(MAX) NULL,
        [IsActive] BIT NOT NULL DEFAULT 1,
        [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        [LastSeen] DATETIME2 NULL
    );
    
    CREATE UNIQUE INDEX IX_Devices_DeviceId ON [Devices]([DeviceId]);
    PRINT 'Table [Devices] created successfully.';
END
ELSE
BEGIN
    PRINT 'Table [Devices] already exists.';
END
GO

-- Create AccessLog Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccessLog]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[AccessLog] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [TimestampUtc] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        [UserId] INT NULL,
        [DeviceId] NVARCHAR(100) NULL,
        [Endpoint] NVARCHAR(200) NOT NULL,
        [Method] NVARCHAR(10) NOT NULL,
        [ParamsJson] NVARCHAR(MAX) NULL,
        [Success] BIT NOT NULL,
        [ResponseStatus] INT NOT NULL,
        [IpAddress] NVARCHAR(45) NULL
    );
    
    CREATE INDEX IX_AccessLog_TimestampUtc ON [AccessLog]([TimestampUtc]);
    CREATE INDEX IX_AccessLog_DeviceId ON [AccessLog]([DeviceId]);
    CREATE INDEX IX_AccessLog_UserId ON [AccessLog]([UserId]);
    PRINT 'Table [AccessLog] created successfully.';
END
ELSE
BEGIN
    PRINT 'Table [AccessLog] already exists.';
END
GO

-- Create OperationsLog Table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OperationsLog]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[OperationsLog] (
        [OperationId] UNIQUEIDENTIFIER PRIMARY KEY,
        [DeviceId] NVARCHAR(100) NULL,
        [UserId] INT NULL,
        [Endpoint] NVARCHAR(200) NOT NULL,
        [PayloadHash] NVARCHAR(64) NOT NULL,
        [ProcessedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
    
    CREATE INDEX IX_OperationsLog_DeviceId ON [OperationsLog]([DeviceId]);
    CREATE INDEX IX_OperationsLog_ProcessedAt ON [OperationsLog]([ProcessedAt]);
    PRINT 'Table [OperationsLog] created successfully.';
END
ELSE
BEGIN
    PRINT 'Table [OperationsLog] already exists.';
END
GO

PRINT 'All security tables have been created or verified.';
GO
