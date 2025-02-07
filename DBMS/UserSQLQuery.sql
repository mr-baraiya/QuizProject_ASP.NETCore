-- use QUIZ

CREATE TABLE [MST_User] (
    [UserID]		INT PRIMARY KEY IDENTITY(1,1)
    ,[UserName]		NVARCHAR(100) NOT NULL      
    ,[Password]		NVARCHAR(100) NOT NULL      
    ,[Email]		NVARCHAR(100) NOT NULL         
    ,[Mobile]		NVARCHAR(100) NOT NULL     
    ,[IsActive]		BIT NOT NULL DEFAULT 1
	,[IsAdmin]		BIT NOT NULL DEFAULT 0
    ,[Created]		DATETIME DEFAULT GETDATE()   
    ,[Modified]		DATETIME                     
);

--INSERT INTO [MST_User]
--VALUES
--('JohnDoe', 'Password123', 'johndoe@example.com', '1234567890', 1, 1, GETDATE(), NULL),
--('JaneSmith', 'Jane@2025', 'janesmith@example.com', '0987654321', 1, 1, GETDATE(), NULL),
--('MikeBrown', 'Mike#2025', 'mikebrown@example.com', '1122334455', 0, 0, GETDATE(), NULL),
--('EmilyClark', 'Emily@2025', 'emilyclark@example.com', '5566778899', 1, 0, GETDATE(), NULL),
--('ChrisEvans', 'Chris@123', 'chrisevans@example.com', '6677889900', 1, 0, GETDATE(), NULL),
--('SarahJohnson', 'Sarah2025!', 'sarahjohnson@example.com', '7788990011', 0, 0, GETDATE(), NULL),
--('DavidWilson', 'David2025*', 'davidwilson@example.com', '8899001122', 1, 0, GETDATE(), NULL),
--('LauraLee', 'Laura@2025!', 'lauralee@example.com', '9900112233', 1, 0, GETDATE(), NULL),
--('KevinWright', 'Kevin#2025', 'kevinwright@example.com', '1011121314', 0, 0, GETDATE(), NULL),
--('SophiaMiller', 'Sophia2025#', 'sophiamiller@example.com', '1413121110', 1, 0, GETDATE(), NULL);

--select * from [dbo].[MST_User];

-------------------------------------MST_User--------------------------------

	------------------SelectAll-------------------------
	--EXEC [dbo].[PR_MST_User_SelectAll]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_SelectAll]
	AS
	BEGIN
		SELECT 
			[dbo].[MST_User].[UserID]
			,[dbo].[MST_User].[UserName]
			,[dbo].[MST_User].[Password]
			,[dbo].[MST_User].[Email]
			,[dbo].[MST_User].[Mobile]
			,[dbo].[MST_User].[IsActive]
			,[dbo].[MST_User].[IsAdmin]
			,[dbo].[MST_User].[Created]
			,[dbo].[MST_User].[Modified]
		FROM [dbo].[MST_User]
		ORDER BY [dbo].[MST_User].[UserName]
	END

	------------------SelectByPK-------------------------
	--EXEC [dbo].[PR_MST_User_SelectByPK] 1
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_SelectByPK]
		@userid INT
	AS
	BEGIN
		SELECT 
			[dbo].[MST_User].[UserID]
			,[dbo].[MST_User].[UserName]
			,[dbo].[MST_User].[Password]
			,[dbo].[MST_User].[Email]
			,[dbo].[MST_User].[Mobile]
			,[dbo].[MST_User].[IsActive]
			,[dbo].[MST_User].[IsAdmin]
			,[dbo].[MST_User].[Created]
			,[dbo].[MST_User].[Modified]
		FROM [dbo].[MST_User]
		WHERE [dbo].[MST_User].[UserID] = @userid
	END

	------------------INSERT-------------------------
	--EXEC [dbo].[PR_MST_User_Insert] 'SophiaMiller', 'Sophia2025#', 'sophiamiller@example.com', '1413121110', 0
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_Insert]
		@username		NVARCHAR(100)
		,@password		NVARCHAR(100)
		,@email			NVARCHAR(100)
		,@mobile		NVARCHAR(100)
		,@isadmin		BIT
	AS
	BEGIN
		INSERT INTO [dbo].[MST_User]
		(
			[dbo].[MST_User].[UserName]
			,[dbo].[MST_User].[Password] 
			,[dbo].[MST_User].[Email]
			,[dbo].[MST_User].[Mobile] 
			,[dbo].[MST_User].[IsAdmin]
		)
		VALUES (
			@username
			,@password 
			,@email
			,@mobile 
			,@isadmin
		)
	END

	------------------UPDATE-------------------------
	--EXEC [dbo].[PR_MST_User_Update] 11 , 'Sophia Miller', 'Sophia2025#', 'sophiamiller@example.com', '1413121110', 1, 0
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_Update]
		@userid			INT
		,@username		NVARCHAR(100)
		,@password		NVARCHAR(100)
		,@email			NVARCHAR(100)
		,@mobile		NVARCHAR(100)
		,@isactive		BIT
		,@isadmin		BIT
	AS
	BEGIN
		UPDATE [dbo].[MST_User]
		SET
			[dbo].[MST_User].[UserName]		=	@username
			,[dbo].[MST_User].[Password]	=	@password
			,[dbo].[MST_User].[Email]		=	@email
			,[dbo].[MST_User].[Mobile]		=	@mobile
			,[dbo].[MST_User].[IsActive]	=	@isactive
			,[dbo].[MST_User].[IsAdmin]		=	@isadmin
			,[dbo].[MST_User].[Modified]	=	GETDATE()
		WHERE [dbo].[MST_User].[UserID]		=	@userid
	END

	------------------DELETE-------------------------
	
