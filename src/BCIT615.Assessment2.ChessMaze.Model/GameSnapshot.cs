namespace BCIT615.Assessment2.ChessMaze.Model;

public sealed record GameSnapshot(
  BoardSnapshot BoardSnapshot,
  Position CurrentPosition,
  IReadOnlyList<MoveRecord> MoveHistory,
  bool IsComplete,
  int SequenceNumber
);
