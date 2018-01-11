CREATE TABLE [dbo].[Activities]
(
	[ActivityId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ActivityName] NVARCHAR(50) NOT NULL, 
    [ActivityDate] DATETIME NOT NULL, 
    [ActivityDescription] NVARCHAR(MAX) NOT NULL, 
    [GroupId] INT NOT NULL, 
    CONSTRAINT [FK_Activities_ToGroups] FOREIGN KEY ([GroupId]) REFERENCES [Groups]([GroupId]) ON DELETE CASCADE
)
