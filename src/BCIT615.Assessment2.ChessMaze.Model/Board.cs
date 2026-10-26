namespace BCIT615.Assessment2.ChessMaze.Model;

public sealed class Board
{
  public int Rows { get; }
  public int Columns { get; }

  public Position Start { get; }
  public Position Target { get; }

  public IReadOnlyDictionary<Position, PieceType> Pieces { get; }

  public Board(
      int rows,
      int columns,
      Position start,
      Position target,
      IReadOnlyDictionary<Position, PieceType> pieces
  ) {
    // Validates the board dimensions and sets the 
    // board Row and Column properties before validating
    // the game pieces.
    //
    // This is done to allow the rows and columns to be
    // accessed within the other validation functions
    // without the need to pass the board dimensions to 
    // each
    ValidateDimensions(rows, columns);
    Rows = rows;
    Columns = columns;

    ValidateGamePieces(
      start,
      target,
      pieces
    );
    Start = start;
    Target = target;
    Pieces = new Dictionary<Position, PieceType>(pieces).AsReadOnly();
  }

  // Returns true if the given position is within the board bounds
  public bool IsInbounds(Position position) {
    return (
      position.Row >= 0 &&
      position.Row < Rows &&
      position.Column >= 0 &&
      position.Column < Columns
    );
  }

  private void ValidateGamePieces(
    Position start,
    Position target,
    IReadOnlyDictionary<Position, PieceType> pieces)
  {
    CheckPiecesInbounds(pieces);
    CheckStartAndTargetInbounds(start, target);
    CheckStartAndTargetDifferentPositions(start, target);
    CheckStartContainsPiece(start, pieces);
    CheckAllPieceTypesValid(pieces);
    CheckNoPiecesOnSameSquares(pieces);
  }

  private void ValidateDimensions(int rows, int columns) {
    if (rows <= 0 || columns <= 0) {
      throw new ArgumentOutOfRangeException(
        "Invalid Board: Cannot constuct a new board with non-positive dimensions!"
      );
    }
  }

  private void CheckPiecesInbounds(
    IReadOnlyDictionary<Position, PieceType> pieces)
  {
    foreach (KeyValuePair<Position, PieceType> piece in pieces)
    {
      Position position = piece.Key;
      if (!IsInbounds(position))
      {
        throw new ArgumentOutOfRangeException(
          "Invalid Board: All chess pieces must fit within the board dimensions"
        );
      }
    }
  }

  private void CheckStartAndTargetInbounds(
    Position start,
    Position target)
  {
    if (!IsInbounds(start) || !IsInbounds(target))
    {
      throw new ArgumentOutOfRangeException(
        "Invalid Board: Start or Target position is out-of-bounds"
      );
    }
  }

  private void CheckStartAndTargetDifferentPositions(
    Position start,
    Position target)
  {
    if (
      start.Row == target.Row &&
      start.Column == target.Column)
    {
      throw new ArgumentException(
        "Invalid Board: Start position and target position must be different"
      );
    }
  }

  private void CheckStartContainsPiece(
    Position start,
    IReadOnlyDictionary<Position, PieceType> pieces)
  {
    if (!pieces.ContainsKey(start))
    {
      throw new ArgumentException(
        "Invalid Board: Start position must contain a chess piece"
      );
    }
  }

  private void CheckAllPieceTypesValid(
    IReadOnlyDictionary<Position, PieceType> pieces)
  {
    foreach (KeyValuePair<Position, PieceType> piece in pieces)
    {
      if (!Enum.IsDefined(piece.Value))
      {
        throw new ArgumentException(
          "Invalid Board: All pieces must be a valid PieceType"
        );
      }
    }
  }

  private void CheckNoPiecesOnSameSquares(
    IReadOnlyDictionary<Position, PieceType> pieces)
  {
    if (pieces.Keys.Count() != pieces.Keys.Distinct().Count())
    {
      throw new ArgumentException(
        "Invalid Board: Multiple chess pieces cannot occupy the same square"
      );
    }
  }
}
