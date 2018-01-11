CREATE TABLE [dbo].[GroupsLists] (
    [UserName]      NVARCHAR(50) NOT NULL,
    [GroupId]     INT              NOT NULL,
    [IsModerator] BIT              NOT NULL,
    [IsMember]    BIT              NOT NULL,
    PRIMARY KEY CLUSTERED ([UserName] ASC, [GroupId] ASC),
    CONSTRAINT [FK_Users_ToGroups] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([GroupId]) ON DELETE CASCADE
);

