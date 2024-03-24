CREATE PROCEDURE SearchBook
    @BookName NVARCHAR(50),
    @Role BIT
AS
BEGIN
    IF @Role = 1
        SELECT * FROM Books WHERE BookName LIKE '%' + @BookName + '%'
    ELSE
        SELECT BookName, BookGenre, BookAuthor FROM Books WHERE BookName LIKE '%' + @BookName + '%'
END
