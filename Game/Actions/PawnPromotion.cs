using Chess.Board;
using Chess.Game.Mechanics;
using Chess.Pieces;

namespace Chess.Game.Actions;

public class PawnPromotionAction(Coordinate from, Coordinate to, ChessPiece newPiece) : IAction
{
    public TurnResult Execute(ChessBoard board, Colour turn)
    {
        if (newPiece is King or Pawn) return TurnResult.INVALID_MOVE;
        if (board.IsOutsideBounds(from) || board.IsOutsideBounds(to)) return TurnResult.INVALID_MOVE;
        board[from] = newPiece;
        PieceMove move = new(from, to, board);
        if (move.FromPiece is null) return TurnResult.INVALID_MOVE;

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