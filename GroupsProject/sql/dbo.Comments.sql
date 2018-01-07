CREATE TABLE [dbo].[Comments]
(
	[CommentId] INT NOT NULL PRIMARY KEY IDENTITY (1, 1), 
    [CommentMessage] NVARCHAR(MAX) NOT NULL, 
    [PostId] INT NOT NULL, 
    CONSTRAINT [FK_Comments_ToPosts] FOREIGN KEY ([PostId]) REFERENCES [Posts]([PostId])
)
