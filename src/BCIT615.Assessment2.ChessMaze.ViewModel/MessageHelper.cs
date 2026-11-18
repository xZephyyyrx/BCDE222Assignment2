using System.Collections.ObjectModel;

namespace BCIT615.Assessment2.ChessMaze.ViewModel;

public sealed class MessageHelper
{
  public ObservableCollection<StyledMessage> Messages { get; } = new();

  public void Write(StyledMessage message)
  {
    Messages.Add(message);
  }

  public void Clear()
  {
    Messages.Clear();
  }

  public void WriteMoveRecord(MoveRecord move, PieceType? targetPiece)
  {
    // This ensures that the move's target displays correctly in the console, whether
    // the target is a piece or the goal square
    string targetPieceName = "";
    
    if (targetPiece == null)
    {
      targetPieceName = "Target";
    }
    else
    {
      targetPieceName = $"{targetPiece}";
    }

    string outputMessage = $"{move.SequenceNumber}: {move.MovementPiece} ({move.From.Column},{move.From.Row}) => " +
      $"{targetPieceName} ({move.To.Column},{move.To.Row})";
    Write(new StyledMessage(outputMessage, "White"));
  }
}
