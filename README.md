# Chess

A command-line chess game written in C# and .NET. The project models the board, pieces, legal moves, game state, castling, promotion, check, checkmate, and stalemate.

## Requirements

- .NET 10 SDK
- A terminal capable of reading interactive input

Check the installed SDK with:

```bash
dotnet --version
```

## Build

From the project directory:

```bash
dotnet restore
dotnet build
```

The compiled application is written to the standard `bin/Debug/net10.0/` directory.

## Run

Start an interactive game with:

```bash
dotnet run
```

The game displays the board and prompts the current player for a move. Enter the source and destination squares separated by a space:

```text
e2 e4
```

Coordinates can be entered in algebraic form, such as `a3`, or geometric form, such as `0,1`. Input is case-insensitive.

### Commands

```text
help                    Show the available commands
castle kingside         Castle short
castle queenside        Castle long
castle k                Short form of kingside castling
castle q                Short form of queenside castling
```

Pawn promotion is entered by adding the target piece after the destination square:

```text
a7 a8 queen
```

Available promotion pieces are `queen`, `bishop`, `rook`, and `knight`.

## Project Structure

- `Board/` - Board state and coordinate handling
- `Pieces/` - Chess piece types and colours
- `Game/` - Game state, moves, actions, and rules
- `Game/Mechanics/` - Move validation and status detection
- `CLI/` - Interactive terminal controller and input/output
- `PGN/` - Portable Game Notation parsing support
- `Main.cs` - Application entry point

## Development

Build the project after making changes:

```bash
dotnet build
```

The repository currently uses the executable entry point for manual play. Automated tests can be added as the rule engine and public game APIs continue to develop.

## Future Plans

### Web interface

- Add a browser-based board and move interaction layer.
- Expose the game engine through a web API so the same rules are shared by the CLI and web clients.
- Support browser game sessions, move history, and PGN import/export.
- Add responsive presentation for desktop and mobile screens.

### Bots that can play

- Define a bot interface that can choose legal actions from a game position.
- Add a baseline random or material-based bot for local games.
- Add stronger search-based bots using minimax, alpha-beta pruning, and position evaluation.
- Allow human-versus-bot and bot-versus-bot games through both the CLI and web interface.

### Code quality and gameplay

- Expand automated coverage for movement, captures, promotion, castling, check, checkmate, and stalemate.
- Improve PGN support and validate imported games against the rule engine.
- Add clearer game result and error reporting.