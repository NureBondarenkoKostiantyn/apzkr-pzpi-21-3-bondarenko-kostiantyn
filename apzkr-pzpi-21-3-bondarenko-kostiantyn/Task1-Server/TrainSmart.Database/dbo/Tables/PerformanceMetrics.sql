CREATE TABLE [dbo].[PerformanceMetrics]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[SessionId] UNIQUEIDENTIFIER NOT NULL,
	[TeamAthleteId] UNIQUEIDENTIFIER NOT NULL,
	[MetricType] INT NOT NULL,
	[MetricValue] DECIMAL NULL,
	[TimeStamp] DATE,
	CONSTRAINT	[FK_PerformanceMetrics_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Sessions]([Id]),
	CONSTRAINT	[FK_PerformanceMetrics_TeamAthleteId] FOREIGN KEY ([TeamAthleteId]) REFERENCES [dbo].[TeamAthletes]([Id])
)
