using TrainSmart.Common.DTOs.Database;

namespace TrainSmart.Application.Abstractions.Infrastructure.Database;

public interface IDatabaseService
{
    Task BackupDatabaseAsync(BackupDatabaseDto backupDatabaseDto, CancellationToken cancellationToken = default);
    Task RestoreDatabaseAsync(RestoreDatabaseDto restoreDatabaseDto, CancellationToken cancellationToken = default);
}