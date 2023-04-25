using DailyParser.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyParser.DataAccess.DatabaseContexts;

public class GameContext : DbContext
{
    public DbSet<Game> Games { get; set; }

    public GameContext(DbContextOptions<GameContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>().ToTable("Games");
        modelBuilder.Entity<Game>().HasKey(key => key.Id);
        modelBuilder.Entity<Game>().Property(p=>p.Title).IsRequired();
    }
}
