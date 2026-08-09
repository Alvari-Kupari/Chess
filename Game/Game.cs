namespace Chess.Game;

using Chess.Board;
using Chess.Move;
using Chess.Pieces;

class ChessGame
{
    public Colour Turn {get; private set;}
    public ChessBoard Board {get;}
    private readonly ChessBoardInitializer initializer = new();
    private readonly Referee referee;

    public ChessGame()
    {
        Turn = Colour.WHITE;
        Board = new(initializer);
        referee = new(Board, initializer.WhiteKing, initializer.BlackKing);
    }

    public ChessMessages HandleTurn(Coordinate from, Coordinate to)
    {
        ChessMessages messages = new();

        if (Board.IsOutsideBounds(from) || Board.IsOutsideBounds(to))
        {
            return messages.Append(ChessMessage.ERROR_OUT_OF_BOUNDS);
        }

        var sourcePiece = Board[from];
        if (sourcePiece is null) return messages.Append(ChessMessage.ERROR_SOURCE_IS_EMPTY);
        if (sourcePiece.Colour == Colour.WHITE && Turn == Colour.BLACK)
        {
            return messages.Append(ChessMessage.ERROR_BLACK_CANT_MOVE_A_WHITE_PIECE);
        }
        if (sourcePiece.Colour == Colour.BLACK && Turn == Colour.WHITE)
        {
            return messages.Append(ChessMessage.ERROR_WHITE_CANT_MOVE_A_BLACK_PIECE);
        }
        var possibleMoves = sourcePiece.GetPossibleMoves(from, Board);

        if (!possibleMoves.Contains(to)) return messages.Append(ChessMessage.ERROR_INVALID_MOVE);

        Board[to] = sourcePiece;
        Board[from] = null;
        sourcePiece.OnMove(from, to, Board);

        Turn = Turn == Colour.BLACK ? Colour.WHITE : Colour.BLACK;

        if (referee.IsCheckMate(Colour.BLACK)) return messages.Append(ChessMessage.CHECKMATE_BLACK);
        if (referee.IsCheckMate(Colour.WHITE)) return messages.Append(ChessMessage.CHECKMATE_WHITE);

        if (referee.IsCheck(Colour.BLACK)) return messages.Append(ChessMessage.CHECK_BLACK);
        if (referee.IsCheck(Colour.WHITE)) return messages.Append(ChessMessage.CHECK_WHITE);


        return messages;
    }

}