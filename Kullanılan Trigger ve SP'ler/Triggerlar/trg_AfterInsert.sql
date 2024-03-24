CREATE TRIGGER trg_AfterInsert
ON Books
AFTER INSERT
AS
BEGIN
    UPDATE Books
    SET RegisterDate = GETDATE()
    WHERE BookId IN (SELECT BookId FROM inserted);
END;