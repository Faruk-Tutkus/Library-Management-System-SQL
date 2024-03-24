CREATE PROCEDURE AddBook
    @BookName NVARCHAR(50),
    @BookGenre NVARCHAR(50),
    @BookAuthor NVARCHAR(50)
AS
BEGIN
    INSERT INTO Books (BookName, BookGenre, BookAuthor)
    VALUES (
        CONCAT(UPPER(LEFT(@BookName, 1)), LOWER(SUBSTRING(@BookName, 2, LEN(@BookName)))),
        CONCAT(UPPER(LEFT(@BookGenre, 1)), LOWER(SUBSTRING(@BookGenre, 2, LEN(@BookGenre)))),
        CONCAT(UPPER(LEFT(@BookAuthor, 1)), LOWER(SUBSTRING(@BookAuthor, 2, LEN(@BookAuthor))))
    )
END
