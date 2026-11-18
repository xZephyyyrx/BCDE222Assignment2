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
using BCIT615.Assessment2.ChessMaze.Data;

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

  public void Restart_Click(object sender, RoutedEventArgs e)
  {
    _viewModel.Restart();
  }

  public void ShowHistory_Click(object sender, RoutedEventArgs e)
  {
    _viewModel.ShowHistory();
  }

  public void SaveGame_Click(object sender, RoutedEventArgs e)
  {
    var dialog = new SaveGameDialog
    {
      Owner = this
    };

    bool? result = dialog.ShowDialog();

    if (result == true)
    {
      _viewModel.SaveGame(dialog.SaveName);
    }
  }

  public void LoadGame_Click(object sender, RoutedEventArgs e)
  {
    List<SavedGame> saves = _viewModel.GetSaves();

    var dialog = new LoadGameDialog(saves)
    {
      Owner = this
    };

    dialog.ShowDialog();
  }

  public void ClearLog_Click(object sender, RoutedEventArgs e)
  {
    _viewModel.ClearLog();
  }
}
