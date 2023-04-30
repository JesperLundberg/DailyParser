using DailyParser.DataAccess.DatabaseContexts;
using DailyParser.DataAccess.Repositories;
using DailyParser.DataAccess.Wrappers;
using Microsoft.EntityFrameworkCore;
using DailyParser.Web.Repositories;
using DailyParser.Parser.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IConfigurationRepository, ConfigurationRepository>();
builder.Services.AddSingleton<IDirectory, DirectoryWrapper>();
builder.Services.AddSingleton<IFileReader, FileReaderWapper>();

builder.Services.AddScoped<IDatabaseRepository, DatabaseRepository>();
builder.Services.AddScoped<IFileSystemRepository, FileSystemRepository>();
builder.Services.AddScoped<IParserService, ParserService>();

// Add database context
var configuration = new ConfigurationRepository(builder.Configuration);
builder.Services.AddDbContextPool<DayContext>(
    optionsBuilder => optionsBuilder.UseSqlite(configuration.GetSetting("dbConnection"))
);

// Build web host
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

// TODO: Run migrations?

app.Run();
