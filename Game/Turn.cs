using Chess.Board;
using Chess.Game.Mechanics;
using Chess.Move;
using Chess.Pieces;

namespace Chess.Game;

public class Turn
{
    public Colour Colour { private set; get; }
    public bool IsCheck {private set; get;}
    public bool IsCheckMate {private set; get;}
    private readonly Dictionary<ChessPiece, ISet<Coordinate>> _possibleMoves;
    private readonly ChessBoard _board;
    private readonly Check _checkReferee;

    public Turn(ChessBoard board, Colour colour)
    {
        Colour = colour;
        IsCheck = false;
        IsCheckMate = false;
        _possibleMoves = [];
        _board = board;
        _checkReferee = new(board, colour);

        foreach (var (piece, coordinate) in board)
        {
            _possibleMoves[piece] = piece.GetPossibleMoves(coordinate, board);

        }
    }

    public ChessMessages Execute(Coordinate from, Coordinate to)
    {
        ChessMessages messages = new();

        if (_board.IsOutsideBounds(from) || _board.IsOutsideBounds(to))
        {
            return messages.Append(ChessMessage.ERROR_OUT_OF_BOUNDS);
        }

        var sourcePiece = _board[from];
        if (sourcePiece is null) return messages.Append(ChessMessage.ERROR_SOURCE_IS_EMPTY);
        if (sourcePiece.Colour != Colour)
        {
            return messages.Append(ChessMessage.ERROR_CANT_MOVE_ENEMY);
        }

        var possibleMoves = _possibleMoves[sourcePiece];

        if (!possibleMoves.Contains(to)) return messages.Append(ChessMessage.ERROR_INVALID_MOVE);

        _board[to] = sourcePiece;
        _board[from] = null;
        sourcePiece.OnMove(from, to, _board);

        if (!_checkReferee.IsCheck(_possibleMoves)) return messages;
        messages.Append(ChessMessage.CHECK);

        if (_checkReferee.IsCheckMate()) return messages.Append(ChessMessage.CHECKMATE);

        return messages;
    }
}