using Chess.Board;
using Chess.Game.Mechanics;
using Chess.Pieces;

namespace Chess.Game.Actions;

public class CastlingAction(Colour side, bool queenside) : IAction
{
    private static readonly int KING_Y = 4;

    public TurnResult Execute(ChessBoard board, Colour turn)
    {
        Coordinate expectedKingLocation = new(
            X: side == Colour.BLACK ? 0 : board.XLength - 1,
            KING_Y
        );

        if (board[expectedKingLocation] is not King king || king.Colour != side || king.HasMoved)
        {
            return TurnResult.ERROR_KING_UNCASTLEABLE;
        }
        
        Coordinate expectedRookLocation = new(
            X: side == Colour.BLACK ? 0 : board.XLength - 1,
            Y: queenside ? 0 : board.YLength - 1
        );
        
        if (board[expectedRookLocation] is not Rook rook || rook.HasMoved || rook.Colour != side)
        {
            // handle
            return TurnResult.ERROR_ROOK_UNCASTLEABLE;
        }

        CastlingPath path = new(expectedKingLocation, expectedRookLocation);

        if (path.GetRequiredEmptySquares().Any(board.IsOccupied))
        {
            return TurnResult.ERROR_CASTLING_PATH_BLOCKED;
        }

        MoveRegistry coveredSquares = new(board);
        var enemyCoveredSquares = coveredSquares.GetByColour(turn.Opposite());

        if (path.GetKingsPath().Any(enemyCoveredSquares.Contains))
        {
            // handle
            return TurnResult.ERROR_CASTLING_PATH_CHECKED;
        }

        // Do the castle
        board[path.GetKingsDestination()] = king;
        board[path.GetRooksDestination()] = rook;

        board[expectedKingLocation] = null;
        board[expectedRookLocation] = null;

        king.HasMoved = true;
        rook.HasMoved = true;

        return DetermineStatus.DetermineTurnResult(board, coveredSquares, turn);
    }
}