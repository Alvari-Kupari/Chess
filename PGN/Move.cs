using Chess.Board;
using Chess.Game;
using Chess.Game.Actions;

namespace Chess.PGN;

public class PGNMove
    (
        bool isCheck,
        bool isCheckMate,
        Coordinate to,
        bool isCapture,
        char? specifiedFile,
        int? specifiedRank,
        Type pieceType,
        Type? pawnPromoted
    )
{
    
    public MovePieceAction GetAction(ChessBoard board)
    {
        throw new NotSupportedException();
    }
}