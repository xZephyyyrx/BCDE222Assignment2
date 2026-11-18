namespace BCIT615.Assessment2.ChessMaze.Model;

public sealed record BoardSnapshot(
  int Rows,
  int Columns,
  Position Start,
  Position Target,
  IReadOnlyDictionary<Position, PieceType> Pieces
);
