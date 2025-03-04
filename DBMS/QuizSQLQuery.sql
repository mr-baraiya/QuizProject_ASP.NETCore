-- use QUIZ

CREATE TABLE [MST_Quiz] (
    QuizID					INT NOT NULL PRIMARY KEY IDENTITY(1,1)
    ,QuizName				NVARCHAR(100) NOT NULL
    ,TotalQuestions			NVARCHAR(100) NOT NULL
	,QuizDate				DATETIME NOT NULL
    ,UserID					INT NOT NULL 
    ,Created				DATETIME DEFAULT GETDATE()
    ,Modified				DATETIME
	,FOREIGN KEY (UserID)	REFERENCES [dbo].[MST_User](UserID)
);

--INSERT INTO [MST_Quiz] (QuizName, TotalQuestions, QuizDate, UserID)
--VALUES
--('General Knowledge Quiz', 20, '1-1-2025', 1),
--('Science Trivia', 15, '1-1-2025', 2);
--select * from [MST_Quiz];

-------------------------------------MST_Quiz--------------------------------
	
	------------------SelectAll-------------------------
	--EXEC [dbo].[PR_MST_Quiz_SelectAll]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_SelectAll]
	AS
	BEGIN
		SELECT 
			[dbo].[MST_Quiz].[QuizID]
			,[dbo].[MST_Quiz].[QuizName]
			,[dbo].[MST_Quiz].[TotalQuestions]
			,[dbo].[MST_Quiz].[QuizDate]
			,[dbo].[MST_Quiz].[UserID]
			,[dbo].[MST_User].[UserName]
			,[dbo].[MST_Quiz].[Created]
			,[dbo].[MST_Quiz].[Modified]
		FROM [dbo].[MST_Quiz]
		JOIN [dbo].[MST_User] 
		ON [dbo].[MST_Quiz].[UserID] = [dbo].[MST_User].[UserID]
		ORDER BY [dbo].[MST_Quiz].[QuizName],[dbo].[MST_User].[UserName]
	END
	
	------------------SelectByPK-------------------------
	--EXEC [dbo].[PR_MST_Quiz_SelectByPK] 3
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_SelectByPK]
		@quizid INT
	AS
	BEGIN
		SELECT 
			[dbo].[MST_Quiz].[QuizID]
			,[dbo].[MST_Quiz].[QuizName]
			,[dbo].[MST_Quiz].[TotalQuestions]
			,[dbo].[MST_Quiz].[QuizDate]
			,[dbo].[MST_Quiz].[UserID]
			,[dbo].[MST_User].[UserName]
			,[dbo].[MST_Quiz].[Created]
			,[dbo].[MST_Quiz].[Modified]
		FROM [dbo].[MST_Quiz]
		
		JOIN [dbo].[MST_User] 
		ON [dbo].[MST_Quiz].[UserID] = [dbo].[MST_User].[UserID]
		
		WHERE [dbo].[MST_Quiz].[QuizID] = @quizid
	END

	------------------INSERT-------------------------
	--EXEC [dbo].[PR_MST_Quiz_Insert] 'Science Trivia', 15, '1-1-2025', 5
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_Insert]
		@quizname			NVARCHAR(100)
		,@totalquestions	INT
		,@quizdate			DATETIME
		,@userid			INT
	AS
	BEGIN
		INSERT INTO [dbo].[MST_Quiz]
		(
			[dbo].[MST_Quiz].[QuizName]
			,[dbo].[MST_Quiz].[TotalQuestions]
			,[dbo].[MST_Quiz].[QuizDate]
			,[dbo].[MST_Quiz].[UserID]
		)
		VALUES (
			@quizname,
			@totalquestions,
			@quizdate,
			@userid
		)
	END

	------------------UPDATE-------------------------
	--EXEC [dbo].[PR_MST_Quiz_Update] 5,'Science Trivia', 15, '1-1-2025', 5
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_Update]
		@quizid				INT
		,@quizname			NVARCHAR(100)
		,@totalquestions	INT
		,@quizdate			DATETIME
		,@userid			INT
	AS
	BEGIN
		UPDATE [dbo].[MST_Quiz]
		SET
			[dbo].[MST_Quiz].[QuizName]			= @quizname
			,[dbo].[MST_Quiz].[TotalQuestions]	= @totalquestions
			,[dbo].[MST_Quiz].[QuizDate]		= @quizdate
			,[dbo].[MST_Quiz].[UserID]			= @userid
			,[dbo].[MST_Quiz].[Modified]		= GETDATE()
		WHERE [dbo].[MST_Quiz].[QuizID]			= @quizid
	END

	------------------DELETE-------------------------
	--EXEC [dbo].[PR_MST_Quiz_Delete] 5
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_Delete]
		@quizid INT
	AS
	BEGIN
		DELETE 
		FROM [dbo].[MST_Quiz]
		WHERE [dbo].[MST_Quiz].[QuizID] = @quizid
	END
