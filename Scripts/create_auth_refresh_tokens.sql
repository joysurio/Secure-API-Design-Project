-- Mobile / interactive refresh tokens (dbo.AuthRefreshTokens).
-- Run once per database used by LFS.SecureAPI after deploying persistent refresh-token support.

IF NOT EXISTS (
    SELECT 1 FROM sys.tables t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = N'AuthRefreshTokens' AND s.name = N'dbo')
BEGIN
    CREATE TABLE dbo.AuthRefreshTokens (
        Token        VARCHAR(64)     NOT NULL,
        Subject      NVARCHAR(450)   NOT NULL,
        ExpiresAtUtc DATETIME2       NOT NULL,
        Revoked      BIT             NOT NULL CONSTRAINT DF_AuthRefreshTokens_Revoked DEFAULT (0),
        CreatedAtUtc DATETIME2       NOT NULL CONSTRAINT DF_AuthRefreshTokens_CreatedAtUtc DEFAULT (SYSUTCDATETIME()),
        CONSTRAINT PK_AuthRefreshTokens PRIMARY KEY CLUSTERED (Token)
    );

    CREATE NONCLUSTERED INDEX IX_AuthRefreshTokens_Revoked_Expires
        ON dbo.AuthRefreshTokens (Revoked, ExpiresAtUtc);
END
GO
