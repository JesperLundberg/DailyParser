using DailyParser.DataAccess.DatabaseContexts;
using DailyParser.DataAccess.Repositories;
using DailyParser.DataAccess.Wrappers;
using DailyParser.Parser.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddSingleton<IConfigurationRepository, ConfigurationRepository>();
builder.Services.AddSingleton<IDirectory, DirectoryWrapper>();
builder.Services.AddSingleton<IFileReader, FileReaderWapper>();

builder.Services.AddScoped<IDatabaseRepository, DatabaseRepository>();
builder.Services.AddScoped<IFileSystemRepository, FileSystemRepository>();
builder.Services.AddScoped<IParserService, ParserService>();

// Add database context
// var configuration = new ConfigurationRepository(builder.Configuration);

builder.Services.AddDbContextPool<DayContext>(
    optionsBuilder => optionsBuilder.UseSqlite(builder.Configuration.GetValue<string>("dbConnection"))
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
