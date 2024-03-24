CREATE PROCEDURE SearchUser
    @LoginName NVARCHAR(50)
AS
BEGIN
    SELECT * FROM dbo.[User] WHERE LoginName LIKE '%' + @LoginName + '%'
END
