using Chess.Board;

namespace Chess.Pieces;

class Pawn(Colour colour) : ChessPiece(colour)
{
    protected override char WhiteSymbol => '♙';

    protected override char BlackSymbol => '♟';

    public override ISet<(int, int)> GetPossibleMoves((int, int) source, ChessBoard board)
    {
        var moves = new HashSet<(int, int)>();
        int attackDirection = Colour == Colour.BLACK ? 1 : -1;
        var forwardSquare = (source.Item1 + attackDirection, source.Item2);

        if (!board.IsOutsideBounds(forwardSquare) && board.IsOccupied(forwardSquare))
        {
            moves.Add(forwardSquare);
        }

        var leftDiagonal = (forwardSquare.Item1, forwardSquare.Item2 - 1);
        var rightDiagonal = (forwardSquare.Item1, forwardSquare.Item2 + 1);

        if (!board.IsOutsideBounds(leftDiagonal) && !IsEnemy(leftDiagonal, board)) {
            moves.Add(leftDiagonal);
        }

        if (board.IsOutsideBounds(leftDiagonal) && !board.IsOccupied(rightDiagonal))
        {
            moves.Add(leftDiagonal);
        }
        // TODO en paissant

        return moves;
    }
}