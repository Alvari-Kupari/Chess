using System.Text.RegularExpressions;
using Chess.Board;
using Chess.Move;
using Chess.Pieces;

namespace Chess.Game.Actions;

public class MovePieceAction(Coordinate from, Coordinate to) : IAction
{
    public void Execute(ChessBoard board, Colour turn)
    {
        if (board.IsOutsideBounds(from) || board.IsOutsideBounds(to))
        {
            // todo handle
            return;
        }

        var fromPiece = board[from];

        if (fromPiece is null)
        {
            // todo handle
            return;
        }

        var possibleMoves = fromPiece.GetPossibleMoves(from, board);

        if (!possibleMoves.Contains(to))
        {
            // todo handle invalid move
            return;
        }

        var capturedPiece = board[to];

        if (capturedPiece is not null && capturedPiece.Colour == turn)
        {
            // todo handle
            return;
        }

        board[from] = null;
        board[to] = fromPiece;

        var (friendlyKing, friendlyKingLocation) = board.FirstOrDefault(item => item.Item1 is King king && king.Colour == turn);

        MoveRegistry moves = new(board);

        var enemyCoveredSquares = moves.GetByColour(turn.Opposite());

        // check for discovered attack on your own king
        if (enemyCoveredSquares.Contains(friendlyKingLocation))
        {
            // todo handle
            // revert back
            board[from] = fromPiece;
            board[to] = capturedPiece;

            return;
        }

        fromPiece.OnMove(from, to, board);

        // now determine if its in check / checkmate for the OTHER team
        var (enemyKing, enemyKingLocation) = board.FirstOrDefault(item => item.Item1 is King king && king.Colour != turn);

        var friendlyCoveredSquares = moves.GetByColour(turn);

        if (friendlyCoveredSquares.Contains(enemyKingLocation))
        {
            // todo enemy in check


            var kingsPossibleMoves = enemyKing.GetPossibleMoves(enemyKingLocation, board);

            if (kingsPossibleMoves.All(friendlyCoveredSquares.Contains)) {
                // todo checkmate

            }
            return;
        }

        if (enemyCoveredSquares.Count == 0)
        {
            //stalemate
        }
    }
}