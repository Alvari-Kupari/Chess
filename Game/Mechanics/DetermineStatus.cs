using Chess.Board;
using Chess.Pieces;

namespace Chess.Game.Mechanics;

public class DetermineStatus
{
    public static TurnResult DetermineTurnResult(ChessBoard board, MoveRegistry moves, Colour turn)
    {
        // now determine if its in check / checkmate for the OTHER team
        var (enemyKing, enemyKingLocation) = 
            board.FirstOrDefault(item => item.Item1 is King king && king.Colour != turn);

        var friendlyCoveredSquares = moves.GetByColour(turn);

        if (ValidMoves.HasValidMoves(board, moves, enemyKingLocation, turn.Opposite())) {
            return TurnResult.NORMAL;
        }

        if (friendlyCoveredSquares.Contains(enemyKingLocation))
        {
            return TurnResult.CHECKMATE;
        }
        return TurnResult.STALEMATE;
    }
}