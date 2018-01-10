CREATE TABLE [dbo].[Files] (
    [FileId]   INT           IDENTITY (1, 1) NOT NULL,
    [FileName] NVARCHAR (50) NOT NULL,
    [FullName] NVARCHAR (150) NOT NULL,
    [PostId]   INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([FileId] ASC),
    CONSTRAINT [FK_Files_ToPosts] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Posts] ([PostId]) ON DELETE CASCADE
);

