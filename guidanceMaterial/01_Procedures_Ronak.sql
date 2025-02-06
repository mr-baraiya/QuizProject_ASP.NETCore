-------------------------------------MST_User--------------------------------

	------------------SelectAll-------------------------
	--EXEC [dbo].[PR_MST_User_SelectAll]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_SelectAll]
	AS
	BEGIN
		SELECT 
			[dbo].[MST_User].[UserID],
			[dbo].[MST_User].[UserName],
			[dbo].[MST_User].[Password],
			[dbo].[MST_User].[Email],
			[dbo].[MST_User].[Mobile],
			[dbo].[MST_User].[IsActive],
			[dbo].[MST_User].[IsAdmin],
			[dbo].[MST_User].[Created],
			[dbo].[MST_User].[Modified]
		FROM [dbo].[MST_User]
		ORDER BY [dbo].[MST_User].[UserName]
	END

	------------------SelectByPK-------------------------
	--EXEC [dbo].[PR_MST_User_SelectByPK]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_SelectByPK]
		@userid INT
	AS
	BEGIN
		SELECT 
			[dbo].[MST_User].[UserID],
			[dbo].[MST_User].[UserName],
			[dbo].[MST_User].[Password],
			[dbo].[MST_User].[Email],
			[dbo].[MST_User].[Mobile],
			[dbo].[MST_User].[IsActive],
			[dbo].[MST_User].[IsAdmin],
			[dbo].[MST_User].[Created],
			[dbo].[MST_User].[Modified]
		FROM [dbo].[MST_User]
		WHERE [dbo].[MST_User].[UserID] = @userid
	END

	------------------INSERT-------------------------
	--EXEC [dbo].[PR_MST_User_Insert]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_Insert]
		@username NVARCHAR(100),
		@password NVARCHAR(100),
		@email NVARCHAR(100),
		@mobile NVARCHAR(100),
		@isadmin BIT
	AS
	BEGIN
		INSERT INTO [dbo].[MST_User]
		(
			[dbo].[MST_User].[UserName], 
			[dbo].[MST_User].[Password], 
			[dbo].[MST_User].[Email], 
			[dbo].[MST_User].[Mobile], 
			[dbo].[MST_User].[IsAdmin]
		)
		VALUES (
			@username, 
			@password, 
			@email, 
			@mobile, 
			@isadmin
		)
	END

	------------------UPDATE-------------------------
	--EXEC [dbo].[PR_MST_User_Update]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_Update]
		@userid INT,
		@username NVARCHAR(100),
		@password NVARCHAR(100),
		@email NVARCHAR(100),
		@mobile NVARCHAR(100),
		@isactive BIT,
		@isadmin BIT
	AS
	BEGIN
		UPDATE [dbo].[MST_User]
		SET
			[dbo].[MST_User].[UserName] = @username,
			[dbo].[MST_User].[Password] = @password,
			[dbo].[MST_User].[Email] = @email,
			[dbo].[MST_User].[Mobile] = @mobile,
			[dbo].[MST_User].[IsActive] = @isactive,
			[dbo].[MST_User].[IsAdmin] = @isadmin,
			[dbo].[MST_User].[Modified] = GETDATE()
		WHERE [dbo].[MST_User].[UserID] = @userid
	END

	------------------DELETE-------------------------
	--EXEC [dbo].[PR_MST_User_Delete]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_Delete]
		@userid INT
	AS
	BEGIN
		DELETE 
		FROM [dbo].[MST_User]
		WHERE [dbo].[MST_User].[UserID] = @userid
	END

	-----------------SelectByUserNamePassword-------------------------
	--EXEC [dbo].[PR_MST_User_SelectByUserNamePassword]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_SelectByUserNamePassword]
		@username NVARCHAR(100),
		@password NVARCHAR(100)
	AS
	BEGIN
		SELECT
			[dbo].[MST_User].[UserName],
			[dbo].[MST_User].[Password]
		FROM [dbo].[MST_User]
		WHERE (
				[dbo].[MST_User].[UserName] = @username OR 
				[dbo].[MST_User].[Email] = @username OR 
				[dbo].[MST_User].[Mobile] = @username
			) 
			AND 
			[dbo].[MST_User].[Password] = @password
	END





	-------------------------------------MST_Quiz--------------------------------
	
	------------------SelectAll-------------------------
	--EXEC [dbo].[PR_MST_Quiz_SelectAll]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_SelectAll]
	AS
	BEGIN
		SELECT 
			[dbo].[MST_Quiz].[QuizID],
			[dbo].[MST_Quiz].[QuizName],
			[dbo].[MST_Quiz].[TotalQuestions],
			[dbo].[MST_Quiz].[QuizDate],
			[dbo].[MST_Quiz].[UserID],
			[dbo].[MST_User].[UserName],
			[dbo].[MST_Quiz].[Created],
			[dbo].[MST_Quiz].[Modified]
		FROM [dbo].[MST_Quiz]
		
		JOIN [dbo].[MST_User] 
		ON [dbo].[MST_Quiz].[UserID] = [dbo].[MST_User].[UserID]
		
		ORDER BY [dbo].[MST_Quiz].[QuizName],[dbo].[MST_User].[UserName]
	END
	
	------------------SelectByPK-------------------------
	--EXEC [dbo].[PR_MST_Quiz_SelectByPK]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_SelectByPK]
		@quizid INT
	AS
	BEGIN
		SELECT 
			[dbo].[MST_Quiz].[QuizID],
			[dbo].[MST_Quiz].[QuizName],
			[dbo].[MST_Quiz].[TotalQuestions],
			[dbo].[MST_Quiz].[UserID],
			[dbo].[MST_User].[UserName],
			[dbo].[MST_Quiz].[Created],
			[dbo].[MST_Quiz].[Modified]
		FROM [dbo].[MST_Quiz]
		
		JOIN [dbo].[MST_User] 
		ON [dbo].[MST_Quiz].[UserID] = [dbo].[MST_User].[UserID]
		
		WHERE [dbo].[MST_Quiz].[QuizID] = @quizid
	END

	------------------INSERT-------------------------
	--EXEC [dbo].[PR_MST_Quiz_Insertl
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_Insert]
		@quizname NVARCHAR(100),
		@totalquestions INT,
		@quizdate DATETIME,
		@userid INT
	AS
	BEGIN
		INSERT INTO [dbo].[MST_Quiz]
		(
			[dbo].[MST_Quiz].[QuizName],
			[dbo].[MST_Quiz].[TotalQuestions],
			[dbo].[MST_Quiz].[QuizDate],
			[dbo].[MST_Quiz].[UserID]
		)
		VALUES (
			@quizname,
			@totalquestions,
			@quizdate,
			@userid
		)
	END

	------------------UPDATE-------------------------
	--EXEC [dbo].[PR_MST_Quiz_Update]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_Update]
		@quizid INT,
		@quizname NVARCHAR(100),
		@totalquestions INT,
		@quizdate DATETIME,
		@userid INT
	AS
	BEGIN
		UPDATE [dbo].[MST_Quiz]
		SET
			[dbo].[MST_Quiz].[QuizName] = @quizname,
			[dbo].[MST_Quiz].[TotalQuestions] = @totalquestions,
			[dbo].[MST_Quiz].[QuizDate] = @quizdate,
			[dbo].[MST_Quiz].[UserID] = @userid,
			[dbo].[MST_Quiz].[Modified] = GETDATE()
		WHERE [dbo].[MST_Quiz].[QuizID] = @quizid
	END

	------------------DELETE-------------------------
	--EXEC [dbo].[PR_MST_Quiz_Delete]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_Delete]
		@quizid INT
	AS
	BEGIN
		DELETE 
		FROM [dbo].[MST_Quiz]
		WHERE [dbo].[MST_Quiz].[QuizID] = @quizid
	END




	-------------------------------------MST_Question--------------------------------

	
	------------------SelectAll-------------------------
	--EXEC [dbo].[PR_MST_Question_SelectAll]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_SelectAll]
	AS
	BEGIN
		SELECT 
			[dbo].[MST_Question].[QuestionID],
			[dbo].[MST_Question].[QuestionText],
			[dbo].[MST_Question].[OptionA],
			[dbo].[MST_Question].[OptionB],
			[dbo].[MST_Question].[OptionC],
			[dbo].[MST_Question].[OptionD],
			[dbo].[MST_Question].[CorrectOption],
			[dbo].[MST_Question].[QuestionMarks],
			[dbo].[MST_Question].[IsActive],
			[dbo].[MST_Question].[QuestionLevelID],
			[dbo].[MST_QuestionLevel].[QuestionLevel],
			[dbo].[MST_Question].[UserID],
			[dbo].[MST_User].[UserName],
			[dbo].[MST_Question].[Created],
			[dbo].[MST_Question].[Modified]
		FROM [dbo].[MST_Question]
		
		JOIN [dbo].[MST_User]
		ON [dbo].[MST_Question].[UserID] = [dbo].[MST_User].[UserID]
		
		JOIN [dbo].[MST_QuestionLevel]
		ON [dbo].[MST_Question].[QuestionLevelID] = [dbo].[MST_QuestionLevel].[QuestionLevelID]
		
		ORDER BY [dbo].[MST_Question].[QuestionID],[dbo].[MST_QuestionLevel].[QuestionLevel],[dbo].[MST_User].[UserName]
	END

	------------------SelectByPK-------------------------
	--EXEC [dbo].[PR_MST_Question_SelectByPK]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_SelectByPK]
		@questionid INT
	AS
	BEGIN
		SELECT 
			[dbo].[MST_Question].[QuestionID],
			[dbo].[MST_Question].[QuestionText],
			[dbo].[MST_Question].[OptionA],
			[dbo].[MST_Question].[OptionB],
			[dbo].[MST_Question].[OptionC],
			[dbo].[MST_Question].[OptionD],
			[dbo].[MST_Question].[CorrectOption],
			[dbo].[MST_Question].[QuestionMarks],
			[dbo].[MST_Question].[IsActive],
			[dbo].[MST_Question].[QuestionLevelID],
			[dbo].[MST_QuestionLevel].[QuestionLevel],
			[dbo].[MST_Question].[UserID],
			[dbo].[MST_User].[UserName],
			[dbo].[MST_Question].[Created],
			[dbo].[MST_Question].[Modified]
		FROM [dbo].[MST_Question]
		
		JOIN [dbo].[MST_User]
		ON [dbo].[MST_Question].[UserID] = [dbo].[MST_User].[UserID]
		
		JOIN [dbo].[MST_QuestionLevel]
		ON [dbo].[MST_Question].[QuestionLevelID] = [dbo].[MST_QuestionLevel].[QuestionLevelID]
		
		WHERE [dbo].[MST_Question].[QuestionID] = @questionid
	END
	
	------------------INSERT-------------------------
	--EXEC [dbo].[PR_MST_Question_Insert]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_Insert]
		@questiontext NVARCHAR(MAX),
		@optionA NVARCHAR(100),
		@optionB NVARCHAR(100),
		@optionC NVARCHAR(100),
		@optionD NVARCHAR(100),
		@correctoption NVARCHAR(100),
		@questionmarks INT,
		@questionlevelid INT,
		@userid INT
	AS
	BEGIN
		INSERT INTO [dbo].[MST_Question]
		(
			[dbo].[MST_Question].[QuestionText],
			[dbo].[MST_Question].[OptionA],
			[dbo].[MST_Question].[OptionB],
			[dbo].[MST_Question].[OptionC],
			[dbo].[MST_Question].[OptionD],
			[dbo].[MST_Question].[CorrectOption],
			[dbo].[MST_Question].[QuestionMarks],
			[dbo].[MST_Question].[QuestionLevelID],
			[dbo].[MST_Question].[UserID]
		)
		VALUES (
			@questiontext,
			@optionA,
			@optionB,
			@optionC,
			@optionD,
			@correctoption,
			@questionmarks,
			@questionlevelid,
			@userid
		)
	END
	
	------------------UPDATE-------------------------
	--EXEC [dbo].[PR_MST_Question_Update]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_Update]
		@questionid INT,
		@questiontext NVARCHAR(100),
		@optionA NVARCHAR(100),
		@optionB NVARCHAR(100),
		@optionC NVARCHAR(100),
		@optionD NVARCHAR(100),
		@correctoption NVARCHAR(100),
		@questionmarks INT,
		@questionlevelid INT,
		@userid INT
	AS
	BEGIN
		UPDATE [dbo].[MST_Question]
		SET
			[dbo].[MST_Question].[QuestionText] = @questiontext,
			[dbo].[MST_Question].[OptionA] = @optionA,
			[dbo].[MST_Question].[OptionB] = @optionB,
			[dbo].[MST_Question].[OptionC] = @optionC,
			[dbo].[MST_Question].[OptionD] = @optionD,
			[dbo].[MST_Question].[CorrectOption] = @correctoption,
			[dbo].[MST_Question].[QuestionMarks] = @questionmarks,
			[dbo].[MST_Question].[QuestionLevelID] = @questionlevelid,
			[dbo].[MST_Question].[UserID] = @userid,
			[dbo].[MST_Question].[Modified] = GETDATE()
		WHERE [dbo].[MST_Question].[QuestionID] = @questionid
	END
	
	------------------DELETE-------------------------
	--EXEC [dbo].[PR_MST_Question_Delete]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_Delete]
		@questionid INT
	AS
	BEGIN
		DELETE 
		FROM [dbo].[MST_Question]
		WHERE [dbo].[MST_Question].[QuestionID] = @questionid
	END




	-------------------------------------MST_QuestionLevel--------------------------------

	
	------------------SelectAll-------------------------
	--EXEC [dbo].[PR_MST_QuestionLevel_SelectAll]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuestionLevel_SelectAll]
	AS
	BEGIN
		SELECT 
			[dbo].[MST_QuestionLevel].[QuestionLevelID],
			[dbo].[MST_QuestionLevel].[QuestionLevel],
			[dbo].[MST_QuestionLevel].[UserID],
			[dbo].[MST_User].[UserName],
			[dbo].[MST_QuestionLevel].[Created],
			[dbo].[MST_QuestionLevel].[Modified]
		FROM [dbo].[MST_QuestionLevel]
		
		JOIN [dbo].[MST_User]
		ON [dbo].[MST_QuestionLevel].[UserID] = [dbo].[MST_User].[UserID]
		
		ORDER BY [dbo].[MST_QuestionLevel].[QuestionLevel],[dbo].[MST_User].[UserName]
	END

	------------------SelectByPK-------------------------
	--EXEC [dbo].[PR_MST_QuestionLevel_SelectByPK]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuestionLevel_SelectByPK]
		@questionlevelid INT
	AS
	BEGIN
		SELECT 
			[dbo].[MST_QuestionLevel].[QuestionLevelID],
			[dbo].[MST_QuestionLevel].[QuestionLevel],
			[dbo].[MST_QuestionLevel].[UserID],
			[dbo].[MST_User].[UserName],
			[dbo].[MST_QuestionLevel].[Created],
			[dbo].[MST_QuestionLevel].[Modified]
		FROM [dbo].[MST_QuestionLevel]
		
		JOIN [dbo].[MST_User]
		ON [dbo].[MST_QuestionLevel].[UserID] = [dbo].[MST_User].[UserID]
		
		WHERE [dbo].[MST_QuestionLevel].[QuestionLevelID] = @questionlevelid
	END

	------------------INSERT-------------------------
	--EXEC [dbo].[PR_MST_QuestionLevel_Insert]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuestionLevel_Insert]
		@questionlevel NVARCHAR(100),
		@userid INT
	AS
	BEGIN
		INSERT INTO [dbo].[MST_QuestionLevel]
		(
			[dbo].[MST_QuestionLevel].[QuestionLevel],
			[dbo].[MST_QuestionLevel].[UserID]
		)
		VALUES (
			@questionlevel,
			@userid
		)
	END

	------------------UPDATE-------------------------
	--EXEC [dbo].[PR_MST_QuestionLevel_Update]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuestionLevel_Update]
		@questionlevelid INT,
		@questionlevel NVARCHAR(100),
		@userid INT
	AS
	BEGIN
		UPDATE [dbo].[MST_QuestionLevel]
		SET
			[dbo].[MST_QuestionLevel].[QuestionLevel] = @questionlevel,
			[dbo].[MST_QuestionLevel].[UserID] = @userid,
			[dbo].[MST_QuestionLevel].[Modified] = GETDATE()
		WHERE [dbo].[MST_QuestionLevel].[QuestionLevelID] = @questionlevelid
	END

	------------------DELETE-------------------------
	--EXEC [dbo].[PR_MST_QuestionLevel_Delete]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuestionLevel_Delete]
		@questionlevelid INT
	AS
	BEGIN
		DELETE 
		FROM [dbo].[MST_QuestionLevel]
		WHERE [dbo].[MST_QuestionLevel].[QuestionLevelID] = @questionlevelid
	END






	-------------------------------------MST_QuizWiseQuestions--------------------------------

	
	------------------SelectAll-------------------------
	--EXEC [dbo].[PR_MST_QuizWiseQuestions_SelectAll]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuizWiseQuestions_SelectAll]
	AS
	BEGIN
		SELECT 
			[dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID],
			[dbo].[MST_QuizWiseQuestions].[QuizID],
			[dbo].[MST_Quiz].[QuizName],
			[dbo].[MST_QuizWiseQuestions].[QuestionID],
			[dbo].[MST_Question].[QuestionText],
			[dbo].[MST_QuizWiseQuestions].[UserID],
			[dbo].[MST_User].[UserName],
			[dbo].[MST_QuizWiseQuestions].[Created],
			[dbo].[MST_QuizWiseQuestions].[Modified]
		FROM [dbo].[MST_QuizWiseQuestions]
		
		JOIN [dbo].[MST_User]
		ON [dbo].[MST_QuizWiseQuestions].[UserID] = [dbo].[MST_User].[UserID]
		
		JOIN [dbo].[MST_Quiz]
		ON [dbo].[MST_QuizWiseQuestions].[QuizID] = [dbo].[MST_Quiz].[QuizID]
		
		JOIN [dbo].[MST_Question]
		ON [dbo].[MST_QuizWiseQuestions].[QuestionID] = [dbo].[MST_Question].[QuestionID]
		
		ORDER BY [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID],
				 [dbo].[MST_Quiz].[QuizName],
				 [dbo].[MST_Question].[QuestionID],
				 [dbo].[MST_User].[UserName]
	END

	------------------SelectByPK-------------------------
	--EXEC [dbo].[PR_MST_QuizWiseQuestions_SelectByPK]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuizWiseQuestions_SelectByPK]
		@quizwisequestionsid INT
	AS
	BEGIN
		SELECT 
			[dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID],
			[dbo].[MST_QuizWiseQuestions].[QuizID],
			[dbo].[MST_Quiz].[QuizName],
			[dbo].[MST_QuizWiseQuestions].[QuestionID],
			[dbo].[MST_Question].[QuestionText],
			[dbo].[MST_QuizWiseQuestions].[UserID],
			[dbo].[MST_User].[UserName],
			[dbo].[MST_QuizWiseQuestions].[Created],
			[dbo].[MST_QuizWiseQuestions].[Modified]
		FROM [dbo].[MST_QuizWiseQuestions]
		
		JOIN [dbo].[MST_User]
		ON [dbo].[MST_QuizWiseQuestions].[UserID] = [dbo].[MST_User].[UserID]
		
		JOIN [dbo].[MST_Quiz]
		ON [dbo].[MST_QuizWiseQuestions].[QuizID] = [dbo].[MST_Quiz].[QuizID]
		
		JOIN [dbo].[MST_Question]
		ON [dbo].[MST_QuizWiseQuestions].[QuestionID] = [dbo].[MST_Question].[QuestionID]

		WHERE [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @quizwisequestionsid
	END

	------------------INSERT-------------------------
	--EXEC [dbo].[PR_MST_QuizWiseQuestions_Insert]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuizWiseQuestions_Insert]
		@quizid INT,
		@questionid INT,
		@userid INT
	AS
	BEGIN
		INSERT INTO [dbo].[MST_QuizWiseQuestions]
		(
			[dbo].[MST_QuizWiseQuestions].[QuizID],
			[dbo].[MST_QuizWiseQuestions].[QuestionID],
			[dbo].[MST_QuizWiseQuestions].[UserID]
		)
		VALUES (
			@quizid,
			@questionid,
			@userid
		)
	END

	------------------UPDATE-------------------------
	--EXEC [dbo].[PR_MST_QuizWiseQuestions_Update]
	CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuizWiseQuestions_Update]
		@quizwisequestions INT,
		@quizid INT,
		@questionid INT,
		@userid INT
	AS
	BEGIN
		UPDATE [dbo].[MST_QuizWiseQuestions]
		SET
			[dbo].[MST_QuizWiseQuestions].[QuizID] = @quizid,
			[dbo].[MST_QuizWiseQuestions].[QuestionID] = @questionid,
			[dbo].[MST_QuizWiseQuestions].[UserID] = @userid,
			[dbo].[MST_QuizWiseQuestions].[Modified] = GETDATE()
		WHERE [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @quizwisequestions
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
