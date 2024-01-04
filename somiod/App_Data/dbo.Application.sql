CREATE TABLE [dbo].[Application]
(
    [Id] INT NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(MAX) NOT NULL,
    [CreationDt] DATETIME NOT NULL,

    [ContainerId] INT FOREIGN KEY REFERENCES [dbo].[Container]([Id]),
);