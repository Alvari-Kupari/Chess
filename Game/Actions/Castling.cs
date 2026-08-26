using Chess.Board;
using Chess.Game.Mechanics.Castling;
using Chess.Move;
using Chess.Pieces;

namespace Chess.Game.Actions;

public class CastlingAction(Colour side, bool queenside) : IAction
{
    private static readonly int KING_X = 3;

    public void Execute(ChessBoard board, Colour turn)
    {
        Coordinate expectedKingLocation = new(
            X: KING_X,
            Y: side == Colour.BLACK ? 0 : board.GetBoundary(1)
        );

        if (board[expectedKingLocation] is not King king || king.Colour != side || king.HasMoved)
        {
            // handle
            return;
        }
        
        Coordinate expectedRookLocation = new(
            X: queenside ? 0 : board.GetBoundary(0),
            Y: side == Colour.WHITE ? board.GetBoundary(1) : 0
        );
        
        if (board[expectedRookLocation] is not Rook rook || rook.HasMoved || rook.Colour != side)
        {
            // handle
            return;
        }

        CastlingPath path = new(expectedKingLocation, expectedRookLocation);

        MoveRegistry coveredSquares = new(board);

        var enemyCoveredSquares = coveredSquares.GetByColour(turn.Opposite());

        if (path.GetRequiredEmptySquares().Any(board.IsOccupied))
        {
            // TODO handle
            return;
        }

        if (path.GetKingsPath().Any(enemyCoveredSquares.Contains))
        {
            // handle
            return;
        }

        // Do the castle
        board[path.GetKingsDestination()] = king;
        board[path.GetRooksDestination()] = rook;

        board[expectedKingLocation] = null;
        board[expectedRookLocation] = null;

        king.HasMoved = true;
        rook.HasMoved = true;

    }
}