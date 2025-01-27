CREATE TABLE Quiz (
    QuizID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    QuizName NVARCHAR(100) NOT NULL,
    TotalQuestions NVARCHAR(100) NOT NULL,
    UserID INT NOT NULL,
    Created DATETIME DEFAULT GETDATE(),
    Modified DATETIME,
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

INSERT INTO Quiz (QuizName, TotalQuestions, UserID, Created, Modified)
VALUES
('General Knowledge Quiz', 20, 1, GETDATE(), NULL),
('Science Trivia', 15, 2, GETDATE(), NULL),
('History Challenge', 10, 3, GETDATE(), NULL),
('Math Genius Test', 25, 4, GETDATE(), NULL),
('Literature Quiz', 18, 5, GETDATE(), NULL),
('Geography Quiz', 12, 6, GETDATE(), NULL),
('Programming Basics', 30, 7, GETDATE(), NULL),
('Music and Arts', 22, 8, GETDATE(), NULL),
('World Sports Quiz', 16, 9, GETDATE(), NULL),
('Space Exploration', 20, 10, GETDATE(), NULL);

select * from Quiz;
--1 Procedure to select all quizzes
-- exec [dbo].[PR_quiz_SelectAll]
CREATE or alter PROCEDURE [dbo].[PR_quiz_SelectAll]
AS 
BEGIN
    SELECT * FROM [dbo].[Quiz];
END


--2 Procedure to get a quiz by ID
-- exec [dbo].[PR_Quiz_getbyID] 1
CREATE or alter PROCEDURE [dbo].[PR_Quiz_getbyID]
    @Quizid INT
AS 
BEGIN 
    SELECT * FROM [dbo].[Quiz]
    WHERE QuizID = @Quizid;
END


--3 Procedure to insert a new quiz
exec [dbo].[PR_Quiz_entry] 'General Knowledge Quiz', 20
CREATE or alter PROCEDURE [dbo].[PR_Quiz_entry]
    @Quizname NVARCHAR(100),
    @TotalQuestions INT
AS
BEGIN
    INSERT INTO [dbo].[Quiz] (QuizName, TotalQuestions)
    VALUES (@Quizname, @TotalQuestions);
END


--4 Procedure to update a quiz
CREATE or alter PROCEDURE [dbo].[PR_Quiz_update]
    @Quizid INT,
    @Quizname NVARCHAR(100),
    @Totalquestions INT
AS
BEGIN
    UPDATE [dbo].[Quiz]
    SET QuizName = @Quizname, TotalQuestions = @Totalquestions
    WHERE QuizID = @Quizid;
END
 

--5 Procedure to delete a quiz
CREATE or alter PROCEDURE [dbo].[PR_quiz_delete]
    @Quizid INT
AS
BEGIN
    DELETE FROM [dbo].[Quiz]
    WHERE QuizID = @Quizid;
END