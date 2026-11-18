using Microsoft.EntityFrameworkCore;

namespace BCIT615.Assessment2.ChessMaze.Data;

public class ChessMazeDbContext : DbContext
{
  public DbSet<SavedGame> SavedGames { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder options)
  {
    options.UseSqlite($"Data Source={DatabasePath.Resolve()}");
  }
}
