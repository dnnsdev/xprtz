using Domain.Shows;
using Hangfire;
using Hangfire.MemoryStorage;
using Infrastructure;
using Infrastructure.Repositories;
using Shows.Api.Application.BackgroundService;
using Shows.Api.Application.Config;
using Shows.Api.Application.Queries;
using Shows.Api.Application.Sources;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Bind config for TVMazeConfig
builder.Services.Configure<TVMazeConfig>(builder.Configuration.GetSection("TVMazeConfig"));

// Use HTTP Client factory for TV Maze client
builder.Services.AddHttpClient<ITVMazeClient, TVMazeClient>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});
builder.AddSqlServerDbContext<ShowContext>("showsdb");

// Add InMemory hangfire
builder.Services.AddHangfire(configuration => configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                                                           .UseSimpleAssemblyNameTypeSerializer()
                                                           .UseRecommendedSerializerSettings());

builder.Services.AddHostedService<ShowsPopulationService>();

GlobalConfiguration.Configuration.UseMemoryStorage();

// Add & configure injectables
builder.Services.AddTransient<IShowRepository>(opt => new ShowRepository(opt.GetRequiredService<ShowContext>()));
builder.Services.AddTransient<IShowQueries>(opt => new ShowQueries(opt.GetRequiredService<ShowContext>()));

WebApplication app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();
