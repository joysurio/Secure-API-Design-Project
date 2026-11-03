-- One-time seed for dbo.ApplicationUsers (mobile admin list). Skips if the table already has rows.
-- To wipe the table and insert 5 fresh demo rows instead, run reset_application_users_seed_five.sql.

SET NOCOUNT ON;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'ApplicationUsers' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    RAISERROR ('Table dbo.ApplicationUsers not found.', 16, 1);
    RETURN;
END;

DECLARE @cnt INT = (SELECT COUNT(*) FROM dbo.ApplicationUsers);

IF @cnt > 0
BEGIN
    SELECT @cnt AS ExistingRowCount, N'Skip seed — table already has rows.' AS Message;
    RETURN;
END;

INSERT INTO dbo.ApplicationUsers (
    [Name],
    SurName1,
    SurName2,
    Sex,
    Deleted,
    CreationDate,
    FinishDate,
    RFIDCode,
    Observations,
    Personalized,
    NeverDeleted,
    IdEmployee,
    DNI
)
VALUES
(
    N'Showroom',
    N'Admin',
    N'Test',
    N'M',
    0,
    SYSUTCDATETIME(),
    NULL,
    N'',
    N'Seed (agent): admin when SubCliente DNI matches 00000000T',
    0,
    0,
    90001,
    N'00000000T'
),
(
    N'Showroom',
    N'Operator',
    N'Two',
    N'F',
    0,
    SYSUTCDATETIME(),
    NULL,
    N'',
    N'Seed (agent): admin by IdEmployee 90002',
    0,
    0,
    90002,
    NULL
);

SELECT COUNT(*) AS ApplicationUsersRowsAfterSeed FROM dbo.ApplicationUsers;
SELECT TOP 5 Id, [Name], SurName1, IdEmployee, DNI, Deleted FROM dbo.ApplicationUsers ORDER BY Id DESC;
