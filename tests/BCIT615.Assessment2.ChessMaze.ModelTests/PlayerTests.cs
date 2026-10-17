using BCIT615.Assessment2.ChessMaze.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BCIT615.Assessment2.ChessMaze.ModelTests;

[TestClass]
public sealed class PlayerTests
{
  // Initializes both the testBoard and testPlayer to allow for 
  // global scope across all tests
  private Board _testBoard = null!;
  private Player _testPlayer = null!;

  // Creates a global SetupBoard function, this is used to initialize
  // a test board from the ReferenceBoardData before each test while
  // allowing individual tests to overwrite this with custom board data
  // as needed
  private void SetupBoard(
    int rows,
    int columns,
    Position start,
    Position target,
    IReadOnlyDictionary<Position, PieceType> pieces
    )
  {
    _testBoard = new Board(
      rows,
      columns,
      start,
      target,
      pieces
    );

    _testPlayer = new Player(_testBoard);
  }

  // Defines a function to run before each test. This function
  // creates a new board and player for testing
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
    
    _testPlayer = new Player(
      _testBoard
    );
  }

  ///////////////////////////
  // INITIAL STATE TESTING //
  ///////////////////////////

  // Tests that the player's starting position is the same as
  // the start defined in the board
  [TestMethod]
  public void PlayerHasCorrectStartPosition()
  {
    Assert.AreEqual(
      _testBoard.Start,
      _testPlayer.StartPosition
    );
  }

  // Tests that the player's target position is the same as 
  // the target specified in the board
  [TestMethod]
  public void PlayerHasCorrectTargetPosition()
  {
    Assert.AreEqual(
      _testBoard.Target,
      _testPlayer.TargetPosition
    );
  }

  // Tests that the player's current position begins at the
  // board's start position
  [TestMethod]
  public void PlayerHasCorrectCurrentPosition()
  {
    Assert.AreEqual(
      _testBoard.Start,
      _testPlayer.CurrentPosition
    );
  }

  // Tests that the game does not start in a completed state
  [TestMethod]
  public void PlayerStartsWithGameIncomplete()
  {
    bool expectedOutcome = false;

    Assert.AreEqual(
      _testPlayer.IsComplete,
      expectedOutcome
    );
  }

  // Tests that the player's move history list begins with
  // no entries
  [TestMethod]
  public void PlayerStartsWithNoMoveHistory()
  {
    bool expectedOutcome = false;

    bool isMoveHistoryEmpty = _testPlayer.MoveHistory.Any();

    Assert.AreEqual(
      isMoveHistoryEmpty,
      expectedOutcome
    );
  }

  // Test GetPieceAt returns the correct piece type when 
  // called on a tile that contains a piece
  [TestMethod]
  public void GetPieceTypeReturnsCorrectPieceType()
  {
    Position rookPosition = new Position(5, 0);
    PieceType expectedPiece = PieceType.Rook;
    
    PieceType? retrievedPiece = _testPlayer.GetPieceAt(rookPosition);

    Assert.AreEqual(
      retrievedPiece,
      expectedPiece
    );
  }

  // Test GetPieceAt returns null if no piece is present
  // at the requested position
  [TestMethod]
  public void GetPieceAtReturnsNullIfNoPiecePresent()
  {
    Position emptyPosition = new Position(4, 0);

    PieceType? retrievedPiece = _testPlayer.GetPieceAt(emptyPosition);

    Assert.IsNull(retrievedPiece);
  }

  ///////////////////////////////////////
  // MOVEMENT AND STATE UPDATE TESTING //
  ///////////////////////////////////////
  

  // ROOK MOVEMENT //


  // Test successful rook movement updates position and history
  [TestMethod]
  public void SuccessfulRookMovementUpdatesPositionAndHistory()
  {
    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      TestBoardData.Start,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position destinationPosition = new Position(5, 3);
    int expectedMoveHistoryEntries = 1;
    MoveResult expectedMoveResult = MoveResult.Success;

    MoveResult actualMoveResult = _testPlayer.TryMove(destinationPosition);

    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    Assert.AreEqual(
      destinationPosition,
      _testPlayer.CurrentPosition
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }

  // Test diagonal rook movement returns InvalidMovement, and that neither
  // current position or movehistory are changed
  [TestMethod]
  public void DiagonalRookMovementReturnsInvalidAndDoesntChangeState()
  {
    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      TestBoardData.Start,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position destinationPosition = new Position(3, 2);
    Position expectedPosition = _testPlayer.CurrentPosition;
    int expectedMoveHistoryEntries = 0;
    MoveResult expectedMoveResult = MoveResult.InvalidMovement;

    MoveResult actualMoveResult = _testPlayer.TryMove(destinationPosition);

    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    Assert.AreEqual(
      expectedPosition,
      _testPlayer.CurrentPosition
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }

  // Test blocked rook path returns PathBlocked and does not update state
  [TestMethod]
  public void BlockedRookPathReturnsPathBlockedAndDoesntChangeState()
  {
    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      TestBoardData.Start,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position destinationPosition = new Position(5, 5);
    Position expectedPosition = _testPlayer.CurrentPosition;
    int expectedMoveHistoryEntries = 0;
    MoveResult expectedMoveResult = MoveResult.PathBlocked;

    MoveResult actualMoveResult = _testPlayer.TryMove(destinationPosition);

    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    Assert.AreEqual(
      expectedPosition,
      _testPlayer.CurrentPosition
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }


  // BISHOP MOVEMENT //


  // Tests valid bishop move updates game state and returns success
  [TestMethod]
  public void SuccessfulBishopMovementUpdatesPositionAndHistory()
  {
    Position bishopStartPosition = new Position(5, 5);

    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      bishopStartPosition,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position destinationPosition = new Position(3, 3);
    int expectedMoveHistoryEntries = 1;
    MoveResult expectedMoveResult = MoveResult.Success;

    MoveResult actualMoveResult = _testPlayer.TryMove(destinationPosition);

    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    Assert.AreEqual(
      destinationPosition,
      _testPlayer.CurrentPosition
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }

  // Test invalid bishop movement returns InvalidMovement and game state is
  // not changed
  [TestMethod]
  public void InvalidRookMovementReturnsInvalidAndDoesntChangeState()
  {
    Position bishopStartPosition = new Position(5, 5);

    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      bishopStartPosition,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position destinationPosition = new Position(0, 5);
    Position expectedPosition = _testPlayer.CurrentPosition;
    int expectedMoveHistoryEntries = 0;
    MoveResult expectedMoveResult = MoveResult.InvalidMovement;

    MoveResult actualMoveResult = _testPlayer.TryMove(destinationPosition);

    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    Assert.AreEqual(
      expectedPosition,
      _testPlayer.CurrentPosition
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }

  // Test blocked bishop movement returns PathBlocked and game state is
  // not changed
  [TestMethod]
  public void BlockedBishopPathReturnsPathBlockedAndDoesntChangeState()
  {
    Position bishopStartPosition = new Position(5, 5);

    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      bishopStartPosition,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position destinationPosition = new Position(1, 1);
    Position expectedPosition = _testPlayer.CurrentPosition;
    int expectedMoveHistoryEntries = 0;
    MoveResult expectedMoveResult = MoveResult.PathBlocked;

    MoveResult actualMoveResult = _testPlayer.TryMove(destinationPosition);

    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    Assert.AreEqual(
      expectedPosition,
      _testPlayer.CurrentPosition
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }


  // KNIGHT MOVEMENT //
  

  // Test valid knight move returns success and updates game state
  [TestMethod]
  public void SuccessfulKnightMovementUpdatesPositionAndHistory()
  {
    Position knightStartPosition = new Position(2, 4);

    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      knightStartPosition,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position destinationPosition = new Position(3, 2);
    int expectedMoveHistoryEntries = 1;
    MoveResult expectedMoveResult = MoveResult.Success;

    MoveResult actualMoveResult = _testPlayer.TryMove(destinationPosition);

    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    Assert.AreEqual(
      destinationPosition,
      _testPlayer.CurrentPosition
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }

  // Tests knight can jump over other pieces
  [TestMethod]
  public void KnightMovementOverPiecesIsSuccessful()
  {
    Position knightStartPosition = new Position(5, 3);

    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      knightStartPosition,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position destinationPosition = new Position(3, 2);
    int expectedMoveHistoryEntries = 1;
    MoveResult expectedMoveResult = MoveResult.Success;

    MoveResult actualMoveResult = _testPlayer.TryMove(destinationPosition);

    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    Assert.AreEqual(
      destinationPosition,
      _testPlayer.CurrentPosition
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }

  // Test invalid knight move returns InvalidMovement and game state is 
  // not updated
  [TestMethod]
  public void InvalidKnightMovementReturnsInvalidAndDoesntChangeState()
  {
    Position knightStartPosition = new Position(2, 4);

    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      knightStartPosition,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position destinationPosition = new Position(2, 0);
    Position expectedPosition = _testPlayer.CurrentPosition;
    int expectedMoveHistoryEntries = 0;
    MoveResult expectedMoveResult = MoveResult.InvalidMovement;

    MoveResult actualMoveResult = _testPlayer.TryMove(destinationPosition);

    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    Assert.AreEqual(
      expectedPosition,
      _testPlayer.CurrentPosition
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }


  // KING MOVEMENT //


  // Test valid king move returns success and game state is updated
  [TestMethod]
  public void SuccessfulKingMovementUpdatesPositionAndHistory()
  {
    Position kingStartPosition = new Position(3, 3);

    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      kingStartPosition,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position destinationPosition = new Position(3, 2);
    int expectedMoveHistoryEntries = 1;
    MoveResult expectedMoveResult = MoveResult.Success;

    MoveResult actualMoveResult = _testPlayer.TryMove(destinationPosition);

    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    Assert.AreEqual(
      destinationPosition,
      _testPlayer.CurrentPosition
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }

  // Test invalid king move returns InvalidMovement and game state is
  // not updated
  [TestMethod]
  public void InvalidKingMovementReturnsInvalidAndDoesntChangeState()
  {
    Position kingStartPosition = new Position(3, 3);

    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      kingStartPosition,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position destinationPosition = new Position(5, 3);
    Position expectedPosition = _testPlayer.CurrentPosition;
    int expectedMoveHistoryEntries = 0;
    MoveResult expectedMoveResult = MoveResult.InvalidMovement;

    MoveResult actualMoveResult = _testPlayer.TryMove(destinationPosition);

    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    Assert.AreEqual(
      expectedPosition,
      _testPlayer.CurrentPosition
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }

  
  // MISCELLANEOUS MOVEMENT TESTING //


  // Test a valid move to the target returns GameCompleted and updates
  // game state
  [TestMethod]
  public void MoveToTargetCompletesGame()
  {
    Position knightStartPosition = new Position(2, 4);

    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      knightStartPosition,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position destinationPosition = new Position(0, 5);
    int expectedMoveHistoryEntries = 1;
    MoveResult expectedMoveResult = MoveResult.GameCompleted;

    MoveResult actualMoveResult = _testPlayer.TryMove(destinationPosition);

    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    Assert.AreEqual(
      destinationPosition,
      _testPlayer.CurrentPosition
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }

  // Test an out-of-bounds move returns OutOfBounds and the game state
  // is not updated
  [TestMethod]
  public void OutOfBoundsMoveReturnsOutOfBounds()
  {
    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      TestBoardData.Start,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position destinationPosition = new Position(10, 0);
    Position expectedPosition = _testPlayer.CurrentPosition;
    int expectedMoveHistoryEntries = 0;
    MoveResult expectedMoveResult = MoveResult.OutOfBounds;

    MoveResult actualMoveResult = _testPlayer.TryMove(destinationPosition);

    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    Assert.AreEqual(
      expectedPosition,
      _testPlayer.CurrentPosition
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }

  // Test legal move to empty non-target square returns InvalidDestination and 
  // game state is not updated
  [TestMethod]
  public void MoveToEmptySquareReturnsInvalidDestination()
  {
    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      TestBoardData.Start,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position destinationPosition = new Position(0, 0);
    Position expectedPosition = _testPlayer.CurrentPosition;
    int expectedMoveHistoryEntries = 0;
    MoveResult expectedMoveResult = MoveResult.InvalidDestination;

    MoveResult actualMoveResult = _testPlayer.TryMove(destinationPosition);

    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    Assert.AreEqual(
      expectedPosition,
      _testPlayer.CurrentPosition
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }

  // Test trying a move after game is complete returns GameAlreadyCompleted
  // and game state is not updated
  [TestMethod]
  public void MoveAfterGameCompletedReturnsGameAlreadyCompleted()
  {
    Position knightStartPosition = new Position(2, 4);

    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      knightStartPosition,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position targetPosition;
    Position expectedPosition = targetPosition = new Position(0, 5);
    Position destinationPosition = new Position(1, 5);
    int expectedMoveHistoryEntries = 1;
    MoveResult expectedMoveResult = MoveResult.GameAlreadyCompleted;

    // Initial move to complete the game
    _testPlayer.TryMove(targetPosition);

    MoveResult actualMoveResult = _testPlayer.TryMove(destinationPosition);

    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    Assert.AreEqual(
      expectedPosition,
      _testPlayer.CurrentPosition
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }

  ////////////////////////
  // GAME STATE TESTING //
  ////////////////////////

  // Test Restart() restores the player position, clears history, and 
  // sets IsComplete to false
  [TestMethod]
  public void RestartReturnsGameStateToInitialState()
  {
    Position knightStartPosition = new Position(2, 4);
    
    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      knightStartPosition,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    Position expectedCurrentPosition = _testPlayer.StartPosition;
    int expectedMoveHistoryEntries = 0;
    bool expectedGameCompleteState = false;

    // A move is made to reach the target and set the game state
    // to complete
    MoveResult expectedMoveResult = MoveResult.GameCompleted;
    MoveResult actualMoveResult = _testPlayer.TryMove(new Position(0, 5));

    _testPlayer.Restart();

    // Initial test as proof that the previous move reached the target
    Assert.AreEqual(
      expectedMoveResult,
      actualMoveResult
    );

    // Tests to prove Restart reverted the game state
    Assert.AreEqual(
      expectedCurrentPosition,
      _testPlayer.CurrentPosition  
    );

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );

    Assert.AreEqual(
      expectedGameCompleteState,
      _testPlayer.IsComplete
    );
  }

  // Test to prove move history is only updated on valid moves
  [TestMethod]
  public void MoveHistoryOnlyUpdatesOnValidMoves()
  {
    SetupBoard(
      TestBoardData.Rows,
      TestBoardData.Columns,
      TestBoardData.Start,
      TestBoardData.Target,
      TestBoardData.Pieces
    );

    int expectedMoveHistoryEntries = 3;

    Position validMove1 = new Position(5, 3);
    Position validMove2 = new Position(3, 2);
    Position validMove3 = new Position(4, 3);

    Position invalidMove1 = new Position(5, 1);
    Position invalidMove2 = new Position(4, 5);
    Position invalidMove3 = new Position(10, 0);

    _testPlayer.TryMove(validMove1);
    _testPlayer.TryMove(validMove2);
    _testPlayer.TryMove(validMove3);

    _testPlayer.TryMove(invalidMove1);
    _testPlayer.TryMove(invalidMove2);
    _testPlayer.TryMove(invalidMove3);

    Assert.AreEqual(
      expectedMoveHistoryEntries,
      _testPlayer.MoveHistory.Count
    );
  }

  // Test that move history is read-only
  [TestMethod]
  public void MoveHistoryIsReadOnly()
  {
    // An initial move is made to place an entry in MoveHistory
    _testPlayer.TryMove(new Position(5, 3));

    MoveRecord testMoveRecordData = new MoveRecord(
      2,
      new Position(5, 3),
      new Position(5, 5),
      PieceType.Bishop
    );

    IList<MoveRecord> moveHistory = (IList<MoveRecord>)_testPlayer.MoveHistory;

    Assert.ThrowsException<NotSupportedException>(
      () => moveHistory.Add(testMoveRecordData)
    );
    
    Assert.ThrowsException<NotSupportedException>(
      () => moveHistory.Clear()
    );
  }

  // Test to show each entry in the move history is ordered
  // sequentially, starting from 1
  [TestMethod]
  public void MoveHistorySequenceBeginsAtOne()
  {
    int expectedFirstRecordSequenceNum = 1;
    int expectedSecondRecordSequenceNum = 2;
    int expectedThirdRecordSequenceNum = 3;

    Position firstMove = new Position(5, 3);
    Position secondMove = new Position(3, 5);
    Position thirdMove = new Position(1, 4);

    // Three moves are performed to populate the move history
    _testPlayer.TryMove(firstMove);
    _testPlayer.TryMove(secondMove);
    _testPlayer.TryMove(thirdMove);
    
    Assert.AreEqual(
      expectedFirstRecordSequenceNum,
      _testPlayer.MoveHistory[0].SequenceNumber
    );

    Assert.AreEqual(
      expectedSecondRecordSequenceNum,
      _testPlayer.MoveHistory[1].SequenceNumber
    );

    Assert.AreEqual(
      expectedThirdRecordSequenceNum,
      _testPlayer.MoveHistory[2].SequenceNumber
    );
  }
}
