namespace Chess.Game;

using System.Diagnostics;
using Chess.Board;
using Chess.Pieces;

class ChessGame
{
    private Colour turn;
    private readonly ChessBoard board;
    private readonly ChessBoardInitializer initializer = new();

    public ChessGame()
    {
        turn = Colour.WHITE;
        board = new(initializer);
    }

    public void Run()
    {
        while(!IsCheckMate())
        {
            if (IsCheck())
            {
                GameInputOutput.PrintMessage("Check!\n");
            }
            Console.WriteLine(board.ToString());
            GameInputOutput.PrintMessage($"{(turn == Colour.WHITE ? "Whites " : "Blacks")} turn:\n");
            var from = GameInputOutput.GetUserInput("Enter starting square: \n");
            if (from is null)
            {
                GameInputOutput.PrintMessage("Invalid starting square. Try again.\n");
                continue;
            }

            var source = board[from.Value];
            if (source is not null && source.Colour != turn)
            {
                GameInputOutput.PrintMessage("Piece is the wrong colour for the turn.");
                continue;
            }
            var to = GameInputOutput.GetUserInput("Enter destination square: \n");
            if (to is null)
            {
                GameInputOutput.PrintMessage("Invalid destination square. Try again.\n");
                continue;
            }
            try
            {
                board.MovePiece(from.Value, to.Value);
                turn = turn == Colour.BLACK ? Colour.WHITE : Colour.BLACK;
            } catch (Exception e)
            {
                GameInputOutput.PrintMessage(e.Message);
            }
        }

        GameInputOutput.PrintMessage("Game over!");
    }

    private bool IsCheckMate()
    {
        if (!IsCheck()) return false;
        var (king, kingLocation) = GetCurrentKing();

        return king.GetPossibleMoves(kingLocation, board).Count == 0;
    }

    private bool IsCheck() {
        var (_, kingLocation) = GetCurrentKing();

        foreach (var piece in board)
        {
            if (piece is King) continue;
            var location = board.FindPiece(piece) ?? throw new InvalidProgramException("Piece should exist.");
            if (piece.GetPossibleMoves(location, board).Contains(kingLocation)) {
                return true;
            }
        }
        return false;
    }

    private (King, (int, int)) GetCurrentKing() {
        var king = turn == Colour.WHITE ? initializer.WhiteKing : initializer.BlackKing;
        return (king, board.FindPiece(king) ?? throw new InvalidProgramException("There should be a king on the board."));
    }
}