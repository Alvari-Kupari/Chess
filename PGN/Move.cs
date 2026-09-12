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
        if (board[to] is not null && !isCapture)
        {
            throw new Exception("Piece capture contradiction");
        }

        Coordinate? from = null;
        foreach (var (piece, location) in board)
        {
            if (
                piece.GetType() == pieceType &&
                (specifiedFile is null || specifiedFile == location.File) &&
                (specifiedRank is null || specifiedRank == location.Rank)
            )
            {
                from = location;
            }

            


  
        }

        return new(from, to, null);
    }
}