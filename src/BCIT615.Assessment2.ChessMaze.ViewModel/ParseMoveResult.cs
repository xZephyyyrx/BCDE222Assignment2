namespace BCIT615.Assessment2.ChessMaze.ViewModel;

public static class ParseMoveResult
{
  public static StyledMessage Parse(MoveResult moveResult)
  {
    string message = GetMessage(moveResult);
    string color = GetColor(moveResult);

    return new StyledMessage(message, color);
  }

  // Responsible for converting MoveResults into more human friendly messages
  private static string GetMessage(MoveResult moveResult)
  {
    string message = moveResult switch
    {
      MoveResult.GameAlreadyCompleted => "Move Error: The game is already complete",
      MoveResult.OutOfBounds => "Move Error: The selected tile must be inbounds",
      MoveResult.InvalidMovement => "Move Error: Current piece cannot move to the selected tile",
      MoveResult.PathBlocked => "Move Error: Another piece is blocking your path",
      MoveResult.InvalidDestination => "Move Error: The selected tile must contain either a piece or the target",
      MoveResult.GameCompleted => "Success! You won the game!",
      MoveResult.Success => "Move Successful!",
      _ => "Move Error: An unknown error has occured"
    };

    return message;
  }

  // Responsible for displaying messages with the correct colors
  private static string GetColor(MoveResult moveResult)
  {
    string color = moveResult switch
    {
      MoveResult.Success => "White",
      MoveResult.GameCompleted => "Yellow",
      _ => "Red"
    };

    return color;
  }
}
