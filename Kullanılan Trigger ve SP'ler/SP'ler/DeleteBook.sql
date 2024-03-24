CREATE PROCEDURE DeleteBook
    @BookId INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Books WHERE BookId = @BookId AND BookOwner IS NULL)
        DELETE FROM Books WHERE BookId = @BookId
    ELSE
        SELECT 'Kitap sahibi oldu�u i�in kitap silinemez.' AS Message
END
