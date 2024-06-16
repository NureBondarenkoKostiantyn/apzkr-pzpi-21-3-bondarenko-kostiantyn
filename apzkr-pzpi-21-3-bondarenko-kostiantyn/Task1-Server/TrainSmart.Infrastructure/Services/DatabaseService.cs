using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TrainSmart.Application.Abstractions.Infrastructure.Database;
using TrainSmart.Common.DTOs.Database;

namespace TrainSmart.Infrastructure.Services;

public class DatabaseService: IDatabaseService
{
    private readonly IConfiguration _configuration;

    public DatabaseService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task BackupDatabaseAsync(
        BackupDatabaseDto backupDatabaseDto,
        CancellationToken cancellationToken = default)
    {
        var filePath = BuildBackupPathWithFilename(backupDatabaseDto);

        var connectionString = _configuration.GetConnectionString("SqlConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ApplicationException("Connection string is null");
        }
        
        await using var connection = new SqlConnection(connectionString);
        var query = $"BACKUP DATABASE [{backupDatabaseDto.DatabaseName}] TO DISK='{filePath}'";

        await using var command = new SqlCommand(query, connection);
        await connection.OpenAsync(cancellationToken);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task RestoreDatabaseAsync(
        RestoreDatabaseDto restoreDatabaseDto, 
        CancellationToken cancellationToken = default)
    {
        var connectionString = _configuration.GetConnectionString("SqlConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ApplicationException("Connection string is null");
        }
        
        await using var connection = new SqlConnection(connectionString);
        
        await connection.OpenAsync(cancellationToken);
        
        try
        {
            var sql = @"
                        declare @database varchar(max) = quotename(@databaseName)
                        EXEC('ALTER DATABASE ' + @database + ' SET SINGLE_USER WITH ROLLBACK IMMEDIATE')";
            await using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@databaseName", restoreDatabaseDto.DatabaseName);

                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            var dataPath = Path.Combine(restoreDatabaseDto.SqlServerBasePath, "DATA");
            var fileListDataPath = Path.Combine(dataPath, $"{restoreDatabaseDto.DatabaseName}.mdf");
            var fileListLogPath = Path.Combine(dataPath, "FileListLogName.ldf");

            sql = @"
                    RESTORE DATABASE @databaseName 
                    FROM DISK = @localDatabasePath 
                    WITH REPLACE,
                    MOVE @fileListDataName to @fileListDataPath,
                    MOVE @fileListLogName to @fileListLogPath";

            await using (var command = new SqlCommand(sql, connection))
            {
                command.CommandTimeout = 7200;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@databaseName", "FileListLogName.ldf");
                command.Parameters.AddWithValue("@localDatabasePath", restoreDatabaseDto.LocalDatabasePath);
                command.Parameters.AddWithValue("@fileListDataName", "FileListDataName.ldf");
                command.Parameters.AddWithValue("@fileListDataPath", fileListDataPath);
                command.Parameters.AddWithValue("@fileListLogName", fileListLogPath);
                command.Parameters.AddWithValue("@fileListLogPath", fileListLogPath);

                await command.ExecuteNonQueryAsync(cancellationToken);
            }

            sql = @"
                        declare @database varchar(max) = quotename(@databaseName)
                        EXEC('ALTER DATABASE ' + @database + ' SET MULTI_USER')";
            await using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@databaseName", restoreDatabaseDto.DatabaseName);

                await command.ExecuteNonQueryAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Exception occured during database backup: " + ex.Message);
        }
    }
    
    private static string BuildBackupPathWithFilename(BackupDatabaseDto backupDatabaseDto)
    {
        var filename = $"{backupDatabaseDto.DatabaseName}-{DateTime.Now:yyyy-MM-dd}.bak";

        return Path.Combine(backupDatabaseDto.FolderPath, filename);
    }
}