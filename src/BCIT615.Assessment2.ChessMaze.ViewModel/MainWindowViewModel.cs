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

  // Board state properties to be used when drawing the board in the UI
  public int BoardRows => _game.BoardRows;
  public int BoardColumns => _game.BoardColumns;
  public Position TargetPosition => _game.TargetPosition;
  public Position CurrentPosition => _game.CurrentPosition;

  // TODO: Ensure the viewmodel handles invalid game loading gracefully
  public MainWindowViewModel()
  {
    LoadNewGame();
    AddMessage(new StyledMessage("Starting Game!", "Green"));
  }

  // Used by ChessBoardView to know on which board tiles to render chess pieces
  public PieceType? GetPieceAt(Position position)
  {
    return _game.GetPieceAt(position);
  }

  public void TryMove(Position position)
  {
    MoveResult result = _game.TryMove(position);

    if (result == MoveResult.Success)
    {
      DisplayLastMove();
    }
    else
    {
      AddMessage(ParseMoveResult.Parse(result));
    }

    OnPropertyChanged(nameof(CurrentPosition));
  }

  public void AddMessage(StyledMessage message)
  {
    MessageBoxOutput.Add(message);
  }

  private void DisplayMove(MoveRecord move)
  {
    // This ensures that the move's target displays correctly in the console, whether
    // the target is a piece or the goal square
    string targetPiece = "";
    
    if (GetPieceAt(move.To) == null)
    {
      targetPiece = "Target";
    }
    else
    {
      targetPiece = $"{GetPieceAt(move.To)}";
    }

    string outputMessage = $"{move.SequenceNumber}: {move.MovementPiece} ({move.From.Row},{move.From.Column}) => " +
      $"{targetPiece} ({move.To.Row},{move.To.Column})";
    AddMessage(new StyledMessage(outputMessage, "White"));
  }

  public void DisplayLastMove()
  {
    DisplayMove(_game.MoveHistory.Last());
  }

  public void ShowHistory()
  {
    AddMessage(new StyledMessage("# Move History #", "Green"));
    foreach (MoveRecord move in _game.MoveHistory)
    {
      DisplayMove(move);
    };
    AddMessage(new StyledMessage("#\\Move History #", "Green"));
  }

  public void Restart()
  {
    _game.Restart();
    AddMessage(new StyledMessage("Game Restarted", "Green"));
    OnPropertyChanged(nameof(CurrentPosition));
  }

  // Calls OnPropertyChanged to update the observable game board in the view 
  // whenever the gameboard size may be changed
  private void LoadNewGame()
  {
    _game = SetupGame.CreateNewGame();
    OnPropertyChanged(nameof(BoardRows));
    OnPropertyChanged(nameof(BoardColumns));
  }
  
  private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}
