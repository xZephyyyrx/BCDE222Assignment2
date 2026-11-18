using Microsoft.EntityFrameworkCore;

namespace BCIT615.Assessment2.ChessMaze.Data;

public class ChessMazeDbContext : DbContext
{
  public DbSet<GameSaves> GameSaves { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder options)
  {
    options.UseSqlite("Data Source=chessmaze.db");
  }
}
