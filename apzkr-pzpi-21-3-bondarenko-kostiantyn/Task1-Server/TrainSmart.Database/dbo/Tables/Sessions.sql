﻿CREATE TABLE [dbo].[Sessions]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[TeamId] UNIQUEIDENTIFIER NOT NULL,
	[Date] DATETIME NOT NULL,
	[EndDate] DATETIME NOT NULL,
	[Duration] INT NOT NULL,
	CONSTRAINT	[FK_Sessions_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams]([Id])
)
