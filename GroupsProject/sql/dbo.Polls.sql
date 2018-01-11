CREATE TABLE [dbo].[Polls] (
    [PollId]       INT            IDENTITY (1, 1) NOT NULL,
    [PollType]     BIT            NOT NULL,
    [PollQuestion] NVARCHAR (100) NOT NULL,
    [PostId]       INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([PollId] ASC),
    CONSTRAINT [FK_Polls_ToPosts] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Posts] ([PostId]) ON DELETE CASCADE
);

