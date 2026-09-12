using Chess.Board;
using Chess.Pieces;
using Chess.Game.Mechanics;

namespace Chess.Game.Actions;

public class MovePieceAction(Coordinate from, Coordinate to, ChessPiece? pawnPromoted = null) : IAction
{
    public TurnResult Execute(ChessBoard board, Colour turn)
    {
        if (board.IsOutsideBounds(from) || board.IsOutsideBounds(to)) 
            return TurnResult.ERROR_OUTSIDE_BOUNDS;

        if (pawnPromoted is King or Pawn) 
            return TurnResult.ERROR_BAD_PROMOTION;

        using PieceMove move = new(from, to, board, pawnPromoted);
        var endFile = turn == Colour.BLACK ? 0 : board.GetBoundary(0); 

        if (move.FromPiece is null) 
            return TurnResult.ERROR_EMPTY_SOURCE;

        if (move.FromPiece is Pawn && to.X == endFile && pawnPromoted is null) 
            return TurnResult.ERROR_END_PAWN_MUST_PROMOTE;
    
        var possibleMoves = move.FromPiece.GetPossibleMoves(from, board);

        Console.WriteLine("From: " + move.FromPiece.Symbol);
        Console.WriteLine("Possible moves:" + string.Join(", ", possibleMoves));

        if (!possibleMoves.Contains(to)) 
            return TurnResult.ERROR_PIECE_CANT_MOVE_THERE;

        if (move.ToPiece is not null && move.ToPiece.Colour == turn) 
            return TurnResult.ERROR_CANT_CAPTURE_OWN_COLOUR;

        var (friendlyKing, friendlyKingLocation) = board.First(item => item.Item1 is King king && king.Colour == turn);

        MoveRegistry moves = new(board);

        var enemyCoveredSquares = moves.GetByColour(turn.Opposite());

        // check for discovered attack on your own king
        if (enemyCoveredSquares.Contains(friendlyKingLocation)) 
            return TurnResult.ERROR_SELF_DISCOVERED_ATTACK;

        move.Commit();

        return DetermineStatus.DetermineTurnResult(board, moves, turn);
    }
}