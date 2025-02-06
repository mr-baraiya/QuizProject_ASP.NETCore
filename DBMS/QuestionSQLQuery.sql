--use Quiz

CREATE TABLE [MST_Question] (
    QuestionID			INT NOT NULL PRIMARY KEY IDENTITY(1,1)
    ,QuestionText		NVARCHAR(MAX) NOT NULL
	,QuestionLevelID	INT NOT NULL
    ,OptionA			NVARCHAR(100) NOT NULL
    ,OptionB			NVARCHAR(100) NOT NULL
    ,OptionC			NVARCHAR(100) NOT NULL
    ,OptionD			NVARCHAR(100) NOT NULL
    ,CorrectOption		NVARCHAR(100) NOT NULL
	,QuestionMarks		INT
	,IsActive			BIT Not Null Default 1
    ,UserID				INT NOT NULL
    ,Created			DATETIME DEFAULT GETDATE()
    ,Modified			DATETIME
    ,FOREIGN KEY (QuestionLevelID) REFERENCES [MST_QuestionLevel](QuestionLevelID)
    ,FOREIGN KEY (UserID) REFERENCES [MST_User](UserID)
);

-------------------------------------MST_Question--------------------------------

	
	------------------SelectAll-------------------------
	--EXEC [dbo].[PR_MST_Question_SelectAll]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_SelectAll]
	AS
	BEGIN
		SELECT 
			[dbo].[MST_Question].[QuestionID]
			,[dbo].[MST_Question].[QuestionText]
			,[dbo].[MST_Question].[OptionA]
			,[dbo].[MST_Question].[OptionB]
			,[dbo].[MST_Question].[OptionC]
			,[dbo].[MST_Question].[OptionD]
			,[dbo].[MST_Question].[CorrectOption]
			,[dbo].[MST_Question].[QuestionMarks]
			,[dbo].[MST_Question].[IsActive]
			,[dbo].[MST_Question].[QuestionLevelID]
			,[dbo].[MST_QuestionLevel].[QuestionLevel]
			,[dbo].[MST_Question].[UserID]
			,[dbo].[MST_User].[UserName]
			,[dbo].[MST_Question].[Created]
			,[dbo].[MST_Question].[Modified]
		FROM [dbo].[MST_Question]
		
		JOIN [dbo].[MST_User]
		ON [dbo].[MST_Question].[UserID] = [dbo].[MST_User].[UserID]
		
		JOIN [dbo].[MST_QuestionLevel]
		ON [dbo].[MST_Question].[QuestionLevelID] = [dbo].[MST_QuestionLevel].[QuestionLevelID]
		
		ORDER BY [dbo].[MST_Question].[QuestionID],[dbo].[MST_QuestionLevel].[QuestionLevel],[dbo].[MST_User].[UserName]
	END

	------------------SelectByPK-------------------------
	--EXEC [dbo].[PR_MST_Question_SelectByPK] 1
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_SelectByPK]
		@questionid INT
	AS
	BEGIN
		SELECT 
			[dbo].[MST_Question].[QuestionID]
			,[dbo].[MST_Question].[QuestionText]
			,[dbo].[MST_Question].[OptionA]
			,[dbo].[MST_Question].[OptionB]
			,[dbo].[MST_Question].[OptionC]
			,[dbo].[MST_Question].[OptionD]
			,[dbo].[MST_Question].[CorrectOption]
			,[dbo].[MST_Question].[QuestionMarks]
			,[dbo].[MST_Question].[IsActive]
			,[dbo].[MST_Question].[QuestionLevelID]
			,[dbo].[MST_QuestionLevel].[QuestionLevel]
			,[dbo].[MST_Question].[UserID]
			,[dbo].[MST_User].[UserName]
			,[dbo].[MST_Question].[Created]
			,[dbo].[MST_Question].[Modified]
		FROM [dbo].[MST_Question]
		
		JOIN [dbo].[MST_User]
		ON [dbo].[MST_Question].[UserID] = [dbo].[MST_User].[UserID]
		
		JOIN [dbo].[MST_QuestionLevel]
		ON [dbo].[MST_Question].[QuestionLevelID] = [dbo].[MST_QuestionLevel].[QuestionLevelID]
		
		WHERE [dbo].[MST_Question].[QuestionID] = @questionid
	END
	
	------------------INSERT-------------------------
	--EXEC [dbo].[PR_MST_Question_Insert] '1 + 1', '2', '3', '4', '5', 'A', 1, 3, 1
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_Insert]
		@questiontext		NVARCHAR(MAX)
		,@optionA			NVARCHAR(100)
		,@optionB			NVARCHAR(100)
		,@optionC			NVARCHAR(100)
		,@optionD			NVARCHAR(100)
		,@correctoption		NVARCHAR(100)
		,@questionmarks		INT
		,@questionlevelid	INT
		,@userid			INT
	AS
	BEGIN
		INSERT INTO [dbo].[MST_Question]
		(
			[dbo].[MST_Question].[QuestionText]
			,[dbo].[MST_Question].[OptionA]
			,[dbo].[MST_Question].[OptionB]
			,[dbo].[MST_Question].[OptionC]
			,[dbo].[MST_Question].[OptionD]
			,[dbo].[MST_Question].[CorrectOption]
			,[dbo].[MST_Question].[QuestionMarks]
			,[dbo].[MST_Question].[QuestionLevelID]
			,[dbo].[MST_Question].[UserID]
		)
		VALUES (
			@questiontext
			,@optionA
			,@optionB
			,@optionC
			,@optionD
			,@correctoption
			,@questionmarks
			,@questionlevelid
			,@userid
		)
	END
	
	------------------UPDATE-------------------------
	--EXEC [dbo].[PR_MST_Question_Update] 2, '1 + 2', '2', '3', '4', '5', 'B', 1, 3, 1
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_Update]
		@questionid			INT
		,@questiontext		NVARCHAR(100)
		,@optionA			NVARCHAR(100)
		,@optionB			NVARCHAR(100)
		,@optionC			NVARCHAR(100)
		,@optionD			NVARCHAR(100)
		,@correctoption		NVARCHAR(100)
		,@questionmarks		INT
		,@questionlevelid	INT
		,@userid			INT
	AS
	BEGIN
		UPDATE [dbo].[MST_Question]
		SET
			[dbo].[MST_Question].[QuestionText]			= @questiontext
			,[dbo].[MST_Question].[OptionA]				= @optionA
			,[dbo].[MST_Question].[OptionB]				= @optionB
			,[dbo].[MST_Question].[OptionC]				= @optionC
			,[dbo].[MST_Question].[OptionD]				= @optionD
			,[dbo].[MST_Question].[CorrectOption]		= @correctoption
			,[dbo].[MST_Question].[QuestionMarks]		= @questionmarks
			,[dbo].[MST_Question].[QuestionLevelID]		= @questionlevelid
			,[dbo].[MST_Question].[UserID]				= @userid
			,[dbo].[MST_Question].[Modified]			= GETDATE()
		WHERE [dbo].[MST_Question].[QuestionID]			= @questionid
	END
	
	------------------DELETE-------------------------
	--EXEC [dbo].[PR_MST_Question_Delete] 2
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_Delete]
		@questionid INT
	AS
	BEGIN
		Update [dbo].[MST_Question]
		set [dbo].[MST_Question].[IsActive] = 0
		WHERE [dbo].[MST_Question].[QuestionID] = @questionid
	END

	--EXEC [dbo].[PR_MST_Question_Delete] 2
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_Delete]
		@questionid INT
	AS
	BEGIN
		DELETE 
		FROM [dbo].[MST_Question]
		WHERE [dbo].[MST_Question].[QuestionID] = @questionid
	END




	