using Chess.Board;

namespace Chess.Pieces;

class Bishop(Colour colour) : ChessPiece(colour)
{
    protected override char WhiteSymbol => '♗';
    protected override char BlackSymbol => '♝';

    public override ISet<(int, int)> GetPossibleMoves((int, int) source, ChessBoard board)
    {
        return GetMovesInDirection(
            start: source,
            board,
            directions: [
                // diagonal
                ((1, 1), int.MaxValue),
                ((1, -1), int.MaxValue),
                ((-1, 1), int.MaxValue),
                ((-1, -1), int.MaxValue),
            ]
        );
    }
}