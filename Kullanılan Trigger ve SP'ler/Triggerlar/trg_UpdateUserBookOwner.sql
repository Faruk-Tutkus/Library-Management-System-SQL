Update dbo.[User] set BookOwner = NULL
Update dbo.[Books] set BookOwner = NULL
CREATE TRIGGER trg_UpdateUserBookOwner
ON dbo.[User]
AFTER UPDATE
AS
BEGIN
    IF (TRIGGER_NESTLEVEL() > 1)
        RETURN;

    DECLARE @bookName nvarchar(50), @loginName nvarchar(50)

    SELECT @bookName = INSERTED.BookOwner, @loginName = INSERTED.LoginName
    FROM INSERTED

    UPDATE Books
    SET BookOwner = @loginName
    WHERE BookName = @bookName
END
