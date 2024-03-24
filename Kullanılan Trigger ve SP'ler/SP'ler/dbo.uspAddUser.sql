CREATE TABLE dbo.[User]
(
  UserID INT IDENTITY (1,1) NOT NULL,
  LoginName NVARCHAR (40) NOT NULL,
  PasswordHash BINARY (64) NOT NULL,
  BookOwner NVARCHAR (40) NULL,
  Role BIT NOT NULL DEFAULT 0,
  CONSTRAINT [PK_User_UserID] PRIMARY KEY CLUSTERED (UserID ASC)
)
GO

CREATE PROCEDURE dbo.uspAddUser
  @pLogin NVARCHAR (40),
  @pPassword NVARCHAR (50),
  @pBookOwner NVARCHAR (40),
  @pRole BIT,
  @responseMessage NVARCHAR (250) OUTPUT
AS
BEGIN
  SET NOCOUNT ON
  BEGIN TRY
    INSERT INTO dbo.[User] (LoginName, PasswordHash, BookOwner, Role)
    VALUES (@pLogin, HASHBYTES ('SHA2_512', @pPassword), @pBookOwner, @pRole)
    SET @responseMessage='Success'
  END TRY
  BEGIN CATCH
    SET @responseMessage=ERROR_MESSAGE ()
  END CATCH
END
GO
ALTER TABLE dbo.[User] ADD CONSTRAINT UNQ_User_LoginName UNIQUE (LoginName);

