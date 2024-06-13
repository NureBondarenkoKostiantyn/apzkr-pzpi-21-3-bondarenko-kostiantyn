namespace TrainSmart.Common.DTOs.Database;

public record RestoreDatabaseDto(
    string DatabaseName = Constants.Database.TrainSmartDb,
    string SqlServerBasePath = "",
    string LocalDatabasePath = "");