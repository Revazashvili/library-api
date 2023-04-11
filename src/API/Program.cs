using System.Text.Json.Serialization;
using API.Endpoints;
using Application;
using Infrastructure;
using Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var apiEndpointRouteBuilder = app.MapApi();
apiEndpointRouteBuilder.MapAuthors();
apiEndpointRouteBuilder.MapBooks();

try
{
    var libraryDbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<LibraryDbContext>();
    await DatabaseSeeder.TrySeedAsync(libraryDbContext);
}
catch
{
    // ignored
}

app.Run();