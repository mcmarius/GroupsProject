CREATE TABLE [dbo].[Options] (
    [OptionId]   INT           IDENTITY (1, 1) NOT NULL,
    [OptionName] NVARCHAR (50) NOT NULL,
    [PollId]     INT           NOT NULL,
    [OptionCount] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([OptionId] ASC),
    CONSTRAINT [FK_Options_ToPolls] FOREIGN KEY ([PollId]) REFERENCES [dbo].[Polls] ([PollId]) ON DELETE CASCADE
);

