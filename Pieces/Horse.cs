using Chess.Board;

namespace Chess.Pieces;

class Horse(Colour colour) : ChessPiece(colour)
{
    protected override char WhiteSymbol => '♘';

    protected override char BlackSymbol => '♞';

    public override ISet<(int, int)> GetPossibleMoves((int, int) source, ChessBoard board)
    {
        return GetMovesInDirection(
            start: source,
            board,
            directions: [
                ((1, 2), 1),
                ((1, -2), 1),
                ((-1, 2), 1),
                ((-1, -2), 1),

                ((2, 1), 1),
                ((2, -1), 1),
                ((-2, 1), 1),
                ((-2, -1), 1),
            ]
        );
    }
}