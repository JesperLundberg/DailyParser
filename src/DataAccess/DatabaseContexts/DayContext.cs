using DailyParser.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DailyParser.DataAccess.DatabaseContexts;

public class DayContext : DbContext
{
    public DbSet<ParsedDay> ParsedDays { get; set; }
    public DbSet<Game> Games { get; set; }

    public DayContext(DbContextOptions<DayContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParsedDay>().HasKey(key => key.Id);
        modelBuilder.Entity<ParsedDay>().Property(p => p.Date).IsRequired();
        modelBuilder.Entity<ParsedDay>().HasMany(p => p.Games);

        modelBuilder.Entity<Game>().HasKey(key => key.Id);
        modelBuilder.Entity<Game>().HasOne(g => g.ParsedDay);
    }
}

public class DayContextFactory : IDesignTimeDbContextFactory<DayContext>
{
    public DayContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory().Replace("DataAccess", "Api"))
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<DayContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DbConnection"));

        return new DayContext(optionsBuilder.Options);
    }
}
