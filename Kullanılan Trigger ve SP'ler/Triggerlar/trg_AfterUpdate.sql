CREATE TRIGGER trg_AfterUpdate
ON Books
AFTER UPDATE
AS
BEGIN
    UPDATE Books
    SET UpdateDate = GETDATE()
    WHERE BookId IN (SELECT BookId FROM inserted);
END;
