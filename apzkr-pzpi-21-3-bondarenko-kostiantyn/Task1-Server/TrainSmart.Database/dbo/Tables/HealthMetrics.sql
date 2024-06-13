CREATE TABLE [dbo].[HealthMetrics]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[SessionId] UNIQUEIDENTIFIER NOT NULL,
	[TeamAthleteId] UNIQUEIDENTIFIER NOT NULL,
	[MetricType] INT NOT NULL,
	[MetricValue] DECIMAL NULL,
	[TimeStamp] DATE,
	CONSTRAINT	[FK_HealthMetrics_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Sessions]([Id]),
	CONSTRAINT	[FK_HealthMetrics_TeamAthleteId] FOREIGN KEY ([TeamAthleteId]) REFERENCES [dbo].[TeamAthletes]([Id])
)
