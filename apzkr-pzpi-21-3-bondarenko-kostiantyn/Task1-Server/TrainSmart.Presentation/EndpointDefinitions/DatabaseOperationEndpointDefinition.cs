using Microsoft.AspNetCore.Mvc;
using TrainSmart.Application.Abstractions.Infrastructure.Database;
using TrainSmart.Common.DTOs.Database;
using TrainSmart.Presentation.Abstractions;

namespace TrainSmart.Presentation.EndpointDefinitions;

public class DatabaseOperationEndpointDefinition: IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var databaseOperationGroup = app.MapGroup("/api/databaseOperations");

        databaseOperationGroup.MapPost("/backup", Backup);
        databaseOperationGroup.MapPost("/restore", Restore);
    }

    private static async Task<IResult> Backup(
        IDatabaseService databaseService,
        [FromBody] BackupDatabaseDto backupDatabaseDto)
    {
        await databaseService.BackupDatabaseAsync(backupDatabaseDto);
        return Results.NoContent();
    }

    private static async Task<IResult> Restore(
        IDatabaseService databaseService,
        [FromBody] RestoreDatabaseDto restoreDatabaseDto)
    {
        await databaseService.RestoreDatabaseAsync(restoreDatabaseDto);
        return Results.NoContent();
    }
}