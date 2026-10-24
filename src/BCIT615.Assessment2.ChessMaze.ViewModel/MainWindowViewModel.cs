using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BCIT615.Assessment2.ChessMaze.Model;

namespace BCIT615.Assessment2.ChessMaze.ViewModel;

public sealed class MainWindowViewModel : INotifyPropertyChanged
{
  public event PropertyChangedEventHandler? PropertyChanged;

  // This is used by the ItemControl in MainWindwo.xaml. It allows messages to be
  // displayed with different colors depending on what type of message they are
  public ObservableCollection<StyledMessage> MessageBoxOutput { get; } = new();

  private Game _game { get; set; } = null!;

  public int BoardRows => _game.BoardRows;

  public int BoardColumns => _game.BoardColumns;

  // TODO: Ensure the viewmodel handles invalid game loading gracefully
  public MainWindowViewModel()
  {
    this.AddMessage(new StyledMessage("Loading Game...", "Green"));
    this.LoadNewGame();
    this.AddMessage(new StyledMessage("Game Successfully Loaded!", "Green"));
  }

  private void AddMessage(StyledMessage message)
  {
    MessageBoxOutput.Add(message);
  }

  // Calls OnPropertyChanged to update the observable game board in the view 
  // whenever the gameboard size may be changed
  private void LoadNewGame()
  {
    this._game = SetupGame.CreateNewGame();
    OnPropertyChanged(nameof(BoardRows));
    OnPropertyChanged(nameof(BoardColumns));
  }
  
  private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}
