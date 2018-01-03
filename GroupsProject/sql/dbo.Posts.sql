CREATE TABLE [dbo].[Posts] (
    [PostId]   INT           IDENTITY (1, 1) NOT NULL,
    [PostType] NVARCHAR (50) NOT NULL,
    [GroupId]  INT           NOT NULL,
    [PostDate] DATETIME NOT NULL, 
    PRIMARY KEY CLUSTERED ([PostId] ASC),
    CONSTRAINT [FK_Posts_ToGroups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([GroupId]) ON DELETE CASCADE
);

