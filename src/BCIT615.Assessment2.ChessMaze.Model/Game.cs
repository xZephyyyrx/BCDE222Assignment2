namespace BCIT615.Assessment2.ChessMaze.Model;

// Class is named "Game" to avoid potential clashes with 
// the GameGame namespace
public sealed class Game : IGame
{
  // A read-only reference of the board is stored here for
  // piece look-ups
  private readonly Board _board;

  public int BoardRows => _board.Rows;
  public int BoardColumns => _board.Columns;
  public IReadOnlyDictionary<Position, PieceType> BoardPieces => _board.Pieces;

  public Position StartPosition { get; }
  public Position TargetPosition { get; }
  public Position CurrentPosition { get; private set; }

  public bool IsComplete { get; private set; } = false;

  private readonly List<MoveRecord> moveHistory = new();
  public IReadOnlyList<MoveRecord> MoveHistory => moveHistory.AsReadOnly();

  // Used to track the sequenceNumber for moves added to 
  // the move history
  private int sequenceNumber = 0;

  // Constructor which assigns the game board to a readonly private
  // attribute, and derives the player start position, current position,
  // and target position from it
  public Game(Board board)
  {
    _board = board;
    CurrentPosition = StartPosition = _board.Start;
    TargetPosition = _board.Target;
  }

  // Returns either the piece type at the given position,
  // or if no piece exists there, null
  public PieceType? GetPieceAt(Position position)
  {
    return _board.Pieces.TryGetValue(position, out PieceType piece)
      ? piece
      : null;
  }

  // Validates a move based on the passed position, the player's current
  // position, and the board state. Returns a MoveResult value based on
  // the move's success or failure
  public MoveResult TryMove(Position position)
  {
    // Validates the attempted move. If the move is deemed invalid the
    // move result is returned
    MoveResult moveCheck = ValidateMove(position);
    if (moveCheck != MoveResult.Success)
    {
      return moveCheck;
    }

    // If the move is valid the game state is updated
    PerformMove(position);
    
    // If the move reaches the target the game is set to complete
    if (position == TargetPosition)
    {
      IsComplete = true;
      return MoveResult.GameCompleted;
    }

    // If the move does not reach the target the move's success is
    // returned without adjusting the IsComplete propertry
    return MoveResult.Success;
  }

  // Reverts the player's current position to the start position,
  // clears the move history, and sets the game state to incomplete
  public void Restart()
  {
    CurrentPosition = StartPosition;
    moveHistory.Clear();
    IsComplete = false;
  }

  // Returns true if the path from startPos to endPos contains no pieces
  private bool IsPathClear(Position startPos, Position endPos)
  {
    // In the for loop in function, both the row and column check
    // each step between their start and end positions for pieces. The
    // row and column signs are modifiers used to determine whether each
    // step should increment or decrement the row and column values
    int rowSign = Math.Sign(endPos.Row - startPos.Row);
    int columnSign = Math.Sign(endPos.Column - startPos.Column);
    
    // The difference is used to determine how many steps must be 
    // taken from the start position to the end position
    int difference = Math.Max(
      Math.Abs(endPos.Row - startPos.Row),
      Math.Abs(endPos.Column - startPos.Column)
    );

    // Checks each step from start position to end position and 
    // returns false if a piece is present
    for (int step = 1; step < difference; step++)
    {
      // A new position is created for each step to be checked
      Position stepPosition = new Position(
        startPos.Row + step * rowSign,
        startPos.Column + step * columnSign
      );

      if (GetPieceAt(stepPosition) != null)
      {
        return false;
      }
    }

    // Returns true if the loop concludes and no piece has been found
    return true;
  }

  // Updates the player's current position and adds the move to the
  // move record
  // PieceType is passed from the caller function to avoid needing to
  // validate the piece type again
  private void PerformMove(Position position) 
  {

    PieceType currentPiece = ValidateCurrentPosition();

    // Sets the player's current position to the destination 
    // position
    CurrentPosition = position;

    // Adds the successful move to the move history
    sequenceNumber++;
    moveHistory.Add(
      new MoveRecord(
        sequenceNumber,
        CurrentPosition,
        position,
        currentPiece
      )
    );
  }

  // Validates the piece type at the current position and returns the piece type
  private PieceType ValidateCurrentPosition() {

    PieceType? currentPieceCheck = GetPieceAt(CurrentPosition);
    if (currentPieceCheck is not PieceType currentPiece)
    {
      throw new InvalidOperationException(
        "Invalid player current position! A piece must exist at the current position"
      );
    } 
    return currentPiece;
  }

  private MoveResult ValidateMove(Position position)
  {
    // Checks if the game is already complete
    if (IsComplete)
    {
      return MoveResult.GameAlreadyCompleted;
    }

    // Validates that a piece exists at the current position
    PieceType currentPiece = ValidateCurrentPosition();
    
    // Checks if the target position is inbounds
    if (!_board.IsInbounds(position))
    {
      return MoveResult.OutOfBounds;
    }

    // Checks if destination position is the same as the current position
    if (position == CurrentPosition)
    {
      return MoveResult.InvalidMovement;
    }

    // Checks if the destination position is valid according to the 
    // current chess piece's movement rules
    if (!MoveRules.IsValidPieceMove(
      CurrentPosition,
      position,
      currentPiece))
    {
      return MoveResult.InvalidMovement;
    }

    // If the current piece is either a rook or bishop, checks
    // if the path from the current position to the destination
    // is clear of any other pieces
    if (
      currentPiece == PieceType.Rook ||
      currentPiece == PieceType.Bishop
    )
    {
      if (!IsPathClear(CurrentPosition, position))
      {
        return MoveResult.PathBlocked;
      }
    }
    
    // Checks if the destination position contains a piece
    // or is the target
    if (GetPieceAt(position) == null &&
        position != TargetPosition)
    {
      return MoveResult.InvalidDestination;
    }

    return MoveResult.Success;
  }
}
