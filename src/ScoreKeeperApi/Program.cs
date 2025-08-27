using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScoreKeeperApi.Data;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

// Add Entity Framework
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:DefaultConnection");
builder.Services.AddDbContext<ScoreKeeperDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Build().Run();