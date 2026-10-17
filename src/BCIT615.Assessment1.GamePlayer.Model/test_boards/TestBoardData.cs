using System.Collections.ObjectModel;
using BCIT615.Assessment1.GamePlayer;

namespace BCIT615.Assessment1.GamePlayer.Model;

/// <summary>Provides the known-good in-memory board used by the Assessment 1 reference workflow.</summary>
public static class TestBoardData
{
    public const int Rows = 6;
    public const int Columns = 6;

    public static Position Start => new Position(5, 0);
    public static Position Target => new Position(0, 5);

    public static IReadOnlyDictionary<Position, PieceType> Pieces { get; } =
        new ReadOnlyDictionary<Position, PieceType>(
            new Dictionary<Position, PieceType>
            {

                [new Position(1, 1)] = PieceType.Bishop,
                [new Position(2, 4)] = PieceType.Knight,
                [new Position(3, 2)] = PieceType.King,
                [new Position(3, 3)] = PieceType.King,
                [new Position(4, 2)] = PieceType.Rook,
                [new Position(4, 3)] = PieceType.Rook,
                [new Position(5, 0)] = PieceType.Rook,
                [new Position(5, 3)] = PieceType.Knight,
                [new Position(5, 5)] = PieceType.Bishop
            });
}
