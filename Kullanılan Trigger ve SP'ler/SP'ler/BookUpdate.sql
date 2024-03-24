CREATE PROCEDURE UpdateBook
    @BookId INT,
    @BookName NVARCHAR(50),
    @BookGenre NVARCHAR(50),
    @BookAuthor NVARCHAR(50)
AS
BEGIN
    UPDATE Books
    SET BookName = @BookName, BookGenre = @BookGenre, BookAuthor = @BookAuthor
    WHERE BookId = @BookId
END
