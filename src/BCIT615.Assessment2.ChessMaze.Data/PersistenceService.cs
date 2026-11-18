using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

using System.Text.Json;
using BCIT615.Assessment2.ChessMaze.Model;

namespace BCIT615.Assessment2.ChessMaze.Data;

public sealed class PersistenceService
{
  public void Save(string name, GameSnapshot snapshot)
  {
    using var db = new ChessMazeDbContext();

    SavedGame save = new SavedGame
    {
      Name = name,
      SaveTime = DateTime.UtcNow,
      GameSnapshot = JsonSerializer.Serialize(snapshot)
    };
    
    db.SavedGames.Add(save);
    db.SaveChanges();
  }
}
