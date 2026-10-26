using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using BCIT615.Assessment2.ChessMaze.ViewModel;

namespace BCIT615.Assessment2.ChessMaze.View;

public sealed class ChessBoardView
{
  private readonly MainWindow _window;
  private readonly MainWindowViewModel _context;

  public ChessBoardView(MainWindow window, MainWindowViewModel context)
  {
    this._window = window;
    this._context = context;
  }

  // TODO: Draw chess pieces to correct squares & change filled tile rendering
  // to DrawingBrush with a simple pattern
  public void SetupChessBoardView()
  {
    double tileSize = 60D;
    int rows = this._context.BoardRows;
    int columns = this._context.BoardColumns;

    for (int y = 0; y < rows; y++)
    {
      for (int x = 0; x < columns; x++)
      {
        Button tile = new Button();
        Position tilePos = new Position(y, x);
        tile.Tag = tilePos;

        // Button styling //
        
        // Scaling
        tile.Width = tileSize;
        tile.Height = tileSize;

        // Borders
        tile.BorderBrush = Brushes.White;
        tile.BorderThickness = this.GetTileBorderThickness(rows, columns, tilePos);

        // Colors
        tile.Background = ((x + y) % 2 == 0) ? Brushes.Transparent : Brushes.White;

        this._window.ChessBoardGrid.Children.Add(tile);
      }
    }
  }

  // Ensures tile borders are only drawn on edge tiles
  public Thickness GetTileBorderThickness(int maxRow, int maxColumn, Position tilePos)
  {
    return new Thickness(
      tilePos.Column == 0 ? 3 : 0,
      tilePos.Row == 0 ? 3 : 0,
      tilePos.Column == maxColumn - 1 ? 3 : 0,
      tilePos.Row == maxRow - 1 ? 3 : 0
    );
  }
}
