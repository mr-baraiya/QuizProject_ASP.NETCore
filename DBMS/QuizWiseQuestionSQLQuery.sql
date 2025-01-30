--use Quiz

CREATE TABLE [MST_QuizWiseQuestion] (
    QuizWiseQuestionID			INT NOT NULL PRIMARY KEY IDENTITY(1,1)
	,QuizID						INT NOT NULL
	,QuestionID					INT NOT NULL
    ,UserID						INT NOT NULL
    ,Created					DATETIME DEFAULT GETDATE()
    ,Modified					DATETIME
    ,FOREIGN KEY (QuizID)		REFERENCES [MST_Quiz](QuizID)
	,FOREIGN KEY (QuestionID)	REFERENCES [MST_Question](QuestionID)
	,FOREIGN KEY (UserID)		REFERENCES [MST_User](UserID)
);


-------------------------------------MST_QuizWiseQuestions--------------------------------

	
	------------------SelectAll-------------------------
	--EXEC [dbo].[PR_MST_QuizWiseQuestions_SelectAll]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuizWiseQuestions_SelectAll]
	AS
	BEGIN
		SELECT 
			[dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID]
			,[dbo].[MST_QuizWiseQuestions].[QuizID]
			,[dbo].[MST_Quiz].[QuizName]
			,[dbo].[MST_QuizWiseQuestions].[QuestionID]
			,[dbo].[MST_Question].[QuestionText]
			,[dbo].[MST_QuizWiseQuestions].[UserID]
			,[dbo].[MST_User].[UserName]
			,[dbo].[MST_QuizWiseQuestions].[Created]
			,[dbo].[MST_QuizWiseQuestions].[Modified]
		FROM [dbo].[MST_QuizWiseQuestions]
		
		JOIN [dbo].[MST_User]
		ON [dbo].[MST_QuizWiseQuestions].[UserID]		= [dbo].[MST_User].[UserID]
		
		JOIN [dbo].[MST_Quiz]
		ON [dbo].[MST_QuizWiseQuestions].[QuizID]		= [dbo].[MST_Quiz].[QuizID]
		
		JOIN [dbo].[MST_Question]
		ON [dbo].[MST_QuizWiseQuestions].[QuestionID]	= [dbo].[MST_Question].[QuestionID]
		
		ORDER BY [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID]
				 ,[dbo].[MST_Quiz].[QuizName]
				 ,[dbo].[MST_Question].[QuestionID]
				 ,[dbo].[MST_User].[UserName]
	END

	------------------SelectByPK-------------------------
	--EXEC [dbo].[PR_MST_QuizWiseQuestions_SelectByPK]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuizWiseQuestions_SelectByPK]
		@quizwisequestionsid INT
	AS
	BEGIN
		SELECT 
			[dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID]
			,[dbo].[MST_QuizWiseQuestions].[QuizID]
			,[dbo].[MST_Quiz].[QuizName]
			,[dbo].[MST_QuizWiseQuestions].[QuestionID]
			,[dbo].[MST_Question].[QuestionText]
			,[dbo].[MST_QuizWiseQuestions].[UserID]
			,[dbo].[MST_User].[UserName]
			,[dbo].[MST_QuizWiseQuestions].[Created]
			,[dbo].[MST_QuizWiseQuestions].[Modified]
		FROM [dbo].[MST_QuizWiseQuestions]
		
		JOIN [dbo].[MST_User]
		ON [dbo].[MST_QuizWiseQuestions].[UserID]		= [dbo].[MST_User].[UserID]
		
		JOIN [dbo].[MST_Quiz]
		ON [dbo].[MST_QuizWiseQuestions].[QuizID]		= [dbo].[MST_Quiz].[QuizID]
		
		JOIN [dbo].[MST_Question]
		ON [dbo].[MST_QuizWiseQuestions].[QuestionID]	= [dbo].[MST_Question].[QuestionID]

		WHERE [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @quizwisequestionsid
	END

	------------------INSERT-------------------------
	--EXEC [dbo].[PR_MST_QuizWiseQuestions_Insert] 3, 3, 3
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuizWiseQuestions_Insert]
		@quizid		INT,
		@questionid INT,
		@userid		INT
	AS
	BEGIN
		INSERT INTO [dbo].[MST_QuizWiseQuestions]
		(
			[dbo].[MST_QuizWiseQuestions].[QuizID]
			,[dbo].[MST_QuizWiseQuestions].[QuestionID]
			,[dbo].[MST_QuizWiseQuestions].[UserID]
		)
		VALUES (
			@quizid
			,@questionid
			,@userid
		)
	END

	------------------UPDATE-------------------------
	--EXEC [dbo].[PR_MST_QuizWiseQuestions_Update]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuizWiseQuestions_Update]
		@quizwisequestions	INT
		,@quizid			INT
		,@questionid		INT
		,@userid			INT
	AS
	BEGIN
		UPDATE [dbo].[MST_QuizWiseQuestions]
		SET
			[dbo].[MST_QuizWiseQuestions].[QuizID]					= @quizid
			,[dbo].[MST_QuizWiseQuestions].[QuestionID]				= @questionid
			,[dbo].[MST_QuizWiseQuestions].[UserID]					= @userid
			,[dbo].[MST_QuizWiseQuestions].[Modified]				= GETDATE()
		WHERE [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID]	= @quizwisequestions
	END

	------------------DELETE-------------------------
	--EXEC [dbo].[PR_MST_QuizWiseQuestions_Delete]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuizWiseQuestions_Delete]
		@quizwisequestions INT
	AS
	BEGIN
		DELETE 
		FROM [dbo].[MST_QuizWiseQuestions]
		WHERE [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @quizwisequestions
	END
