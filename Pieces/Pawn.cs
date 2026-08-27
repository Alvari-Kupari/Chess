using Chess.Board;
using Chess.Move;

namespace Chess.Pieces;

class Pawn(Colour colour) : TrackedPiece(colour)
{
    protected override char WhiteSymbol => '♙';

    protected override char BlackSymbol => '♟';

    public override ISet<Coordinate> GetPossibleMoves(Coordinate source, ChessBoard board)
    {
        var moves = new HashSet<Coordinate>();
        Coordinate attackDirection = Colour == Colour.BLACK ? new Coordinate(1, 0) : new Coordinate(-1, 0);
        var forwardSquare = source + attackDirection;

        if (!board.IsOutsideBounds(forwardSquare) && !board.IsOccupied(forwardSquare))
        {
            moves.Add(forwardSquare);
            var forwardTwoSquare = source + attackDirection * 2;

            if (!board.IsOutsideBounds(forwardTwoSquare) && !HasMoved && !board.IsOccupied(forwardTwoSquare))
            {
                moves.Add(forwardTwoSquare);
            }
        }

        var leftDiagonal = new Coordinate(forwardSquare.X, forwardSquare.Y - 1);
        var rightDiagonal = new Coordinate(forwardSquare.X, forwardSquare.Y + 1);

        if (!board.IsOutsideBounds(leftDiagonal) && IsEnemy(leftDiagonal, board)) {
            moves.Add(leftDiagonal);
        }

        if (!board.IsOutsideBounds(rightDiagonal) && IsEnemy(rightDiagonal, board))
        {
            moves.Add(rightDiagonal);
        }
        // TODO en paissant

        return moves;
    }
}