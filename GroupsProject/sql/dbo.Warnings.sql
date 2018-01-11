CREATE TABLE [dbo].[Warnings]
(
	[WarningId] INT NOT NULL PRIMARY KEY IDENTITY (1, 1), 
    [WarningMessage] NVARCHAR(150) NOT NULL, 
    [UserName] NVARCHAR(50) NOT NULL, 
    [GroupId] INT NOT NULL, 
    CONSTRAINT [FK_Warnings_ToGroups] FOREIGN KEY ([GroupId]) REFERENCES [Groups]([GroupId])
)
