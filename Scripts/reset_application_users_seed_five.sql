-- Destructive: DELETE all rows from dbo.ApplicationUsers, then INSERT 5 demo admins.
-- Run against the correct database (dev/staging/prod) only after backup if needed.
-- If DELETE fails with a foreign-key error, remove or update child rows that reference ApplicationUsers.Id first.

SET NOCOUNT ON;
SET XACT_ABORT ON;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'ApplicationUsers' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    RAISERROR ('Table dbo.ApplicationUsers not found.', 16, 1);
    RETURN;
END;

BEGIN TRANSACTION;

DELETE FROM dbo.ApplicationUsers;

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
    N'Demo',
    N'Admin',
    N'One',
    N'M',
    0,
    SYSUTCDATETIME(),
    NULL,
    N'',
    N'Reset script: QR userId = DNI 80000001A or IdEmployee 92001',
    0,
    0,
    92001,
    N'80000001A'
),
(
    N'Demo',
    N'Admin',
    N'Two',
    N'F',
    0,
    SYSUTCDATETIME(),
    NULL,
    N'',
    N'Reset script: QR userId = DNI 80000002B or IdEmployee 92002',
    0,
    0,
    92002,
    N'80000002B'
),
(
    N'Demo',
    N'Admin',
    N'Three',
    N'M',
    0,
    SYSUTCDATETIME(),
    NULL,
    N'',
    N'Reset script: QR by DNI only 80000003C',
    0,
    0,
    NULL,
    N'80000003C'
),
(
    N'Demo',
    N'Admin',
    N'Four',
    N'F',
    0,
    SYSUTCDATETIME(),
    NULL,
    N'',
    N'Reset script: QR by IdEmployee only 92004',
    0,
    0,
    92004,
    NULL
),
(
    N'Demo',
    N'Admin',
    N'Five',
    N'M',
    0,
    SYSUTCDATETIME(),
    NULL,
    N'',
    N'Reset script: QR userId = DNI 80000005D or IdEmployee 92005',
    0,
    0,
    92005,
    N'80000005D'
);

COMMIT TRANSACTION;

SELECT COUNT(*) AS ApplicationUsersRowCount FROM dbo.ApplicationUsers;
SELECT Id, [Name], SurName1, IdEmployee, DNI, Deleted, Observations
FROM dbo.ApplicationUsers
ORDER BY Id;
