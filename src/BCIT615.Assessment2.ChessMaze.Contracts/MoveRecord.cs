namespace BCIT615.Assessment2.ChessMaze;

/// <summary>Records one successful game movement.</summary>
public readonly record struct MoveRecord(
    int SequenceNumber,
    Position From,
    Position To,
    PieceType MovementPiece);
