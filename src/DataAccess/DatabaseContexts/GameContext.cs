using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DatabaseContexts;

public class GameContext : DbContext
{
    public DbSet<Game> Games { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=GameDB.db;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>().ToTable("Games");

        modelBuilder.Entity<Game>()
            .HasKey(key => key.Id);
    }
}