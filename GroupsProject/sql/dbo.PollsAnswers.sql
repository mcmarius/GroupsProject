CREATE TABLE [dbo].[PollsAnswers]
(
	[PollsAnswersId] INT NOT NULL PRIMARY KEY IDENTITY (1, 1), 
    [Answered] BIT NOT NULL, 
    [UserName] NVARCHAR(50) NOT NULL, 
    [PollId] INT NOT NULL, 
    CONSTRAINT [FK_PollsAnswers_ToPolls] FOREIGN KEY ([PollId]) REFERENCES [Polls]([PollId])
)
