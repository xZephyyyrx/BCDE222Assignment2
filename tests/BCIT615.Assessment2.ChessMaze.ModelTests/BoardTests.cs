using System.Collections.ObjectModel;
using BCIT615.Assessment2.ChessMaze.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BCIT615.Assessment2.ChessMaze.ModelTests;

[TestClass]
public sealed class BoardTests
{
  // Initialize the testboard to allow for global scope across all tests
  private static Board _testBoard = null!;
  
  // Creates a global SetupBoard function to allow the creation of new
  // boards with different parameters for the various tests
  private void SetupBoard(
    int rows,
    int columns,
    Position start,
    Position target,
    IReadOnlyDictionary<Position, PieceType> pieces)
  {
    _testBoard = new Board(
      rows,
      columns,
      start,
      target,
      pieces
    );
  }

  // Initializes each test by creating a new board using the reference
  // board data. This board may be overwritten by calling SetupBoard
  // in later tests to demonstrate board creation validation
  [TestInitialize]
  public void BeforeEach()
  {
    SetupBoard(
      ReferenceBoardData.Rows,
      ReferenceBoardData.Columns,
      ReferenceBoardData.Start,
      ReferenceBoardData.Target,
      ReferenceBoardData.Pieces
    );
  }

  // Test that testBoard contains the reference board Rows data
  [TestMethod]
  public void BoardContainsCorrectRows()
  {
    Assert.AreEqual(
      ReferenceBoardData.Rows,
      _testBoard.Rows
    );
  }

  // Test that testBoard contains the reference board Columns data
  [TestMethod]
  public void BoardContainsCorrectColumns()
  {
    Assert.AreEqual(
      ReferenceBoardData.Columns,
      _testBoard.Columns
    );
  }

  // Test the testBoard contains the reference board Start data
  [TestMethod]
  public void BoardContainsTheCorrectStart()
  {
    Assert.AreEqual(
      ReferenceBoardData.Start,
      _testBoard.Start
    );
  }

  // Test the testBoard contains the reference board Target data
  [TestMethod]
  public void BoardContainsTheCorrecttarget()
  {
    Assert.AreEqual(
      ReferenceBoardData.Target,
      _testBoard.Target
    );
  }

  // Tests if IsInbounds returns true for an inbound position
  [TestMethod]
  public void IsInboundsReturnsTrueForInboundPosition()
  {
    Position inboundsPosition = new Position(3, 3);
    bool expectedResult = true;

    bool actualResult = _testBoard.IsInbounds(inboundsPosition);

    Assert.AreEqual(
      expectedResult,
      actualResult
    );
  }

  // Tests if IsInbounds returns false for an out-of-bounds position
  [TestMethod]
  public void IsInboundsReturnsFalseForOutOfBoundsPosition()
  {
    Position outOfBoundsPosition = new Position(10, 10);
    bool expectedResult = false;

    bool actualResult = _testBoard.IsInbounds(outOfBoundsPosition);

    Assert.AreEqual(
      expectedResult,
      actualResult
    );
  }

  
  //////////////////////
  // BOARD VALIDATION //
  //////////////////////

  // Tests a board with non-positive rows throws an exception
  [TestMethod]
  public void NonPositiveBoardRowsThrowsError()
  {
    int invalidRows = -1;

    Assert.ThrowsException<ArgumentOutOfRangeException>(
      () => SetupBoard(
        invalidRows,
        ReferenceBoardData.Columns,
        ReferenceBoardData.Start,
        ReferenceBoardData.Target,
        ReferenceBoardData.Pieces
      )
    );
  }

