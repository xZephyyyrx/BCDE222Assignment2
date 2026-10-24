using BCIT615.Assessment2.ChessMaze.Model;

namespace BCIT615.Assessment2.ChessMaze.ViewModel;

// TODO: Add ability to load different boards based on given params
public static class SetupGame
{
  public static Game CreateNewGame()
  {
    // TODO: Add error handling for invalid board data
    Board newBoard = new Board(
      ReferenceBoardData.Rows,
      ReferenceBoardData.Columns,
      ReferenceBoardData.Start,
      ReferenceBoardData.Target,
      ReferenceBoardData.Pieces
    );

    return new Game(newBoard);
  }
}
