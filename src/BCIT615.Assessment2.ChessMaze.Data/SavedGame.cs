namespace BCIT615.Assessment2.ChessMaze.Data;

public sealed class SavedGame
{
  public int Id { get; set; }

  public string Name { get; set; } = null!;

  public DateTime SaveTime { get; set; }

  public string GameSnapshot { get; set; } = null!;
}
