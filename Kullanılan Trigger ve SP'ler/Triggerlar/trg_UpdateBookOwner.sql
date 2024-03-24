CREATE TRIGGER trg_UpdateBookOwner
ON Books
AFTER UPDATE
AS
BEGIN
    DECLARE @bookName nvarchar(50), @loginName nvarchar(50)

    SELECT @bookName = INSERTED.BookName, @loginName = INSERTED.BookOwner
    FROM INSERTED

    UPDATE dbo.[User]
    SET BookOwner = @bookName
    WHERE LoginName = @loginName
END
