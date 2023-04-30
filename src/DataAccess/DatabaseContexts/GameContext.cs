using DailyParser.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyParser.DataAccess.DatabaseContexts;

public class DayContext : DbContext
{
    public DbSet<ParsedDay> ParsedDays { get; set; }

    public DayContext(DbContextOptions<DayContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParsedDay>().ToTable("ParsedDays");
        modelBuilder.Entity<ParsedDay>().HasKey(key => key.Id);
        modelBuilder.Entity<ParsedDay>().Property(p => p.Date).IsRequired();
    }
}
