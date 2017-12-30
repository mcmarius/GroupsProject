CREATE TABLE [dbo].[Messages]
(
	[MessageId] INT NOT NULL PRIMARY KEY IDENTITY (1, 1), 
    [MessageTitle] NVARCHAR(50) NOT NULL, 
    [MessageContent] NCHAR(10) NOT NULL
)
