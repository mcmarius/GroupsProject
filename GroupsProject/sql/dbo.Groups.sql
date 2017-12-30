CREATE TABLE [dbo].[Groups]
(
	[GroupId] INT NOT NULL PRIMARY KEY IDENTITY (1,1), 
    [GroupName] NVARCHAR(50) NOT NULL, 
    [CategoryId] INT NOT NULL, 
    [GroupDescription] NVARCHAR(200) NOT NULL, 
    CONSTRAINT [FK_Groups_ToCategories] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([CategoryId]) 
)
