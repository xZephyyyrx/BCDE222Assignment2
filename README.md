# BCIT222 - Assignment 2 - Chess Maze Game "CheckMaze"

## Description

CheckMaze is a spirited and rousing game that tasks players with navigating from the start to the target square in this game of wits and cunning.

Players must move from piece to piece, with every new piece changing the player's movement rules to match its own.

Each piece behaves as it would in an actual game of chess, with only horizontal or vertical movement available to rooks, or only diagonal movement available to bishops etc.

The target square is denoted by a star character "☆" and upon reaching it the game is concluded.

As if this wasn't enough, CheckMaze also includes the following features:

- Responsive feedback when selecting both valid and invalid squares
- A log of game messages and movement records that updates in real time
- The ability to restart
- Game save and load functionality
- The ability to display a full list of moves taken during a game

Challenge your friends to a game of CheckMaze today! Are YOU ready to be a grandmaster?

## Installation

Note: CheckMaze requires the .NET sdk version 9.0.316 at minimum

Upon extracting the downloaded files, the game can be compiled and run by entering the following commands in a Windows CLI from the chess_maze_game directory:

1 ``` dotnet restore```
2 ``` dotnet build --no-restore```
3 ``` dotnet run --project .\src\BCIT615.Assessment2.ChessMaze.App\ --no-build```

Additionally, test suites may be run following step 2 above with the following command:

- ``` dotnet test --no-build```

## Gameplay

### General Gameplay

The player's current piece is highlighted in yellow with their goal being to reach the square containing the star.

Players cannot move in a way that deviates from their current piece's movement rules. This includes the inability to jump over pieces when playing as either a rook or bishop.

Squares to be moved to are chosen by clicking on them with a mouse. Both valid and invalid moves will return feedback in the console at the bottom of the window.

The game is deemed complete upon reaching the target square, any further attempt to move following this will result in an error message in the console. Play may be restored by either selecting the 'Restart' button or loading an incomplete game save.

### Saving and Loading

Games may be saved by selecting the "Save Game" button. Upon doing so, players will be prompted to provide a name for their game save. A save name is required to successfully save a game.

Games may be loaded by selecting the "Load Game" button. Players will be prompted to select a save from a list displaying the save names as well as the save time. Available saves are ordered with the most recent saves at the top of the list.

### Gameplay Log

The gameplay log displays system messages, as well as game state messages, movement feedback, and movement records. A full history of moves taken during a game may be displayed by selecting the "Show History" button.
The console may be scrolled through using the mouse wheel, and may be cleared by selecting the "Clear Log" button.
