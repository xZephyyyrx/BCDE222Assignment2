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
  private readonly MainWindowViewModel _viewModel = new();
  private readonly ChessBoardView _chessBoardView = null!;
    
  public MainWindow()
  {
    InitializeComponent();
    DataContext = _viewModel;

    // Handles setting up the chess board in the UI
    _chessBoardView = new ChessBoardView(this, _viewModel);
    _chessBoardView.SetupChessBoardView();

    // This ensures the message display scrolls down to display any new messages added
    EnableMessageBoxScrolling();
    EnableCurrentPositionUIUpdates();
  }

  public void EnableMessageBoxScrolling()
  {
    _viewModel.MessageBoxOutput.CollectionChanged += (s, e) =>
    {
      MessageDisplayBox.ScrollToBottom();
    };
  }

  public void EnableCurrentPositionUIUpdates()
  {
    _viewModel.PropertyChanged += (s, e) =>
    {
      if (e.PropertyName == nameof(_viewModel.CurrentPosition))
      {
        _chessBoardView.UpdateCurrentPositionDisplay();
      }
    };
  }

  public void TryMove_Click(object sender, RoutedEventArgs e)
  {
    if (sender is Button button)
    {
      _viewModel.TryMove((Position)button.Tag);
    }
  }
}
