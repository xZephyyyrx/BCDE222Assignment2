# BCIT615 Assessment 1 - Learner Starter v0.5

## Authority and environment
Use the exact released **BCIT615 Assessment 1 Game Player Technical Specification** published with this starter. Use .NET SDK **9.0.316** as pinned in `global.json`, unless teaching staff issue a centrally controlled replacement.

## Supplied and frozen
Do not edit, delete, rename, split, merge, relocate, or reproduce:
- files in `src/BCIT615.Assessment1.GamePlayer.Contracts/`;
- `src/BCIT615.Assessment1.GamePlayer.Model/ReferenceBoardData.cs`;
- supplied baseline tests.

## Learner-owned work
Create your own Model implementation and risk-based MSTest tests. Suggested names:
- `src/BCIT615.Assessment1.GamePlayer.Model/BoardGamePlayer.cs`
- `src/BCIT615.Assessment1.GamePlayer.Model/GamePlayerFactory.cs`
- `tests/BCIT615.Assessment1.GamePlayer.ModelTests/BoardGamePlayerConstructionTests.cs`
- `tests/BCIT615.Assessment1.GamePlayer.ModelTests/BoardGamePlayerMovementTests.cs`
- `tests/BCIT615.Assessment1.GamePlayer.ModelTests/BoardGamePlayerStateTests.cs`

These names support professional organisation but do not prescribe the internal algorithm. The supplied baseline tests check only contracts and reference data and are **not sufficient learner testing evidence**.

## Commands
```text
dotnet restore
dotnet build --no-restore
dotnet test --no-build
dotnet run --project src/BCIT615.Assessment1.GamePlayer.App
```

# Instructions, Limitations, Verification - Student Work 11865997

## Instructions
The supplied files contain both the initial supplied contracts and enums, as well as their implementation and the supporting code. To prepare the project and run the tests use the commands supplied above, namely:
- dotnet restore
- dotnet build --no-restore
- dotnet test --no-build
Running dotnet run --project src/BCIT615.Assessment1.GamePlayer.App will produce an output but the model created will not have any impact on this

## Limitations
The supplied files contain only the functional model but they currently exist mostly in isolation, not as a smaller part of a larger game. This decision was made as the specification document did not request a larger implementation and so the focus was on creating a functional model and verifying it through tests. Despite not being implemented into a larger game however, careful consideration was made to ensure that the model allows for scalability and will be well suited for a larger implementation.
Another potential limitation for the project is that development has taken place exclusively within a lightweight IDE and a console environment. It is possible although unlikely that testing within an environment such as Visual Studio may yield different results.

## Verification
Each of the student created classes, namely:
- Player
- Board
- MoveRules
Has been thoroughly verified through unit testing, with the tests demonstrating full functionality and implementation, as well as exception handling. Each of these classes has a corresponding test file contained within the test directory and testing yields a 100% success rate.
