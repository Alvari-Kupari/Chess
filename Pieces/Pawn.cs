using Chess.Board;

namespace Chess.Pieces;

class Pawn(Colour colour) : ChessPiece(colour)
{
    protected override char WhiteSymbol => '♙';

    protected override char BlackSymbol => '♟';
    private bool hasMoved = false;

    public override ISet<(int, int)> GetPossibleMoves((int, int) source, ChessBoard board)
    {
        var moves = new HashSet<(int, int)>();
        int attackDirection = Colour == Colour.BLACK ? 1 : -1;
        var forwardSquare = (source.Item1 + attackDirection, source.Item2);

        if (!board.IsOutsideBounds(forwardSquare) && !board.IsOccupied(forwardSquare))
        {
            moves.Add(forwardSquare);
            var forwardTwoSquare = (source.Item1 + 2*attackDirection, source.Item2);

            if (!board.IsOutsideBounds(forwardTwoSquare) && !hasMoved && !board.IsOccupied(forwardTwoSquare))
            {
                moves.Add(forwardTwoSquare);
            }
        }

        var leftDiagonal = (forwardSquare.Item1, forwardSquare.Item2 - 1);
        var rightDiagonal = (forwardSquare.Item1, forwardSquare.Item2 + 1);

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

    public override void OnMove((int, int) from, (int, int) to, ChessBoard board)
    {
        hasMoved = true;
    }
}