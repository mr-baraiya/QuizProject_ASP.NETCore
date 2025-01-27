-- use QUIZ

CREATE TABLE [User] (
    UserID INT PRIMARY KEY IDENTITY(1,1), 
    UserName NVARCHAR(100) NOT NULL,      
    Password NVARCHAR(100) NOT NULL,      
    Email NVARCHAR(100) NOT NULL,         
    Mobile NVARCHAR(100) NOT NULL,        
    IsActive BIT NOT NULL,                
    Created DATETIME DEFAULT GETDATE(),   
    Modified DATETIME                     
);

INSERT INTO [User] (UserName, Password, Email, Mobile, IsActive, Created, Modified)
VALUES
('JohnDoe', 'Password123', 'johndoe@example.com', '1234567890', 1, GETDATE(), NULL),
('JaneSmith', 'Jane@2025', 'janesmith@example.com', '0987654321', 1, GETDATE(), NULL),
('MikeBrown', 'Mike#2025', 'mikebrown@example.com', '1122334455', 0, GETDATE(), NULL),
('EmilyClark', 'Emily@2025', 'emilyclark@example.com', '5566778899', 1, GETDATE(), NULL),
('ChrisEvans', 'Chris@123', 'chrisevans@example.com', '6677889900', 1, GETDATE(), NULL),
('SarahJohnson', 'Sarah2025!', 'sarahjohnson@example.com', '7788990011', 0, GETDATE(), NULL),
('DavidWilson', 'David2025*', 'davidwilson@example.com', '8899001122', 1, GETDATE(), NULL),
('LauraLee', 'Laura@2025!', 'lauralee@example.com', '9900112233', 1, GETDATE(), NULL),
('KevinWright', 'Kevin#2025', 'kevinwright@example.com', '1011121314', 0, GETDATE(), NULL),
('SophiaMiller', 'Sophia2025#', 'sophiamiller@example.com', '1413121110', 1, GETDATE(), NULL);

select * from [dbo].[User];

--1 select all from user
-- exec [dbo].[PR_User_SelectAll]
create or alter procedure [dbo].[PR_User_SelectAll] 
as 
begin
	select	*
	from [dbo].[User]
	ORDER BY [dbo].[User].UserName
end

--2 select all from user by pk
-- exec [dbo].[PR_User_SelectByPk] 1
create or alter procedure [dbo].[PR_User_SelectByPk]
@Userid int
as 
begin
	select * from [dbo].[User]
	where [dbo].[User].[UserID] = @Userid
end

--3 insert into user
-- exec [dbo].[PR_User_Insert] 'KevinWright', 'Kevin#2025', 'kevinwright@example.com', '1011121314', 0, NULL , NULL
CREATE OR ALTER PROCEDURE [dbo].[PR_User_Insert]
    @UserName NVARCHAR(100),
    @Password NVARCHAR(100),
    @Email NVARCHAR(100),
    @Mobile NVARCHAR(100),
    @IsActive BIT,
    @Created DATETIME,
    @Modified DATETIME
AS
BEGIN
    INSERT INTO [dbo].[User] (UserName, Password, Email, Mobile, IsActive, Created, Modified)
    VALUES (@UserName, @Password, @Email, @Mobile, @IsActive, @Created, @Modified);
END


--4 update user
-- exec [dbo].[PR_User_Update] 11, 'Kai', 'Kevin#2025', 'kevinwright@example.com', '1011121314', 0, NULL , NULL
CREATE OR ALTER PROCEDURE [dbo].[PR_User_Update]
    @Id INT,
    @UserName NVARCHAR(100),
    @Password NVARCHAR(100),
    @Email NVARCHAR(100),
    @Mobile NVARCHAR(100),
    @IsActive BIT,
    @Created DATETIME,
    @Modified DATETIME
AS
BEGIN
    UPDATE [dbo].[User]
    SET UserName = @UserName,
        Password = @Password,
        Email = @Email,
        Mobile = @Mobile,
        IsActive = @IsActive,
        Created = @Created,
        Modified = @Modified
    WHERE UserID = @Id;
END

--5 delete user
-- exec [dbo].[PR_User_Delete] 12
CREATE OR ALTER PROCEDURE [dbo].[PR_User_Delete]
    @Id INT
AS
BEGIN
    DELETE FROM [dbo].[User]
    WHERE UserID = @Id;
END
 