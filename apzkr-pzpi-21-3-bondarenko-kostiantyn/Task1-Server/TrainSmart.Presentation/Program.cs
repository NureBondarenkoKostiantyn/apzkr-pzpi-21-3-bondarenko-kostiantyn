using TrainSmart.Application;
using TrainSmart.Common;
using TrainSmart.Infrastructure;
using TrainSmart.Infrastructure.Auth;
using TrainSmart.Persistence;
using TrainSmart.Presentation;
using TrainSmart.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationLevelDependencies();
builder.Services.ConfigureCommonLayerDependencies();
builder.Services.ConfigurePersistenceLayerDependencies(builder.Configuration);
builder.Services.ConfigureInfrastructureAuthDependencies(builder.Configuration);
builder.Services.ConfigureInfrastructureLayerDependencies();
builder.Services.ConfigurePresentationLayerDependencies();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors();
app.RegisterEndpointDefinitions();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();