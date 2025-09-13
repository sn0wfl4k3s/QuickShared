using QuickShared.Application;
using QuickShared.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapScalarApiReference(options =>
{
    options.WithTitle("QuickShared API");
    options.WithTheme(ScalarTheme.Mars);
    options.WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Fetch);
});

app.Run();
