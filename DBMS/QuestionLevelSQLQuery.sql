--use Quiz

CREATE TABLE [MST_QuestionLevel] (
    QuestionLevelID			INT NOT NULL PRIMARY KEY IDENTITY(1,1)
    ,QuestionLevel			NVARCHAR(100) NOT NULL
    ,UserID					INT NOT NULL
    ,Created				DATETIME DEFAULT GETDATE()
    ,Modified				DATETIME 
    ,FOREIGN KEY (UserID)	REFERENCES [MST_User](UserID)
);


-------------------------------------MST_QuestionLevel--------------------------------

	
	------------------SelectAll-------------------------
	--EXEC [dbo].[PR_MST_QuestionLevel_SelectAll]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuestionLevel_SelectAll]
	AS
	BEGIN
		SELECT 
			[dbo].[MST_QuestionLevel].[QuestionLevelID]
			,[dbo].[MST_QuestionLevel].[QuestionLevel]
			,[dbo].[MST_QuestionLevel].[UserID]
			,[dbo].[MST_User].[UserName]
			,[dbo].[MST_QuestionLevel].[Created]
			,[dbo].[MST_QuestionLevel].[Modified]
		FROM [dbo].[MST_QuestionLevel]
		
		JOIN [dbo].[MST_User]
		ON [dbo].[MST_QuestionLevel].[UserID] = [dbo].[MST_User].[UserID]
		
		ORDER BY [dbo].[MST_QuestionLevel].[QuestionLevel],[dbo].[MST_User].[UserName]
	END

	------------------SelectByPK-------------------------
	--EXEC [dbo].[PR_MST_QuestionLevel_SelectByPK] 3
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuestionLevel_SelectByPK]
		@questionlevelid INT
	AS
	BEGIN
		SELECT 
			[dbo].[MST_QuestionLevel].[QuestionLevelID]
			,[dbo].[MST_QuestionLevel].[QuestionLevel]
			,[dbo].[MST_QuestionLevel].[UserID]
			,[dbo].[MST_User].[UserName]
			,[dbo].[MST_QuestionLevel].[Created]
			,[dbo].[MST_QuestionLevel].[Modified]
		FROM [dbo].[MST_QuestionLevel]
		
		JOIN [dbo].[MST_User]
		ON [dbo].[MST_QuestionLevel].[UserID] = [dbo].[MST_User].[UserID]
		
		WHERE [dbo].[MST_QuestionLevel].[QuestionLevelID] = @questionlevelid
	END

	------------------INSERT-------------------------
	--EXEC [dbo].[PR_MST_QuestionLevel_Insert] 'EASY' , 1
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuestionLevel_Insert]
		@questionlevel	NVARCHAR(100)
		,@userid			INT
	AS
	BEGIN
		INSERT INTO [dbo].[MST_QuestionLevel]
		(
			[dbo].[MST_QuestionLevel].[QuestionLevel]
			,[dbo].[MST_QuestionLevel].[UserID]
		)
		VALUES (
			@questionlevel
			,@userid
		)
	END

	------------------UPDATE-------------------------
	--EXEC [dbo].[PR_MST_QuestionLevel_Update] 4, 'EASY', 1
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuestionLevel_Update]
		@questionlevelid	INT
		,@questionlevel		NVARCHAR(100)
		,@userid			INT
	AS
	BEGIN
		UPDATE [dbo].[MST_QuestionLevel]
		SET
			[dbo].[MST_QuestionLevel].[QuestionLevel]		= @questionlevel
			,[dbo].[MST_QuestionLevel].[UserID]				= @userid
			,[dbo].[MST_QuestionLevel].[Modified]			= GETDATE()
		WHERE [dbo].[MST_QuestionLevel].[QuestionLevelID]	= @questionlevelid
	END

	------------------DELETE-------------------------
	--EXEC [dbo].[PR_MST_QuestionLevel_Delete] 3
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuestionLevel_Delete]
		@questionlevelid INT
	AS
	BEGIN
		DELETE 
		FROM [dbo].[MST_QuestionLevel]
		WHERE [dbo].[MST_QuestionLevel].[QuestionLevelID] = @questionlevelid
	END
