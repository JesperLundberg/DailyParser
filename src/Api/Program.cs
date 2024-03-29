using DailyParser.DataAccess.DatabaseContexts;
using DailyParser.DataAccess.Models;
using DailyParser.DataAccess.Repositories;
using DailyParser.DataAccess.Wrappers;
using DailyParser.Parser.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDirectory, DirectoryWrapper>();
builder.Services.AddScoped<IFileReader, FileReaderWapper>();

builder.Services.AddScoped<IDatabaseRepository, DatabaseRepository>();
builder.Services.AddScoped<IFileSystemRepository, FileSystemRepository>();
builder.Services.AddScoped<IParserService, ParserService>();

// Add database context
builder.Services.AddDbContextPool<DayContext>(
    optionsBuilder =>
        optionsBuilder.UseSqlServer(builder.Configuration.GetValue<string>("DbConnection"))
);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (
    string.Equals(
        bool.TrueString.ToLower(),
        builder.Configuration.GetValue<string>("Settings:RunMigrationsAtStartUp"),
        StringComparison.InvariantCultureIgnoreCase
    )
)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<DayContext>();
    await db.Database.MigrateAsync();
}

if (
    string.Equals(
        bool.TrueString.ToLower(),
        builder.Configuration.GetValue<string>("Settings:SeedDatabaseAtStartUp"),
        StringComparison.InvariantCultureIgnoreCase
    )
)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<DayContext>();

    if (!await db.ParsedDays.AnyAsync())
    {
        await db.ParsedDays.AddAsync(
            new ParsedDay
            {
                Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                Games = new List<Game>
                {
                    new() { Name = "Game 1" },
                    new() { Name = "Game 2" }
                }
            }
        );

        await db.SaveChangesAsync();
    }
}

app.UseCors(
    options => options
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
