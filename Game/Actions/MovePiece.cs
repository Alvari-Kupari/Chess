using Chess.Board;
using Chess.Pieces;
using Chess.Game.Mechanics;

namespace Chess.Game.Actions;

public class MovePieceAction(Coordinate from, Coordinate to, ChessPiece? pawnPromoted = null) : IAction
{
    public TurnResult Execute(ChessBoard board, Colour turn)
    {
        if (board.IsOutsideBounds(from) || board.IsOutsideBounds(to)) return TurnResult.INVALID_MOVE;
        if (pawnPromoted is King or Pawn) return TurnResult.INVALID_MOVE;

        using PieceMove move = new(from, to, board, pawnPromoted);
        var endFile = turn == Colour.BLACK ? 0 : board.GetBoundary(0); 
        if (move.FromPiece is null) return TurnResult.INVALID_MOVE;
        if (move.FromPiece is Pawn && to.X == endFile && pawnPromoted is null) return TurnResult.INVALID_MOVE;
    
        var possibleMoves = move.FromPiece.GetPossibleMoves(from, board);

        if (!possibleMoves.Contains(to)) return TurnResult.INVALID_MOVE;
        if (move.ToPiece is not null && move.ToPiece.Colour == turn) return TurnResult.INVALID_MOVE;

        var (friendlyKing, friendlyKingLocation) = board.First(item => item.Item1 is King king && king.Colour == turn);

        MoveRegistry moves = new(board);

        var enemyCoveredSquares = moves.GetByColour(turn.Opposite());

        // check for discovered attack on your own king
        if (enemyCoveredSquares.Contains(friendlyKingLocation)) return TurnResult.INVALID_MOVE;

        move.Commit();

        return DetermineStatus.DetermineTurnResult(board, moves, turn);
    }
}