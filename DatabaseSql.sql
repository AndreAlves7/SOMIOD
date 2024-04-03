CREATE TABLE [dbo].[Application] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (50) NOT NULL,
    [creation_dt] DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([name] ASC)
);

CREATE TABLE [dbo].[Container] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (50) NOT NULL,
    [creation_dt] DATETIME     NOT NULL,
    [parent]      INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([name] ASC),
    CONSTRAINT [container_parent_fk] FOREIGN KEY ([parent]) REFERENCES [dbo].[Application] ([Id])
);

CREATE TABLE [dbo].[Data] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (50) NOT NULL,
    [creation_dt] DATETIME     NOT NULL,
    [parent]      INT          NOT NULL,
    [content]     VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([name] ASC),
    CONSTRAINT [data_parent_fk] FOREIGN KEY ([parent]) REFERENCES [dbo].[Container] ([Id])
);

CREATE TABLE [dbo].[Subscription] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (50) NOT NULL,
    [creation_dt] DATETIME     NOT NULL,
    [parent]      INT          NOT NULL,
    [event]       INT          NOT NULL,
    [endpoint]    VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([name] ASC),
    CONSTRAINT [sub_parent_fk] FOREIGN KEY ([parent]) REFERENCES [dbo].[Container] ([Id]),
    CHECK ([event]=(2) OR [event]=(1))
);

SET IDENTITY_INSERT [dbo].[Application] ON
INSERT INTO [dbo].[Application] ([Id], [name], [creation_dt]) VALUES (55, N'teste', N'2023-01-05 00:00:00')
SET IDENTITY_INSERT [dbo].[Application] OFF

SET IDENTITY_INSERT [dbo].[Subscription] ON
INSERT INTO [dbo].[Subscription] ([Id], [name], [creation_dt], [parent], [event], [endpoint]) VALUES (32, N'teste', N'2023-01-23 00:00:00', 34, 1, N'mqtt://127.0.0.1:1883')
SET IDENTITY_INSERT [dbo].[Subscription] OFF

SET IDENTITY_INSERT [dbo].[Data] ON
INSERT INTO [dbo].[Data] ([Id], [name], [creation_dt], [parent], [content]) VALUES (149, N'teste', N'2023-01-23 00:00:00', 34, N'testContent')
SET IDENTITY_INSERT [dbo].[Data] OFF

SET IDENTITY_INSERT [dbo].[Container] ON
INSERT INTO [dbo].[Container] ([Id], [name], [creation_dt], [parent]) VALUES (34, N'teste', N'2023-01-23 00:00:00', 55)
SET IDENTITY_INSERT [dbo].[Container] OFF

