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
    _window = window;
    _context = context;
  }

  // TODO: Draw chess pieces to correct squares & change filled tile rendering
  // to DrawingBrush with a simple pattern
  public void SetupChessBoardView()
  {
    for (int y = 0; y < _context.BoardRows; y++)
    {
      for (int x = 0; x < _context.BoardColumns; x++)
      {
        Button tile = new Button();
        Position tilePos = new Position(y, x);
        tile.Tag = tilePos;

        SetTileStyles(tile, tilePos);
        DrawTileContents(tile, tilePos);

        _window.ChessBoardGrid.Children.Add(tile);
      }
    }
  }

  // Ensures tile borders are only drawn on edge tiles
  public Thickness GetTileBorderThickness(int maxRow, int maxColumn, Position tilePos)
  {
    return new Thickness(
      tilePos.Column == 0 ? 3 : 1.5,
      tilePos.Row == 0 ? 3 : 1.5,
      tilePos.Column == maxColumn - 1 ? 3 : 1.5,
      tilePos.Row == maxRow - 1 ? 3 : 1.5
    );
  }

  public void SetTileStyles(Button tile, Position tilePos)
  {
    double tileSize = 60D;

    // Scaling
    tile.Width = tileSize;
    tile.Height = tileSize;

    // Borders
    tile.BorderBrush = Brushes.White;
    tile.BorderThickness = GetTileBorderThickness(
      _context.BoardRows, 
      _context.BoardColumns, 
      tilePos
    );

    // Colors
    // TODO: Add alternating tile colors
    // tile.Background = ((x + y) % 2 == 0) ? Brushes.Transparent : Brushes.White;
    tile.Background = Brushes.Transparent;
  }

  public void DrawTileContents(Button tile, Position tilePos)
  {
    double pieceSize = 42D;
    tile.Content = _context.GetPieceAt(tilePos) switch
    {
      PieceType.Rook => "♖",
      PieceType.Bishop => "♗",
      PieceType.Knight => "♘",
      PieceType.King => "♔",
      _ => ""
    };
    tile.FontSize = pieceSize;
  }
}
