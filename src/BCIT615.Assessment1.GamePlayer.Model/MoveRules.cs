namespace BCIT615.Assessment1.GamePlayer.Model;

public static class MoveRules
{
  // Checks that a given move follows the appropriate move 
  // rules given the current piece. Checks are not made here
  // that confirm the target position is different than the current
  // position as this has already been checked in the Player class's
  // TryMove function
  public static bool IsValidPieceMove(
      Position startPos,
      Position endPos,
      PieceType piece
  ) => piece switch
  {
    PieceType.Rook => RookMove(startPos, endPos),
    PieceType.Bishop => BishopMove(startPos, endPos),
    PieceType.Knight => KnightMove(startPos, endPos),
    PieceType.King => KingMove(startPos, endPos),
    _ => throw new ArgumentException(
        "Move cannot be validated without a valid piece type!"
    )
  };

  // Returns true if the move remains on either the same row
  // or column 
  private static bool RookMove(Position startPos, Position endPos)
  {
    return
      endPos.Row == startPos.Row ||
      endPos.Column == startPos.Column;
  }

  // Returns true if the piece moves the same absolute number
  // of rows and columns as this indicates diagonal movement
  private static bool BishopMove(Position startPos, Position endPos)
  {
    return
      Math.Abs(endPos.Row - startPos.Row) == 
      Math.Abs(endPos.Column - startPos.Column);
  }

  // Returns true if neither row nor column movement and the
  // sum of the absolute distance travelled is 3
  //
  // The int 3 is hard coded rather than assigned to a variable
  // due to the small size of the function and because for knight
  // moves this value will never change
  private static bool KnightMove(Position startPos, Position endPos)
  {
    return
      endPos.Row - startPos.Row != 0 &&
      endPos.Column - startPos.Column != 0 &&
      Math.Abs(endPos.Row - startPos.Row) +
      Math.Abs(endPos.Column - startPos.Column) == 3;
  }

  // Returns true if the destination is no further than one
  // tile away along the x or y axis
  private static bool KingMove(Position startPos, Position endPos)
  {
    return
      Math.Abs(endPos.Row - startPos.Row) <= 1 &&
      Math.Abs(endPos.Column - startPos.Column) <= 1;
  }
}
