using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BCIT615.Assessment2.ChessMaze.ViewModel;

namespace BCIT615.Assessment2.ChessMaze.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  private readonly MainWindowViewModel viewModel = new();
    
  public MainWindow()
  {
    InitializeComponent();
    DataContext = viewModel;
    this.SetupChessBoardView();

    // This ensures the message display scrolls down to display any new messages added
    viewModel.MessageBoxOutput.CollectionChanged += (s, e) =>
    {
      MessageDisplayBox.ScrollToBottom();
    };
  }

  // TODO: Draw chess pieces to correct squares & change filled tile rendering
  // to DrawingBrush with a simple pattern
  public void SetupChessBoardView()
  {
    double tileSize = 60D;
    int rows = viewModel.BoardRows;
    int columns = viewModel.BoardColumns;

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

        ChessBoardGrid.Children.Add(tile);
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
