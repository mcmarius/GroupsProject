CREATE TABLE [dbo].[Options]
(
	[OptionId] INT NOT NULL PRIMARY KEY IDENTITY (1,1), 
    [OptionName] NVARCHAR(50) NOT NULL, 
    [PollId] INT NOT NULL, 
    CONSTRAINT [FK_Options_ToPolls] FOREIGN KEY ([PollId]) REFERENCES [Polls]([PollId]) ON DELETE CASCADE
)
