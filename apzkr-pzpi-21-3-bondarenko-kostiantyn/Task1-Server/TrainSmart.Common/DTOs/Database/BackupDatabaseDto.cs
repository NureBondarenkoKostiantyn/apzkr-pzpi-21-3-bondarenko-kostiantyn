namespace TrainSmart.Common.DTOs.Database;

public record BackupDatabaseDto(
    string DatabaseName = Constants.Database.TrainSmartDb,
    string FolderPath = "");