-- Soft Deletes a user from MST_User table based on primary key (UserID).
--	exec PR_MST_User_Delete 11

create or alter procedure [dbo].[PR_MST_User_Delete]
@userID int
as
begin
		update [dbo].[MST_User]
		set [dbo].[MST_User].IsActive = 0
		where [dbo].[MST_User].[UserID] = @userID;
end

--	Hard Deletes a user record from MST_User table based on primary key (UserID).
--	exec PR_MST_User_Permenant_Delete 10

create or alter procedure [dbo].[PR_MST_User_Permenant_Delete]
@userID int
as
begin             
		delete
		from [dbo].[MST_User]
		where [dbo].[MST_User].[UserID] = @userID
end

	-----------------SelectByUserNamePassword-------------------------
	--EXEC [dbo].[PR_MST_User_SelectByUserNamePassword] 'JohnDoe', 'Password123'
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_SelectByUserNamePassword]
		@username	NVARCHAR(100)
		,@password	NVARCHAR(100)
	AS
	BEGIN
		SELECT
			[dbo].[MST_User].[UserName]
			,[dbo].[MST_User].[Password]
		FROM [dbo].[MST_User]
		WHERE (
				[dbo].[MST_User].[UserName]		=	 @username OR 
				[dbo].[MST_User].[Email]		=	 @username OR 
				[dbo].[MST_User].[Mobile]		=	 @username
			) 
			AND 
			[dbo].[MST_User].[Password]			=	 @password
	END

	
 ----------------------------------------------------------------- Procedure to fetch QuestionLevel dropdown (in Question Table) data from MST_QuestionLevel table -----------------------------------------------------------------

 -- Fetches QuestionLevelID and QuestionLevel for dropdown population in the form.

-- exec PR_MST_QuestionLevel_FillDropdown
create or alter procedure [dbo].[PR_MST_QuestionLevel_FillDropdown]
as
begin
		select	[dbo].[MST_QuestionLevel].[QuestionLevelID],
				[dbo].[MST_QuestionLevel].[QuestionLevel]
		from [dbo].[MST_QuestionLevel]
		order by [dbo].[MST_QuestionLevel].[QuestionLevel]
end

-------------------------------------------------------------------- Procedure to Fetch All Quizzes for Quiz Dropdown in QuizWiseQuestions Form --------------------------------------------------------------------

-- This procedure fetches all Quiz IDs and their corresponding Quiz Names from the MST_Quiz table.

-- exec PR_MST_QuizWiseQuestions_Fill_Quiz_Dropdown
create or alter procedure [dbo].[PR_MST_QuizWiseQuestions_Fill_Quiz_Dropdown]
as
begin
		select	 [dbo].[MST_Quiz].[QuizID],
				 [dbo].[MST_Quiz].[QuizName]
		from [dbo].[MST_Quiz]
end

-------------------------------------------------------- Procedure to Fetch All Questions for Question Dropdown in QuizWiseQuestions Form----------------------------------------------------------

-- This procedure fetches all the questions with their corresponding question level.

-- exec PR_MST_QuizWiseQuestions_Fill_Question_Dropdown
create or alter procedure [dbo].[PR_MST_QuizWiseQuestions_Fill_Question_Dropdown]
as
begin
		select	[dbo].[MST_QuestionLevel].[QuestionLevel] +  ' - '  +[dbo].[MST_Question].[QuestionText] as QuestionText,
				[dbo].[MST_Question].[QuestionID]
		from 
				[dbo].[MST_Question]
		join
				[dbo].[MST_QuestionLevel]
			on  [dbo].[MST_Question].[QuestionLevelID] = [dbo].[MST_QuestionLevel].[QuestionLevelID]
end
