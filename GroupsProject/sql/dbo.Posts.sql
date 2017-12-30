﻿CREATE TABLE [dbo].[Posts]
(
	[PostId] INT NOT NULL PRIMARY KEY IDENTITY (1,1), 
    [PostType] NVARCHAR(50) NOT NULL, 
    [GroupId] INT NOT NULL, 
    CONSTRAINT [FK_Posts_ToGroups] FOREIGN KEY ([GroupId]) REFERENCES [Groups]([GroupId]) ON DELETE CASCADE
)
