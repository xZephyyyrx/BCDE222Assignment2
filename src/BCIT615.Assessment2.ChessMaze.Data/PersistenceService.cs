using BCIT615.Assessment2.ChessMaze.Model;

namespace BCIT615.Assessment2.ChessMaze.Data;

public sealed class PersistenceService
{
  ChessMazeDbContext Db = new ChessMazeDbContext();

  public void Save(string name, GameSnapshot snapshot)
  {

  }
}
