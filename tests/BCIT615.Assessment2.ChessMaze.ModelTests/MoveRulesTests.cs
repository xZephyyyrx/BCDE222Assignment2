using BCIT615.Assessment2.ChessMaze.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BCIT615.Assessment2.ChessMaze.ModelTests;

[TestClass]
public sealed class MoveRulesTests
{
  // Tests IsValidPieceMove returns true for a valid Rook move
  [TestMethod]
  public void IsValidPieceMoveReturnsTrueForValidRookMove()
  {
    Position startPos = new Position(0, 0);
    Position endPos = new Position(5, 0);
    PieceType rookPiece = PieceType.Rook;

    bool expectedOutcome = true;

    bool actualOutcome = MoveRules.IsValidPieceMove(
      startPos,
      endPos,
      rookPiece
    );

    Assert.AreEqual(
      expectedOutcome,
      actualOutcome
    );
  }

  // Tests IsValidPieceMove returns false for an invalid Rook move
  [TestMethod]
  public void IsValidPieceMoveReturnsFalseForInvalidRookMove()
  {
    Position startPos = new Position(0, 0);
    Position endPos = new Position(5, 1);
    PieceType rookPiece = PieceType.Rook;

    bool expectedOutcome = false;

    bool actualOutcome = MoveRules.IsValidPieceMove(
      startPos,
      endPos,
      rookPiece
    );

    Assert.AreEqual(
      expectedOutcome,
      actualOutcome
    );
  }

  // Tests IsValidPieceMove returns true for a valid Bishop move
  [TestMethod]
  public void IsValidPieceMoveReturnsTrueForValidBishopMove()
  {
    Position startPos = new Position(0, 0);
    Position endPos = new Position(3, 3);
    PieceType bishopPiece = PieceType.Bishop;

    bool expectedOutcome = true;

    bool actualOutcome = MoveRules.IsValidPieceMove(
      startPos,
      endPos,
      bishopPiece
    );

    Assert.AreEqual(
      expectedOutcome,
      actualOutcome
    );
  }

  // Tests IsValidPieceMove returns false for an invalid Bishop move
  [TestMethod]
  public void IsValidPieceMoveReturnsFalseForInvalidBishopMove()
  {
    Position startPos = new Position(0, 0);
    Position endPos = new Position(3, 2);
    PieceType bishopPiece = PieceType.Bishop;

    bool expectedOutcome = false;

    bool actualOutcome = MoveRules.IsValidPieceMove(
      startPos,
      endPos,
      bishopPiece
    );

    Assert.AreEqual(
      expectedOutcome,
      actualOutcome
    );
  }

  // Tests IsValidPieceMove returns true for a valid Knight move
  [TestMethod]
  public void IsValidPieceMoveReturnsTrueForValidKnightMove()
  {
    Position startPos = new Position(0, 0);
    Position endPos = new Position(1, 2);
    PieceType knightPiece = PieceType.Knight;

    bool expectedOutcome = true;

    bool actualOutcome = MoveRules.IsValidPieceMove(
      startPos,
      endPos,
      knightPiece
    );

    Assert.AreEqual(
      expectedOutcome,
      actualOutcome
    );
  }

  // Tests IsValidPieceMove returns false for an invalid Knight move
  [TestMethod]
  public void IsValidPieceMoveReturnsFalseForInvalidKnightMove()
  {
    Position startPos = new Position(0, 0);
    Position endPos = new Position(3, 0);
    PieceType knightPiece = PieceType.Knight;

    bool expectedOutcome = false;

    bool actualOutcome = MoveRules.IsValidPieceMove(
      startPos,
      endPos,
      knightPiece
    );

    Assert.AreEqual(
      expectedOutcome,
      actualOutcome
    );
  }

  // Tests IsValidPieceMove returns true for a valid King move
  [TestMethod]
  public void IsValidPieceMoveReturnsTrueForValidKingMove()
  {
    Position startPos = new Position(0, 0);
    Position endPos = new Position(1, 0);
    PieceType kingPiece = PieceType.King;

    bool expectedOutcome = true;

    bool actualOutcome = MoveRules.IsValidPieceMove(
      startPos,
      endPos,
      kingPiece
    );

    Assert.AreEqual(
      expectedOutcome,
      actualOutcome
    );
  }

  // Test IsValidPieceMove returns false for an invalid King move
  [TestMethod]
  public void IsValidPieceMoveReturnsFalseForInvalidKingMove()
  {
    Position startPos = new Position(0, 0);
    Position endPos = new Position(2, 0);
    PieceType kingPiece = PieceType.King;

    bool expectedOutcome = false;

    bool actualOutcome = MoveRules.IsValidPieceMove(
      startPos,
      endPos,
      kingPiece
    );

    Assert.AreEqual(
      expectedOutcome,
      actualOutcome
    );
  }
}
