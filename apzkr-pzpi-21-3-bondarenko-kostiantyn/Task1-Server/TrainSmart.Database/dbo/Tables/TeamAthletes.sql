CREATE TABLE [dbo].[TeamAthletes]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[AthleteId] UNIQUEIDENTIFIER NOT NULL,
	[TeamId]	UNIQUEIDENTIFIER NOT NULL,
	[DateJoined] DATETIME NULL,
	CONSTRAINT	[FK_TeamAthletes_AthleteId] FOREIGN KEY ([AthleteId]) REFERENCES [dbo].[Athletes]([Id]),
	CONSTRAINT	[FK_TeamAthletes_TeamId]	FOREIGN KEY ([TeamId])	  REFERENCES [dbo].[Teams]([Id])
)
