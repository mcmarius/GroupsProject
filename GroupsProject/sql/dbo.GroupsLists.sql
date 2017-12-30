CREATE TABLE [dbo].[GroupsLists] (
    [UserId]  UNIQUEIDENTIFIER NOT NULL,
    [GroupId] INT              NOT NULL,
    [IsModerator] BIT NOT NULL, 
    PRIMARY KEY CLUSTERED ([UserId] ASC, [GroupId] ASC),
    CONSTRAINT [FK_Users_ToGroups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([GroupId]) ON DELETE CASCADE
);

