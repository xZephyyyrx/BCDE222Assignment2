using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BCIT615.Assessment2.ChessMaze.Model;
using BCIT615.Assessment2.ChessMaze.Data;

namespace BCIT615.Assessment2.ChessMaze.ViewModel;

public sealed class MainWindowViewModel : INotifyPropertyChanged
{
  public event PropertyChangedEventHandler? PropertyChanged;

  private Game _game = null!;

  private MessageHelper _messageHelper = new();

  private PersistenceService _persistenceService = new();

  // This is used by the ItemControl in MainWindwo.xaml. It allows messages to be
  // displayed with different colors depending on what type of message they are
  public ObservableCollection<StyledMessage> MessageBoxOutput => _messageHelper.Messages;

  // Board state properties to be used when drawing the board in the UI
  public int BoardRows => _game.BoardRows;
  public int BoardColumns => _game.BoardColumns;
  public Position TargetPosition => _game.TargetPosition;
  public Position CurrentPosition => _game.CurrentPosition;

  // TODO: Ensure the viewmodel handles invalid game loading gracefully
  public MainWindowViewModel()
  {
    LoadNewGame();
    _messageHelper.Write(new StyledMessage("Starting Game!", "Green"));
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
    else if (result == MoveResult.GameCompleted)
    {
      DisplayLastMove();
      _messageHelper.Write(ParseMoveResult.Parse(result));
    }
    else
    {
      _messageHelper.Write(ParseMoveResult.Parse(result));
    }

    OnPropertyChanged(nameof(CurrentPosition));
  }

  public void ClearLog()
  {
    _messageHelper.Clear();
  }

  public void DisplayLastMove()
  {
    _messageHelper.WriteMoveRecord(
      _game.MoveHistory.Last(),
      GetPieceAt(_game.MoveHistory.Last().To)
    );
  }

  public void ShowHistory()
  {
    if (_game.MoveHistory.Count > 0)
    {
      _messageHelper.Write(new StyledMessage("-----Move History-----", "Green"));

      foreach (MoveRecord move in _game.MoveHistory)
      {
        _messageHelper.WriteMoveRecord(move, GetPieceAt(move.To));
      }

      _messageHelper.Write(new StyledMessage("-----------------------", "Green"));
    }
    else
    {
      _messageHelper.Write(new StyledMessage("No history to show", "Green"));
    }
  }

  public void Restart()
  {
    _game.Restart();
    _messageHelper.Write(new StyledMessage("Game Restarted", "Green"));
    OnPropertyChanged(nameof(CurrentPosition));
  }

  public void SaveGame(string name)
  {
    GameSnapshot gameSnapshot = _game.Save();
    _persistenceService.Save(name, gameSnapshot);
    _messageHelper.Write(new StyledMessage("Game Saved", "Purple"));
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
