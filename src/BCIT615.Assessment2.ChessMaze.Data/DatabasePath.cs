namespace BCIT615.Assessment2.ChessMaze.Data;

public static class DatabasePath
{
  public static string Resolve()
  {
    string root = Path.GetFullPath(
      Path.Combine(AppContext.BaseDirectory, "../../../../..")
    );

    return Path.Combine(root, "data", "chessmaze.db");
  }
}