  // Tests a board with non-positive columns throws an exception
  [TestMethod]
  public void NonPositiveBoardColumnsThrowsException()
  {
    int invalidColumns = 0;

    Assert.ThrowsException<ArgumentOutOfRangeException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        invalidColumns,
        ReferenceBoardData.Start,
        ReferenceBoardData.Target,
        ReferenceBoardData.Pieces
      )
    );
  }

  // The following four tests check that out of bounds pieces
  // will throw an error during board creation
  [TestMethod]
  public void OutOfBoundsAbovePieceThrowsException()
  {
    IReadOnlyDictionary<Position, PieceType> invalidPiecePositions = 
      new ReadOnlyDictionary<Position, PieceType>(
        new Dictionary<Position, PieceType>
        {
          [new Position(-1, 0)] = PieceType.Rook
        });

    Assert.ThrowsException<ArgumentOutOfRangeException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        ReferenceBoardData.Start,
        ReferenceBoardData.Target,
        invalidPiecePositions
      )
    );
  }

  [TestMethod]
  public void OutOfBoundsBelowPieceThrowsException()
  {
    IReadOnlyDictionary<Position, PieceType> invalidPiecePositions = 
      new ReadOnlyDictionary<Position, PieceType>(
        new Dictionary<Position, PieceType>
        {
          [new Position(6, 0)] = PieceType.Rook
        });

    Assert.ThrowsException<ArgumentOutOfRangeException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        ReferenceBoardData.Start,
        ReferenceBoardData.Target,
        invalidPiecePositions
      )
    );
  }

  [TestMethod]
  public void OutOfBoundsLeftPieceThrowsException()
  {
    IReadOnlyDictionary<Position, PieceType> invalidPiecePositions = 
      new ReadOnlyDictionary<Position, PieceType>(
        new Dictionary<Position, PieceType>
        {
          [new Position(0, -1)] = PieceType.Rook
        });

    Assert.ThrowsException<ArgumentOutOfRangeException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        ReferenceBoardData.Start,
        ReferenceBoardData.Target,
        invalidPiecePositions
      )
    );
  }

  [TestMethod]
  public void OutOfBoundsRightPieceThrowsException()
  {
    IReadOnlyDictionary<Position, PieceType> invalidPiecePositions = 
      new ReadOnlyDictionary<Position, PieceType>(
        new Dictionary<Position, PieceType>
        {
          [new Position(0, 6)] = PieceType.Rook
        });

    Assert.ThrowsException<ArgumentOutOfRangeException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        ReferenceBoardData.Start,
        ReferenceBoardData.Target,
        invalidPiecePositions
      )
    );
  }

  // The following eight tests check that both out-of-bounds
  // start and target positions will throw an exception
  [TestMethod]
  public void OutOfBoundsAboveStartThrowsException()
  {
    Position invalidStartPosition = new Position(-1, 0);

    Assert.ThrowsException<ArgumentOutOfRangeException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        invalidStartPosition,
        ReferenceBoardData.Target,
        ReferenceBoardData.Pieces
      )
    );
  }

  [TestMethod]
  public void OutOfBoundsBelowStartThrowsException()
  {
    Position invalidStartPosition = new Position(6, 0);

    Assert.ThrowsException<ArgumentOutOfRangeException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        invalidStartPosition,
        ReferenceBoardData.Target,
        ReferenceBoardData.Pieces
      )
    );
  }

  [TestMethod]
  public void OutOfBoundsLeftStartThrowsException()
  {
    Position invalidStartPosition = new Position(0, -1);

    Assert.ThrowsException<ArgumentOutOfRangeException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        invalidStartPosition,
        ReferenceBoardData.Target,
        ReferenceBoardData.Pieces
      )
    );
  }

  [TestMethod]
  public void OutOfBoundsRightStartThrowsException()
  {
    Position invalidStartPosition = new Position(0, 6);

    Assert.ThrowsException<ArgumentOutOfRangeException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        invalidStartPosition,
        ReferenceBoardData.Target,
        ReferenceBoardData.Pieces
      )
    );
  }

  [TestMethod]
  public void OutOfBoundsAboveTargetThrowsException()
  {
    Position invalidTargetPosition = new Position(-1, 0);

    Assert.ThrowsException<ArgumentOutOfRangeException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        invalidTargetPosition,
        ReferenceBoardData.Target,
        ReferenceBoardData.Pieces
      )
    );
  }

  [TestMethod]
  public void OutOfBoundsBelowTargetThrowsException()
  {
    Position invalidTargetPosition = new Position(6, 0);

    Assert.ThrowsException<ArgumentOutOfRangeException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        invalidTargetPosition,
        ReferenceBoardData.Target,
        ReferenceBoardData.Pieces
      )
    );
  }

  [TestMethod]
  public void OutOfBoundsLeftTargetThrowsException()
  {
    Position invalidTargetPosition = new Position(0, -1);

    Assert.ThrowsException<ArgumentOutOfRangeException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        invalidTargetPosition,
        ReferenceBoardData.Target,
        ReferenceBoardData.Pieces
      )
    );
  }

  [TestMethod]
  public void OutOfBoundsRightTargetThrowsException()
  {
    Position invalidTargetPosition = new Position(0, 6);

    Assert.ThrowsException<ArgumentOutOfRangeException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        invalidTargetPosition,
        ReferenceBoardData.Target,
        ReferenceBoardData.Pieces
      )
    );
  }

  // Tests that creating a new board with matching start and target
  // positions throws an exception
  [TestMethod]
  public void SameStartAndTargetThrowsException()
  {
    Position startAndTargetPosition = new Position(0, 0);

    Assert.ThrowsException<ArgumentException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        startAndTargetPosition,
        startAndTargetPosition,
        ReferenceBoardData.Pieces
      )
    );
  }

  // Tests that a start position that does not contain a chess piece 
  // throws an exception
  [TestMethod]
  public void StartWithNoPieceThrowsException()
  {
    Position startPosition = new Position(0, 0);

    IReadOnlyDictionary<Position, PieceType> invalidPiecePositions = 
      new ReadOnlyDictionary<Position, PieceType>(
        new Dictionary<Position, PieceType>
        {
          [new Position(3, 3)] = PieceType.Rook
        });

    Assert.ThrowsException<ArgumentException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        startPosition,
        ReferenceBoardData.Target,
        invalidPiecePositions
      )
    );
  }

  // Tests that creating a board with an invalid piecetype throws
  // an exception
  [TestMethod]
  public void InvalidPieceTypeThrowsException()
  {
    int invalidPieceTypeValue = 1;

    IReadOnlyDictionary<Position, PieceType> invalidPieceType =
      new ReadOnlyDictionary<Position, PieceType>(
        new Dictionary<Position, PieceType>
        {
          [new Position(0, 0)] = (PieceType)invalidPieceTypeValue
        });

    Assert.ThrowsException<ArgumentException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        ReferenceBoardData.Start,
        ReferenceBoardData.Target,
        invalidPieceType
      )
    );
  }

  // Tests that two chess pieces occupying the same square throws
  // an exception
  [TestMethod]
  public void MultiplePiecesOnSameSquareThrowsException()
  {
    IReadOnlyDictionary<Position, PieceType> invalidPiecePositions =
      new ReadOnlyDictionary<Position, PieceType>(
        new Dictionary<Position, PieceType>{
          [new Position(0, 0)] = PieceType.Rook,
          [new Position(0, 0)] = PieceType.Bishop
        });

    Assert.ThrowsException<ArgumentException>(
      () => SetupBoard(
        ReferenceBoardData.Rows,
        ReferenceBoardData.Columns,
        ReferenceBoardData.Start,
        ReferenceBoardData.Target,
        invalidPiecePositions
      )
    );
  }
}
